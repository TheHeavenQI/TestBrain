using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class Level145 : LevelBasePage
{
    float targetValue = 310;
    float defValue = 121;
    public Image mValue;
    public InputField mInput;
    public EventCallBack mEventScript;

    protected override void Start()
    {
        base.Start();
        mEventScript.needPressTime = 1f;
        mEventScript.onLongPress = longPress;
    }
    void longPress()
    {
        mInput.text = "37";
        Tweener t=  mValue.rectTransform.DOSizeDelta(new Vector2(18, targetValue),0.5f);
        //mValue.rectTransform.sizeDelta = new Vector2(18, targetValue);
        t.onComplete = () => { Completion(); };
    }
    public void clickCommit()
    {
        ShowError();
    }
    public override void Refresh()
    {
        base.Refresh();
        mInput.text = "";
        mValue.rectTransform.sizeDelta = new Vector2(18, defValue);
    }
}
