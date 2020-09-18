using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class ConnectLine : MonoBehaviour, IBeginDragHandler, IEndDragHandler,IDragHandler
{
    public RectTransform point;
    RectTransform rect;
    public System.Action TriggerConnect;
    void Start()
    {
        rect = transform as RectTransform;
    }
    float xMin = 0;
    float xMax = 0;
    float startDragx = 0;
    float endDragx = 0;
    public void OnBeginDrag(PointerEventData eventData)
    {
        Vector3 pos = ComponentTool.getPointPos(transform.parent.gameObject,eventData);
        point.position = pos;
        startDragx = point.localPosition.x;
    }
    public void OnDrag(PointerEventData eventData)
    {
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        Vector3 pos = ComponentTool.getPointPos(transform.parent.gameObject, eventData);
        point.position = pos;
        endDragx = point.localPosition.x;
        float needWidth = rect.sizeDelta.x * 0.4f;
        //Debug.LogError(startDragx + " : " + endDragx + "  :  " + needWidth);
        if (startDragx < -needWidth * 0.5f && endDragx > needWidth * 0.5f || endDragx < -needWidth * 0.5f && startDragx > needWidth * 0.5f)
            TriggerConnect?.Invoke();
    }
}
