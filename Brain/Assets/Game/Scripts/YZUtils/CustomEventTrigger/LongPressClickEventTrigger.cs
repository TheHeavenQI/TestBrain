using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �������ε���¼���������
/// </summary>
public class LongPressClickEventTrigger : CustomEventTrigger {
    /// <summary>
    /// ��������ʱ��
    /// </summary>
    public float pressTriggerTime = 2;
    /// <summary>
    /// ��ε����������
    /// </summary>
    public int _needClickCount = 3;
    /// <summary>
    /// ��ε������ʱ��
    /// </summary>
    public int _maxClickDeltaTime = 2;
    /// <summary>
    /// �����¼�
    /// </summary>
    public event Action onLongPress;
    /// <summary>
    /// ��ε���¼�
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
