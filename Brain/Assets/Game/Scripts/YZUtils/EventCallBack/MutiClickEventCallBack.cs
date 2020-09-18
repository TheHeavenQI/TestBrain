using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MutiClickEventCallBack : MonoBehaviour,IPointerDownHandler {
    private int pointCount;
    public int needPointCount;
    private float _intervalTime = 0.5f;
    private float _lastTime = 0;
    public Action onMutiClick;
    
    public void OnPointerDown(PointerEventData eventData) {
        if (Time.time - _lastTime < _intervalTime) {
            pointCount += 1;
        }
        else {
            pointCount = 1;
        }
        if (pointCount > needPointCount) {
            onMutiClick?.Invoke();
        }
        _lastTime = Time.time;
    }
    
}
