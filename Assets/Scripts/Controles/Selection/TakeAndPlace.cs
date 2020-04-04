using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeAndPlace : MonoBehaviour
{
    public LayerMask _layer;
    [Range(1,10)]
    public float _separation;
    [Range(10, 20)]
    public float _raycastRange;

    private Camera _cam;
    [HideInInspector]
    public GameObject _currentObject;

    private void Start()
    {
        _cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    public void RayCast()
    {
        #region PC Input

        Ray ray = _cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, _raycastRange, _layer))
        {
            if (hit.collider.CompareTag("Base"))
                _currentObject = hit.collider.GetComponent<RotorCompuerta>()._compuerta;
            else
                _currentObject = null;
        }
        else
        {
            _currentObject = null;
        }

        #endregion
    }
}
