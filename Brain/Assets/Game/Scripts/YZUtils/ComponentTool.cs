using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// 主要对unity组件的一些简单操作
/// </summary>
public static class ComponentTool
{
    public static Transform GetChildFromGrid(Transform parent,int index)
    {
        if (parent.childCount == 0)
            throw new System.Exception("use this method the parent at least need one child Template");
        if (index < parent.childCount)
            return parent.GetChild(index);
        else
        {
            Transform temp = parent.GetChild(0);
            Transform ret = GameObject.Instantiate(temp);
            ret.SetParent(parent);
            ret.localScale = temp.localScale;ret.localEulerAngles = temp.localEulerAngles;
            ret.SetAsLastSibling();
            return ret;
        } 
    }
    public static void HidenChildren(Transform parent)
    {
        int count = parent.childCount;
        if (count > 0)
        {
            for (int i = 0; i < count; i++)
            {
                parent.GetChild(i).gameObject.SetActive(false) ;
            }
        }
      
    }
    public static Vector3 getPointPos(GameObject go,PointerEventData eventData)
    {
        RectTransform rt = go.GetComponent<RectTransform>();
        if (rt == null)
        {
            Debug.LogError("get pos from eventData error");
            return Vector3.zero;
        }
        Vector3 globalUIPos;
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(rt, eventData.position, eventData.pressEventCamera, out globalUIPos))
        {
            return globalUIPos;
        }
        return Vector3.zero;
    }
    public static Vector3 getPointPos(GameObject go, Vector3 eventDataPos)
    {
        RectTransform rt = go.GetComponent<RectTransform>();
        if (rt == null)
        {
            Debug.LogError("get pos from eventData error");
            return Vector3.zero;
        }
        Vector3 globalUIPos;
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(rt, eventDataPos,Camera.main, out globalUIPos))
        {
            return globalUIPos;
        }
        return Vector3.zero;
    }
}
