using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotorCompuerta : MonoBehaviour
{
    [HideInInspector]
    public bool _isOpen;
    [HideInInspector]
    public GameObject _compuerta;
    private SphereCollider _collider;
    private Quaternion _startRot, _endRot;
    public Vector3 _torretaRotation;

    private void Start()
    {
        _compuerta = this.gameObject.transform.GetChild(0).gameObject;
        _collider = GetComponent<SphereCollider>();
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
