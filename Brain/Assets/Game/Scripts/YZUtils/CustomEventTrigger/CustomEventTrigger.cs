using System;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// 事件触发功能
/// </summary>
/// <see cref="UnityEngine.EventSystems.EventTrigger"/>
public class CustomEventTrigger :
    MonoBehaviour,
    IPointerEnterHandler,
    IEventSystemHandler,
    IPointerExitHandler,
    IPointerClickHandler,
    IPointerDownHandler,
    IPointerUpHandler,
    IInitializePotentialDragHandler,
    IBeginDragHandler,
    IDragHandler,
    IEndDragHandler,
    IDropHandler,
    IScrollHandler,
    IUpdateSelectedHandler,
    ISelectHandler,
    IDeselectHandler,
    IMoveHandler,
    ISubmitHandler,
    ICancelHandler {

    //Reference: UnityEngine.EventSystems.EventTrigger

    public Action<PointerEventData> onBeginDrag;
    public Action<BaseEventData> onCancel;
    public Action<BaseEventData> onDeselect;
    public Action<PointerEventData> onDrag;
    public Action<PointerEventData> onDrop;
    public Action<PointerEventData> onEndDrag;
    public Action<PointerEventData> onInitializePotentialDrag;
    public Action<AxisEventData> onMove;
    public Action<PointerEventData> onPointerClick;
    public Action<PointerEventData> onPointerDown;
    public Action<PointerEventData> onPointerEnter;
    public Action<PointerEventData> onPointerExit;
    public Action<PointerEventData> onPointerUp;
    public Action<PointerEventData> onScroll;
    public Action<BaseEventData> onSelect;
    public Action<BaseEventData> onSubmit;
    public Action<BaseEventData> onUpdateSelected;

    private RectTransform _rectTransform;

    public RectTransform rectTransform {
        get {
            if (_rectTransform == null) {
                _rectTransform = transform as RectTransform;
            }
            return _rectTransform;
        }
    }

    protected float pointDownTime;
    protected float pointUpTime;
    public bool isPress { get; private set; }
    private const float POINT_CLICK_TIME = 0.2f;


    protected virtual void Awake() {
        _rectTransform = transform as RectTransform;
    }

    protected virtual void Start() {

    }

    public virtual void OnBeginDrag(PointerEventData data) {
        onBeginDrag?.Invoke(data);
    }

    public virtual void OnCancel(BaseEventData data) {
        onCancel?.Invoke(data);
    }

    public virtual void OnDeselect(BaseEventData data) {
        onDeselect?.Invoke(data);
    }

    public virtual void OnDrag(PointerEventData data) {
        onDrag?.Invoke(data);
    }

    public virtual void OnDrop(PointerEventData data) {
        onDrop?.Invoke(data);
    }

    public virtual void OnEndDrag(PointerEventData data) {
        onEndDrag?.Invoke(data);
    }

    public virtual void OnInitializePotentialDrag(PointerEventData data) {
        onInitializePotentialDrag?.Invoke(data);
    }

    public virtual void OnMove(AxisEventData data) {
        onMove?.Invoke(data);
    }

    public virtual void OnPointerClick(PointerEventData data) {

        if (pointUpTime - pointDownTime <= POINT_CLICK_TIME) {
            onPointerClick?.Invoke(data);
        }
    }

    public virtual void OnPointerDown(PointerEventData data) {
        pointDownTime = Time.time;
        isPress = true;
        onPointerDown?.Invoke(data);
    }

    public virtual void OnPointerEnter(PointerEventData data) {
        onPointerEnter?.Invoke(data);
    }

    public virtual void OnPointerExit(PointerEventData data) {
        onPointerExit?.Invoke(data);
    }

    public virtual void OnPointerUp(PointerEventData data) {
        pointUpTime = Time.time;
        isPress = false;
        onPointerUp?.Invoke(data);
    }

    public virtual void OnScroll(PointerEventData data) {
        onScroll?.Invoke(data);
    }

    public virtual void OnSelect(BaseEventData data) {
        onSelect?.Invoke(data);
    }

    public virtual void OnSubmit(BaseEventData data) {
        onSubmit?.Invoke(data);
    }

    public virtual void OnUpdateSelected(BaseEventData data) {
        onUpdateSelected?.Invoke(data);
    }
}