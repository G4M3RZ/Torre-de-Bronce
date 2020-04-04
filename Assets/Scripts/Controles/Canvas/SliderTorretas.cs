using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderTorretas : MonoBehaviour
{
    private bool _open;
    private RectTransform _rTns;
    private float pong, _startPos, _newPos;

    private void Start()
    {
        _open = false;
        _rTns = this.gameObject.transform.GetChild(1).GetComponent<RectTransform>();
        _startPos = _rTns.localPosition.x;
        pong = _startPos;
        _newPos = _startPos + 200;
    }
    private void FixedUpdate()
    {
        _rTns.localPosition = new Vector2(pong, 0);

        if (_open)
            pong = (pong < _newPos) ? pong += 50 : pong = _newPos;
        else
            pong = (pong > _startPos) ? pong -= 50 : pong = _startPos;
    }

    public void GaleryButton()
    {
        _open = !_open;
    }
}
