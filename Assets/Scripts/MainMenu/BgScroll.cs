using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class BgScroll : MonoBehaviour
{
    [SerializeField] private RawImage _Bg;
    [SerializeField] private float _x, _y;

    void Update()
    {
        _Bg.uvRect = new Rect(_Bg.uvRect.x + _x * Time.deltaTime, _Bg.uvRect.y + _y * Time.deltaTime, _Bg.uvRect.width, _Bg.uvRect.height);
    }
}