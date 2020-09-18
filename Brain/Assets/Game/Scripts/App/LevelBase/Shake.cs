using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shake: MonoBehaviour {
    //记录上一次的重力感应的Y值
    private Vector3 _old_acceleration;
    //记录当前的重力感应的Y值
    private Vector3 _new_acceleration;
    //当前手机晃动的距离
    private Vector3 _currentDistance;
    //手机晃动的有效距离
    private float _distanceMin { set; get; } = 0.8f;
    private float _distanceMax { set; get; } = 10f;
    public float needShakeTime = 1.5f;
    private float _shakeTime;
    private float _lastShakeTime;
    private bool _finish;
    
    public Action shakeAction;

    void Start() {
        _lastShakeTime = Time.realtimeSinceStartup;
    }

    // Update is called once per frame
    void Update() {
        _new_acceleration = Input.acceleration;
        _currentDistance = _new_acceleration - _old_acceleration;
        _old_acceleration = _new_acceleration;
        var offset = new Vector3(Math.Abs(_currentDistance.x),Math.Abs(_currentDistance.y),Math.Abs(_currentDistance.z));
        if (offset.x > _distanceMin && offset.x < _distanceMax ||
            offset.y > _distanceMin && offset.y < _distanceMax ||
            offset.z > _distanceMin && offset.z < _distanceMax) {
            Debug.Log($"shakeAction1: {_distanceMin}");
            var currernttime = Time.realtimeSinceStartup;
            /// 重新计时间
            if (currernttime - _lastShakeTime > 1f) {
                _shakeTime = 0;
                _finish = false;
            }
            _shakeTime += (currernttime - _lastShakeTime);
            if (_shakeTime > needShakeTime && !_finish) {
                _finish = true;
                Debug.Log($"shakeAction:{shakeAction}");
                shakeAction?.Invoke();
            }
            _lastShakeTime = currernttime;
        }
    }
}
