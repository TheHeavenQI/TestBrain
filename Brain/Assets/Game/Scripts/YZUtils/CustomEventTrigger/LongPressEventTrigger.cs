using System;
using UnityEngine;

/// <summary>
/// 长按事件触发功能
/// </summary>
public class LongPressEventTrigger : CustomEventTrigger {
    /// <summary>
    /// 长按触发时间
    /// </summary>
    public float pressTriggerTime = 2;
    /// <summary>
    /// 长按事件
    /// </summary>
    public event Action onLongPress;

    private bool _isPressFinish;

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
    }
}
