
using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// 回调顺序 OnPointerDown  OnBeginDrag  OnDrag  OnPointerUp  OnPointerClick  OnEndDrag
/// </summary>
/// 
public class DragMove : EventCallBack {

    public float offsetX = 0;
    public float offsetY = 0;
    public RectTransform mRefRect;
    [HideInInspector]
    public bool enabelDrag = true;
    
    private Vector3 _originPos;
    
    private RectTransform _rectTransform;
    public Action onDrag;
    public RectTransform rectTransform {
        get {
            if (_rectTransform == null) {
                _rectTransform = transform as RectTransform;
            }
            return _rectTransform;
        }
    }

    protected virtual void Awake() {
        base.Awake();
        _rectTransform = transform as RectTransform;
        
    }

    protected virtual void Start() {
        _originPos = transform.position;
        onDragDraging = (a) => {
            if (enabelDrag) {
                Rect rect;
                if (mRefRect == null)
                    rect = transform.parent.GetComponent<RectTransform>().rect;
                else
                    rect = mRefRect.rect;
                transform.position = a;
                var vector3 = RangeVector(transform.localPosition, rect.x + offsetX, rect.y + offsetY, rect.width - offsetX * 2, rect.height - offsetY * 2);
                transform.localPosition = vector3;
                onDrag?.Invoke();
            }
        };
    }

    public Vector3 GetOriginPos() {
        return _originPos;
    }

    public void RefreshOriginPos() {
        _originPos = transform.position;
    }

    /// <summary>
    /// 回到最开始的位置
    /// </summary>
    public void Return2OriginPos() {
        transform.position = _originPos;
    }

    /// <summary>
    /// 回到最开始的位置
    /// </summary>
    public Tweener Return2OriginPos(float duration) {
        return transform.DOMove(_originPos, duration);
    }

    private Vector3 RangeVector(Vector3 vector, float x, float y, float w, float h) {
        if (vector.x < x) vector.x = x;
        if (vector.y < y) vector.y = y;
        if (vector.x > x + w) vector.x = x + w;
        if (vector.y > y + h) vector.y = y + h;
        return vector;
    }

}
