using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class Torreta : MonoBehaviour , IDragHandler
{
    private bool _drag;

    #region AcessValues
    private Swipe _swipe;
    private Camera _cam;
    private TakeAndPlace _tkp;
    private GameObject _parent;
    private SliderInventaro _inventory;
    #endregion

    private Vector3 _startPos, _finalPos, _baseRotation;

    private void Start()
    {
        _drag = false;
        _startPos = transform.localPosition;
        _finalPos = _startPos;

        #region AcessObjects
        _swipe = GameObject.FindGameObjectWithTag("GameController").GetComponent<Swipe>();
        _cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        _tkp = GameObject.FindGameObjectWithTag("GameController").GetComponent<TakeAndPlace>();
        _inventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<SliderInventaro>();
        #endregion
    }

    private void Update()
    {
        if (_drag)
            _tkp.RayCast();
        else
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, _finalPos, Time.deltaTime * 10);
            transform.localRotation = Quaternion.Euler(_baseRotation.x, _baseRotation.y, _baseRotation.z);
        }
    }

    private void OnMouseUp()
    {
        if(_parent != null)
        {
            if (!_parent.GetComponentInParent<RotorCompuerta>()._isOpen)
            {
                transform.parent = _parent.transform;
                _parent.GetComponentInParent<RotorCompuerta>()._isOpen = true;
                _baseRotation = _parent.GetComponentInParent<RotorCompuerta>()._torretaRotation;
                _finalPos = Vector3.zero;
            }
            else
                _finalPos = Vector3.zero;
        }
        else
        {
            _finalPos = _startPos;
            _baseRotation = Vector3.zero;
        }

        if (_tkp._currentObject != null)
            _tkp._currentObject = null;

        _drag = _swipe._blockSwipe = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        _drag = _swipe._blockSwipe = true;

        Vector3 _mousePosition = Input.mousePosition;
        _mousePosition.z = _tkp._separation;
        transform.position = Vector3.Lerp(transform.position, _cam.ScreenToWorldPoint(_mousePosition), Time.deltaTime * 50);

        if (_tkp._currentObject != null)
            _parent = _tkp._currentObject;
        else
        {
            transform.parent = _inventory._inventory[1].transform;
            
            if(_parent != null)
                _parent.GetComponentInParent<RotorCompuerta>()._isOpen = false;
            
            _parent = null;
        }
    }
}
