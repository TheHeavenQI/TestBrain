using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level053 : LevelBasePage {
    public Transform chuanTransform;

    private TouchZoom _touchZoom = new TouchZoom();

    private void Update() {
        if (isLevelComplete) {
            return;
        }

        TouchZoom.ZoomType zoomType = _touchZoom.Update();
        switch (zoomType) {
            case TouchZoom.ZoomType.Large:
                chuanTransform.localScale = chuanTransform.localScale * 1.1f;
                if (chuanTransform.localScale.x > 2.5f) {
                    Completion();
                }
                break;
            case TouchZoom.ZoomType.Small:
                chuanTransform.localScale = chuanTransform.localScale * 0.9f;
                break;
            case TouchZoom.ZoomType.None:
            default: break;
        }
    }

    public override void Refresh() {
        chuanTransform.localScale = Vector3.one;
    }
}
