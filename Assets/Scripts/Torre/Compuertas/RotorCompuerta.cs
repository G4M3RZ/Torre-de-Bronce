using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotorCompuerta : MonoBehaviour
{
    public int _num;
    //[HideInInspector]
    public bool _isOpen;
    private GameObject _compuerta;
    private BoxCollider _collider;
    private Quaternion _startRot, _endRot;

    private void Start()
    {
        _compuerta = this.gameObject.transform.GetChild(0).gameObject;
        _collider = GetComponent<BoxCollider>();
        _collider.isTrigger = true;
        _startRot = transform.localRotation;
        _endRot.eulerAngles = Vector3.zero;
    }

    private void Update()
    {
        if (!_isOpen)
        {
            _collider.enabled = true;
            transform.localRotation = Quaternion.Lerp(transform.localRotation, _startRot, Time.deltaTime * 5);
        }
        else
        {
            _collider.enabled = false;
            transform.localRotation = Quaternion.Lerp(transform.localRotation, _endRot, Time.deltaTime * 5);
        }
    }
}
