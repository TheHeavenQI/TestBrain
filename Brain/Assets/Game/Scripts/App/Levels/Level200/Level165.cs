using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class Level165 : LevelBasePage
{
    public Image mBaby_1;
    public Image mBaby_2;
    public Image mBaby_3;
    public Transform babyHair;
    Vector3 hairDefPos;
    public RectTransform babyHead;
    public DragMove tool_1;
    public DragMove tool_2;
    public DragMove tool_3;
    protected override void Start()
    {
        base.Start();
        hairDefPos = babyHair.localPosition;
        tool_1.onDragEnd = () =>
         {
             tool_1.Return2OriginPos();
             ShowError();
         };
        tool_2.onDragEnd = () =>
        {
            tool_2.Return2OriginPos();
            ShowError();
        };
        tool_3.onDragEnd = onCorrectDragEnd;
    }
    /// <summary>
    /// 
    /// </summary>
    void onCorrectDragEnd()
    {
        if (triggerSleep &&RectTransformExtensions.IsRectTransformOverlap(babyHead, tool_3.transform as RectTransform))
        {
            mBaby_2.enabled = false;
            babyHair.DOLocalMove(hairDefPos + new Vector3(10, -300, 0), 1).OnComplete(() => { Completion(); });
            babyHair.DORotate(new Vector3(0, 0, -180), 1);
            mBaby_3.enabled = true;
           // Completion();
        }
        else
        {
            tool_3.Return2OriginPos();
            ShowError();
        }
    }
    bool triggerSleep = false;
    private void Update()
    {
        if (triggerSleep)
            return;
        if (Input.acceleration.z > 0.8f) //手机反转
        {       
            triggerSleep = true;
            mBaby_1.enabled = false;
            mBaby_2.enabled = true;
            mBaby_3.enabled = false;
        }
    }
    public override void Refresh()
    {
        base.Refresh();
        babyHair.transform.localPosition = hairDefPos;
        babyHair.transform.localEulerAngles = Vector3.zero;
        triggerSleep = false;
        mBaby_1.enabled = true;
        mBaby_2.enabled = false;
        mBaby_3.enabled = false;
    }
}
