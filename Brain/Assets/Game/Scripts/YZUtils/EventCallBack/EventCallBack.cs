using System;
using System.Collections.Generic;
using BaseFramework;
using UnityEngine;
using UnityEngine.EventSystems;

public enum SwipeDirection {
    None,
    Up,
    Down,
    Left,
    Right,
}
/// <summary>
/// 回调顺序 OnPointerDown  OnBeginDrag  OnDrag  OnPointerUp  OnPointerClick  OnEndDrag
/// </summary>
public class EventCallBack :  MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler {
    
    public bool isPressing { get; private set; }
    /// <summary>
    /// 是否为长按手势
    /// </summary>
    public bool isLongPress { get; private set; }
    public bool isDraging { get; private set; }

    /// <summary>
    /// 需要按压的时长
    /// </summary>
    public float needPressTime { get; set; } = 2f;
    /// <summary>
    /// 当前按压时长
    /// </summary>
    private float _currentPressTime;
    
    // 回调
    public Action onDragEnd;
    public Action onClick;
    public Action<Vector3> onDragDraging;
    public Action<Vector3> onDragDragingWithOutOffset;
    public Action onDragBegin;
    public Action onPointerDown;
    /// <summary>
    /// 带参
    /// </summary>
    public Action<PointerEventData> OnPointDown;
    public Action onPointerUp;
    public Action onLongPress;
    /// <summary>
    /// 相同方向，回调一次
    /// </summary>
    public Action<SwipeDirection> onSwipe;
    /// <summary>
    /// 相同方向，持续回调
    /// </summary>
    public Action<SwipeDirection> onSwipeRepeat;
    private float swipeDistance = 10;
    /// <summary>
    /// 上下左右
    /// </summary>
    private SwipeDirection _swipeDirection = SwipeDirection.None;
    private Vector2 lastDragPos = Vector2.zero;
    protected virtual  void Awake() {
        
    }
    
    public void OnPointerDown(PointerEventData eventData) {
        isPressing = true;
        isLongPress = false;
        isDraging = false;
        _swipeDirection = SwipeDirection.None;
        onPointerDown?.Invoke();
        OnPointDown?.Invoke(eventData);
        Log("OnPointerDown");
    }
    
    public void OnPointerUp(PointerEventData eventData) {
        isPressing = false;
        onPointerUp?.Invoke();
        Log("OnPointerUp");
    }


    public void OnPointerClick(PointerEventData eventData) {
        if (!isDraging && !isLongPress) {
            onClick?.Invoke();
        }
        Log("OnPointerClick");
    }

    //    拖拽
    //    IDragHandler,IBeginDragHandler,IEndDragHandler
    private Vector3 offsetDragPos;  //临时记录点击点与UI的相对位置
    public void OnBeginDrag(PointerEventData eventData) {
        var pos = ComponentTool.getPointPos(gameObject, eventData);
        isDraging = true;
        offsetDragPos = pos - transform.position;
        onDragBegin?.Invoke();
        Log("OnBeginDrag");
    }
    public void OnDrag(PointerEventData eventData) {
        var pos = ComponentTool.getPointPos(gameObject, eventData);
        onDragDragingWithOutOffset?.Invoke(pos);
        onDragDraging?.Invoke(pos-offsetDragPos);
        if (onSwipe != null || onSwipeRepeat != null) {
            var position = eventData.position;
            var dis = Vector2.Distance(lastDragPos, position);
            if (Vector2.Distance(lastDragPos,position) > swipeDistance) {
                var posVec = new Vector2(position.x,position.y);
                var direction = GetSwipeDirection(posVec - lastDragPos);
                lastDragPos = posVec;
                onSwipeRepeat?.Invoke(direction);
                if (_swipeDirection != direction) {
                    _swipeDirection = direction;
                    Log($"SwipeDirection.{_swipeDirection}");
                    onSwipe?.Invoke(_swipeDirection);
                }
            }

        }
        Log("OnDrag");
    }
    
    public void OnEndDrag(PointerEventData eventData) {
        onDragEnd?.Invoke();
        Log("OnEndDrag");
    }
    
    /// <summary>
    /// 滑动方向
    /// </summary>
    private SwipeDirection GetSwipeDirection(Vector2 m_Dir) {
        
        if (m_Dir.y < m_Dir.x && m_Dir.y > -m_Dir.x)
        {
            return SwipeDirection.Right;
        }
        else if (m_Dir.y > m_Dir.x && m_Dir.y < -m_Dir.x)
        {
            return SwipeDirection.Left;
        }
        else if (m_Dir.y > m_Dir.x && m_Dir.y > -m_Dir.x)
        {
            return SwipeDirection.Up;
        }
        else if (m_Dir.y < m_Dir.x && m_Dir.y < -m_Dir.x)
        {
            return SwipeDirection.Down;
        }
        return SwipeDirection.None;
    }
    
    protected virtual void Update() {
        if (isPressing) {
            _currentPressTime += Time.deltaTime;
        } else {
            _currentPressTime = 0;
        }
        
        if (!isLongPress) {
            if (_currentPressTime > needPressTime) {
                Log($"_currentPressTime:{_currentPressTime}");
                isLongPress = true;
                onLongPress?.Invoke();
            }
        }
    }
    
    private void Log(string value) {
//        Debug.Log($"EventCallBack:{value}");
    }
    
    
}
