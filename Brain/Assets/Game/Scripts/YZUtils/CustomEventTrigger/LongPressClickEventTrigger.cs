using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 长按或多次点击事件触发功能
/// </summary>
public class LongPressClickEventTrigger : CustomEventTrigger {
    /// <summary>
    /// 长按触发时间
    /// </summary>
    public float pressTriggerTime = 2;
    /// <summary>
    /// 多次点击触发次数
    /// </summary>
    public int _needClickCount = 3;
    /// <summary>
    /// 多次点击限制时间
    /// </summary>
    public int _maxClickDeltaTime = 2;
    /// <summary>
    /// 长按事件
    /// </summary>
    public event Action onLongPress;
    /// <summary>
    /// 多次点击事件
    /// </summary>
    public event Action onMultClick;

    private bool _isPressFinish;

    private Queue<float> _clickTimeQueue = new Queue<float>();
    private bool _isClickFinish;

    protected override void Start() {
        base.Start();

        this.onPointerClick += (d) => {
            if (_isClickFinish) {
                return;
            }
            _clickTimeQueue.Enqueue(Time.time);
            while (Time.time - _clickTimeQueue.Peek() > _maxClickDeltaTime) {
                _clickTimeQueue.Dequeue();
            }
            if (_clickTimeQueue.Count >= _needClickCount) {
                _isClickFinish = true;
                onMultClick.Invoke();
            }
        };
    }

    private void Update() {
        if (!_isPressFinish) {
            if (isPress) {
                if (Time.time - pointDownTime >= pressTriggerTime) {
                    _isPressFinish = true;
                    onLongPress?.Invoke();
                }
            }
        }
    }

    public void ResetFinish() {
        _isPressFinish = false;
        pointDownTime = Time.time;
        _isClickFinish = false;
        _clickTimeQueue.Clear();
    }
}
