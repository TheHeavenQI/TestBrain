using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class Level103 : LevelBasePage
{
    public List<RectTransform> mCanSelList;
    public RectTransform mTargetArea;
    Dictionary<RectTransform, Vector2> difPos;
    int curSelIndex = 0;
    protected override void Start()
    {
        base.Start();
        difPos = new Dictionary<RectTransform, Vector2>();
        foreach (var item in mCanSelList)
            difPos.Add(item,item.anchoredPosition);
    }

    public void ClickItem(RectTransform itemRect)
    {    
        Transform target = mTargetArea.GetChild(curSelIndex);
        itemRect.SetParent(target);
        itemRect.DOAnchorPos(Vector2.zero,0.2f);
        curSelIndex += 1;
    }
    public void ClickCommit()
    {
        bool correct = false;
        Transform o_1_parent = mCanSelList[5].parent;
        Transform o_2_parent = mCanSelList[6].parent;
        correct = curSelIndex == 2 && o_1_parent != transform&& o_2_parent != transform;
        if (correct)
            Completion();
        else
        {
            ShowError();
            Refresh();
        }
    }
    public override void Refresh()
    {
        base.Refresh();
        foreach (var item in mCanSelList)
        {
            item.SetParent(transform);
            item.anchoredPosition = difPos[item];
        }
        curSelIndex = 0;
    }
}
