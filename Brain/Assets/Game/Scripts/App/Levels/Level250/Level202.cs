
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class Level202 : LevelBasePage
{
    public Image[] food;
    public Image boxCommon;
    public Image boxFire;

    public RectTransform red;
    public RectTransform mark;
    public RectTransform target;
    Tweener tween;
    float redWidth;
    protected override void Start()
    {
        base.Start();
        redWidth = red.sizeDelta.x;
        showFood(-1);
        showTempretureAni();
    }
    bool inShowState = false;
    public void ClickBox()
    {
        if (inShowState)
            return;
        if (RectTransformExtensions.IsRectTransformOverlap(mark.localPosition + red.localPosition, mark.sizeDelta, target.localPosition, target.sizeDelta))
        {
            showFood(1);
            After(() =>
            {
                Completion();
            }, 0.5f);
        }
        else
        {
            if (mark.localPosition.y + red.localPosition.y > target.localPosition.y)
            {
                showFood(2);
                boxFire.enabled = true;
                boxCommon.enabled = false;
            }
            else
            {
                showFood(0);
            }
            ShowError();
            After(() => {           
                Refresh();
            }, 1.5f);
        }
        if (tween != null)
            tween.Pause();
        inShowState = true;
    }
    void showFood(int index)
    {
        for (int i = 0; i < food.Length; i++)
        {
            if (i == index)
                food[i].enabled = true;
            else
                food[i].enabled = false;
        }
    }
    public override void Refresh()
    {
        base.Refresh();
        showTempretureAni();
        showFood(-1);
        boxCommon.enabled = true;
        boxFire.enabled = false;
    }
    void showTempretureAni()
    {
        if (tween != null)
            tween.Kill();
        inShowState = false;
        red.sizeDelta = new Vector2(redWidth,20);
        After(() =>
        {
            tween = red.DOSizeDelta(new Vector2(redWidth, 108), 0.5f);
            tween.onComplete = () =>
            {
                tween = red.DOSizeDelta(new Vector2(redWidth, 20), 1f);
                tween.onComplete = () =>
                {
                    tween = red.DOSizeDelta(new Vector2(redWidth, 135), 0.5f);
                    tween.onComplete = () =>
                    {
                        tween = red.DOSizeDelta(new Vector2(redWidth, 20), 1f);
                        tween.onComplete = () =>
                        {
                            tween = red.DOSizeDelta(new Vector2(redWidth, 232), 0.75f);
                        };
                    };
                };
            };
        }, 1f);
    }


}
