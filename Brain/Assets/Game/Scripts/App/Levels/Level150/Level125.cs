using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level125 : LevelBasePage
{
    public DragMoveFollow body;
    public DragMoveFollow top;
    Vector3 bodyPos;
    Vector3 topPos;
    protected override void Start()
    {
        base.Start();
        top.onDragBegin = onTopDragBegin;
       // top.onDragEnd = onTopDragEnd;
        bodyPos = body.transform.localPosition;
        topPos = top.transform.localPosition;
        body.onDragEnd = top.onDragEnd = onDragEnd;
    }
    void onTopDragBegin()
    {
        if (body.isPressing)
        {
            top.mFollowObj = null;
            body.mFollowObj = null;
        }
    }
    //void onTopDragEnd()
    //{
    //    if (body.isPressing)
    //    {
    //       // top.mFollowObj = null;
    //        Completion();
    //    }
    //}
    void onDragEnd()
    {
        if (top.mFollowObj == null)
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
        top.mFollowObj = body.rectTransform;
        body.mFollowObj = top.rectTransform;
        body.transform.localPosition = bodyPos;
        top.transform.localPosition = topPos;
    }
}
