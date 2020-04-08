using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class Torreta : MonoBehaviour , IDragHandler, IEndDragHandler
{
    private bool _drag;

    #region AcessValues
    private Swipe _swipe;
    private Camera _cam;
    private TakeAndPlace _tkp;
    private GameObject _parent;
    private SliderInventaro _inventory;
    #endregion

    private Vector3 _finalPos, _baseRotation;

    private void Start()
    {
        _drag = false;
        if (transform.parent == null)
            _finalPos = transform.position;
        else
            _finalPos = new Vector3(0, -50, 0);

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

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("soltar");
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
            //convertir a hijo de slot en el inventario
            for (int i = 0; i < _inventory._inventory.Count; i++)
            {
                if (_inventory._inventory[i].transform.childCount == 0)
                {
                    transform.parent = _inventory._inventory[i].transform;
                    break;
                }   
            }

            _finalPos = new Vector3(0,-50, 0);
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
            transform.parent = null;

            if (_parent != null)
                _parent.GetComponentInParent<RotorCompuerta>()._isOpen = false;
            
            _parent = null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Base"))
        {
            _parent = other.transform.GetChild(0).gameObject;
            transform.parent = _parent.transform;
            _baseRotation = other.GetComponent<RotorCompuerta>()._torretaRotation;
            transform.localPosition = _finalPos = Vector3.zero;
            other.GetComponent<RotorCompuerta>()._isOpen = true;
        }
    }
}
