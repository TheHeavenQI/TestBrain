using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AnyFingerMove : MonoBehaviour
{
    [SerializeField]
    public int needFingerNum = 3;
    protected Vector3 _originPos;
    protected void Start()
    {
        _originPos = transform.localPosition;
    }
    public float distanceToOrign
    {
        get;
        private set;
    }
    bool draging = false;
    public System.Action onDragEnd;
    private void Update()
    {
        if (!touchCountSatisfy())
        {
            if (draging)
            {
                onDragEnd?.Invoke();
                draging = false;
            }
            return;
        }
        draging = true;
        Vector3 firstFingerPos = Input.GetTouch(0).position;
        Vector3 pos = ComponentTool.getPointPos(transform.parent.gameObject,firstFingerPos);
        transform.position = pos;
        distanceToOrign = Vector3.Distance(transform.localPosition, _originPos);
    }
    bool touchCountSatisfy()
    {
        if (Input.touchCount < needFingerNum)
            return false;
        int touchCount = 0;
        for (int i = 0; i < Input.touchCount; i++)
        {
            // Check if finger is over a UI element
            if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(i).fingerId))
            {
                touchCount += 1;
            }
            if (touchCount >= needFingerNum)
                return true;
        }
        return false;
    }
    public void ReturnToStart()
    {
        transform.localPosition = _originPos;
    }
}
