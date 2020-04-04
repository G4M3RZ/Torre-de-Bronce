using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderInventaro : MonoBehaviour
{
    //Slider
    private Swipe _swp;
    private RectTransform _rTns;
    private float pong, _startPos, _newPos;
    private bool _swipeUp, _swipeDown;

    //Slots
    public List<GameObject> _inventory;
    [Range(0,10)]
    public int SlotsNum;

    private void Start()
    {
        #region Slider
        _swp = GameObject.FindGameObjectWithTag("GameController").GetComponent<Swipe>();
        _rTns = this.gameObject.transform.GetChild(0).GetComponent<RectTransform>();
        _startPos = _rTns.localPosition.y;
        pong = _startPos;
        _newPos = _startPos + 200;
        #endregion

        _inventory = new List<GameObject>();
        for (int i = 0; i < SlotsNum; i++)
        {
            _inventory.Add(this.gameObject.transform.GetChild(0).GetChild(i).gameObject);
        }
    }

    private void FixedUpdate()
    {
        Slider();
        Slots();
    }

    void Slider()
    {
        _rTns.localPosition = new Vector2(0, pong);

        if (_swp._swipeUp)
            _swipeUp = true;
        else if (_swp._swipeDown)
            _swipeDown = true;

        if (_swipeUp)
        {
            pong = (pong < _newPos) ? pong += 50 : pong = _newPos;

            if (pong == _newPos)
                _swipeUp = false;
        }
        else if (_swipeDown)
        {
            pong = (pong > _startPos) ? pong -= 50 : pong = _startPos;

            if (pong == _startPos)
                _swipeDown = false;
        }
    }

    void Slots()
    {

    }
}
