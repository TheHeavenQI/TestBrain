using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Level185 : LevelBasePage
{
    private int stateIndex = 0;
    public Shake mShake;
    public Image[] states;
    public DragMove chongzi;
    public RectTransform yugou;
    protected override void Start()
    {
        base.Start();
        mShake.shakeAction = onShakePhone;
        chongzi.onDragEnd = onChongziDragEnd;
    }
    void onShakePhone()
    {
        switch (stateIndex)
        {
            case 0:
                stateIndex += 1;
                showState();
                break;
            case 2:
                stateIndex += 1;
                showState();
                Completion();
                break;
        }
    }
    void onChongziDragEnd()
    {
        if (stateIndex == 1 && RectTransformExtensions.IsRectTransformOverlap(chongzi.rectTransform, yugou))
        {
            stateIndex += 1;
            showState();
            chongzi.gameObject.SetActive(false);
        }
        else
            chongzi.Return2OriginPos();
    }
    void showState()
    {
        for (int i = 1; i < states.Length; i++)
        {
            states[i].enabled = stateIndex == i;
        }
    }
    public override void Refresh()
    {
        base.Refresh();
        chongzi.Return2OriginPos();
        chongzi.gameObject.SetActive(true);
        stateIndex = 0;
        showState();
    }
}
