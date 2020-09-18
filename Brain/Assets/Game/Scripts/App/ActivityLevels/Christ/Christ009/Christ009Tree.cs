using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class Christ009Tree : MonoBehaviour
{
    float perDistance = 0;
    private Dictionary<RectTransform, Vector3> mChidrenDic = new Dictionary<RectTransform, Vector3>();
    void Start()
    {
        foreach (Transform child in transform)
        {
            RectTransform rect = child as RectTransform;
            mChidrenDic.Add(rect,rect.anchoredPosition3D);
            if (perDistance == 0)
                perDistance = rect.sizeDelta.y;
        }
    }
    private int curCutIndex = 0;
    private bool isShowAni = false;
    public bool IsShowAni
    {
        get { return isShowAni; }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="direction">0.往右飞，1.往左飞</param>
    /// <returns></returns>
    public bool BeCut(int direction)
    {
        if (curCutIndex == transform.childCount - 1)
            return true;
        Transform child = transform.GetChild(curCutIndex);
        int argu = 500;// Random.Range(100,300);
        int temp = direction == 0 ? argu : -argu;
        isShowAni = true;
        Tweener t = child.DOLocalMoveX(child.localPosition.x + temp, 0.2f);
        t.onComplete = () =>
        {
            child.gameObject.SetActive(false);
            curCutIndex += 1;
            for (int i = curCutIndex; i < transform.childCount; i++)
            {
                RectTransform item = transform.GetChild(i) as RectTransform;
                item.anchoredPosition -= new Vector2(0, perDistance);
            }
            isShowAni = false;
        };
        if (curCutIndex == transform.childCount - 1)
            return true;
        return false;
    }
    public void Reset()
    {
        curCutIndex = 0;
        foreach (var item in mChidrenDic)
        {
            item.Key.anchoredPosition3D = item.Value;
            item.Key.gameObject.SetActive(true);
        }
        isShowAni = false;
    }

  
}
