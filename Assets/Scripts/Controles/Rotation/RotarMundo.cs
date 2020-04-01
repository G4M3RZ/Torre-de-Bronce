using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotarMundo : MonoBehaviour
{
    public Swipe _swipe;
    private int _rotMundo;

    private void Start()
    {
        _rotMundo = 0;
    }
    private void Update()
    {
        Rotar();
        if (_swipe._swipeLeft)
        {
            _rotMundo += 90;
        }
        if (_swipe._swipeRight)
        {
            _rotMundo -= 90;
        }
    }
    void Rotar()
    {
        Quaternion _newRot = Quaternion.Euler(0, _rotMundo, 0);
        transform.rotation = Quaternion.Lerp(transform.rotation, _newRot, Time.deltaTime * 10);
    }
}
