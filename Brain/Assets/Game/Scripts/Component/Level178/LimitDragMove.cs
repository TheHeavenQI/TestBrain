using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// 受限的拖拽物体
/// </summary>
public class LimitDragMove : DragMoveEventTrigger {
    /// <summary>
    /// 禁止X方向移动
    /// </summary>
    [Tooltip("禁止Y方向移动")]
    public bool freezeX = false;
    /// <summary>
    /// 禁止Y方向移动
    /// </summary>
    [Tooltip("禁止Y方向移动")]
    public bool freezeY = false;
    /// <summary>
    /// X方向障碍物，禁止穿越
    /// </summary>
    [Tooltip("X方向障碍物，禁止穿越")]
    public List<float> barrierX;
    /// <summary>
    /// Y方向障碍物，禁止穿越
    /// </summary>
    [Tooltip("Y方向障碍物，禁止穿越")]
    public List<float> barrierY;
    /// <summary>
    /// 障碍物,是否使用本地坐标
    /// </summary>
    [Tooltip("障碍物,是否使用本地坐标")]
    public bool isLocalBarrier = true;

    public override void OnDrag(PointerEventData data) {
        onDrag?.Invoke(data);
        if (enableDragMove) {
            if (RectTransformUtility.ScreenPointToWorldPointInRectangle(this.rectTransform, data.position,
                                                               Camera.main,
                                                               out Vector3 worldPoint)) {

                Vector3 aim = worldPoint - offset;

                if (freezeX) {
                    aim.x = this.rectTransform.position.x;
                }
                if (freezeY) {
                    aim.y = this.rectTransform.position.y;
                }



                this.rectTransform.position = aim;
                var cur = transform.localPosition;
                if (barrierY.Count > 0 && transform.localPosition.y < barrierY[0])
                {
                    transform.localPosition = new Vector3(cur.x ,barrierY[0],cur.z);
                }
            }
        }
    }

    private Vector3 CheckLocalBarrier(Vector3 aim) {

        if (rectTransform.parent == null) {
            return CheckWorldBarrier(aim);
        }

        Vector3 cur = this.rectTransform.localPosition;

        //世界坐标转为本地坐标
        aim = rectTransform.parent.InverseTransformPoint(aim);

        foreach (float barrier in barrierX) {
            if (cur.x <= barrier && aim.x > barrier || cur.x >= barrier && aim.x < barrier) {
                aim.x = cur.x;
            }
        }
        foreach (float barrier in barrierY) {
            if (cur.y <= barrier && aim.y > barrier || cur.y >= barrier && aim.y < barrier) {
                aim.y = cur.y;
            }
        }

        //本地坐标转为世界坐标
        aim = rectTransform.parent.TransformPoint(aim);
        return aim;
    }

    private Vector3 CheckWorldBarrier(Vector3 aim) {

        Vector3 cur = this.rectTransform.position;

        foreach (float barrier in barrierX) {
            if (cur.x <= barrier && aim.x > barrier || cur.x >= barrier && aim.x < barrier) {
                aim.x = cur.x;
            }
        }
        foreach (float barrier in barrierY) {
            if (cur.y <= barrier && aim.y > barrier || cur.y >= barrier && aim.y < barrier) {
                aim.y = cur.y;
            }
        }
        return aim;
    }
}
