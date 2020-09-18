using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level203: LevelBasePage
{
    public RectTransform bingxiang;
    public RectTransform changjinglu;
    public DragMove boxGaizi;
    Vector3 boxGaiziDefPos;
    DragMove mDragMove;

    private TouchZoom _touchZoom = new TouchZoom();

    Vector3 changjingluDefPos;
    protected override void Start()
    {
        base.Start();
        changjingluDefPos = changjinglu.localPosition;
        mDragMove = changjinglu.GetComponent<DragMove>();
        mDragMove.onDragEnd = () =>
        {
            if(boxGaizi.gameObject.activeSelf  || (bingxiang.localScale.x < 1.9f || Vector3.Distance(changjinglu.localPosition, bingxiang.localPosition) > 100))
            {
                if (RectTransformExtensions.IsRectTransformOverlap(bingxiang, changjinglu))
                    ShowError();
            }
            else
                Completion();
        };
        boxGaiziDefPos = boxGaizi.transform.localPosition;
        boxGaizi.onDragEnd = () =>
        {
            if (Vector3.Distance(boxGaiziDefPos, boxGaizi.transform.localPosition) > 30)
            {
                boxGaizi.gameObject.SetActive(false);
            }
        };
    }
    private void Update()
    {
        if (isLevelComplete)
        {
            return;
        }
        TouchZoom.ZoomType zoomType = _touchZoom.Update();
        switch (zoomType)
        {
            case TouchZoom.ZoomType.Large:
                if (bingxiang.localScale.x < 2.5f)
                    bingxiang.localScale = bingxiang.localScale * 1.1f;
                break;
            case TouchZoom.ZoomType.Small:
                if (bingxiang.localScale.x > 0.5f)
                    bingxiang.localScale = bingxiang.localScale * 0.9f;
                break;
            case TouchZoom.ZoomType.None:
            default: break;
        }
    }
    public override void Refresh()
    {
        base.Refresh();
        boxGaizi.transform.localPosition = boxGaiziDefPos;
        boxGaizi.gameObject.SetActive(true);
        bingxiang.localScale = Vector3.one;
        changjinglu.localPosition = changjingluDefPos;
    }
}
