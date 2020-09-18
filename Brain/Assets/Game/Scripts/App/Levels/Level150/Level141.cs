using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level141 : LevelBasePage
{

    public Transform pageBg;

    private TouchZoom _touchZoom = new TouchZoom();

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
                if (pageBg.localScale.x <2)
                    pageBg.localScale = pageBg.localScale * 1.1f;
                break;
            case TouchZoom.ZoomType.Small:
                if (pageBg.localScale.x >0.5f)
                    pageBg.localScale = pageBg.localScale * 0.9f;
                break;
            case TouchZoom.ZoomType.None:
            default: break;
        }
    }

    public override void Refresh()
    {
        base.Refresh();
        pageBg.localScale = Vector3.one;
    }
    public void clickNormalItem()
    {
        ShowError();
    }
    public void ClickBigChicken()
    {
        Completion();
    }
}
