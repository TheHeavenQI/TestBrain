using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 多次点击事件触发功能
/// </summary>
public class MultClickEventTrigger : CustomEventTrigger {
    /// <summary>
    /// 多次点击触发次数
    /// </summary>
    public int _needClickCount = 3;
    /// <summary>
    /// 多次点击限制时间
    /// </summary>
    public int _maxClickDeltaTime = 2;
    /// <summary>
    /// 多次点击事件
    /// </summary>
    public event Action onMultClick;

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

    public void ResetFinish() {
        _isClickFinish = false;
        _clickTimeQueue.Clear();
    }
}
