using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// 拖拽物体
/// </summary>
public class DragMoveEventTrigger : CustomEventTrigger {
    /// <summary>
    /// 是否可以拖拽
    /// </summary>
    public bool enableDragMove { get; set; } = true;
    /// <summary>
    /// 物体起始位置
    /// </summary>
    protected Vector3 originPos;
    /// <summary>
    /// 开始拖拽是手指偏移量
    /// </summary>
    protected Vector3 offset;

    protected override void Start() {
        base.Start();
        originPos = transform.position;
    }

    public override void OnBeginDrag(PointerEventData data) {
        base.OnBeginDrag(data);
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(this.rectTransform, data.position,
                                                               Camera.main,
                                                               out Vector3 worldPoint)) {
            offset = worldPoint - transform.position;
        }
    }

    public override void OnDrag(PointerEventData data) {
        base.OnDrag(data);
        if (enableDragMove) {
            if (RectTransformUtility.ScreenPointToWorldPointInRectangle(this.rectTransform, data.position,
                                                               Camera.main,
                                                               out Vector3 worldPoint)) {
                this.rectTransform.position = worldPoint - offset;
            }
        }
    }

    public Vector3 GetOriginPos() {
        return originPos;
    }

    public void RefreshOriginPos() {
        originPos = transform.position;
    }

    /// <summary>
    /// 回到最开始的位置
    /// </summary>
    public void Return2OriginPos() {
        transform.position = originPos;
    }

    /// <summary>
    /// 回到最开始的位置
    /// </summary>
    public Tweener Return2OriginPos(float duration) {
        return transform.DOMove(originPos, duration);
    }
}
