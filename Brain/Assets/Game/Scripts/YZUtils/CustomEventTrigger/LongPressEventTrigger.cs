using System;
using UnityEngine;

/// <summary>
/// �����¼���������
/// </summary>
public class LongPressEventTrigger : CustomEventTrigger {
    /// <summary>
    /// ��������ʱ��
    /// </summary>
    public float pressTriggerTime = 2;
    /// <summary>
    /// �����¼�
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
