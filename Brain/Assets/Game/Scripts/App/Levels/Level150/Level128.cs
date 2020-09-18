using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level128 : LevelBasePage {
    public Transform blank;

    private TouchZoom _touchZoom = new TouchZoom();

    private void Update() {
        if (isLevelComplete) {
            return;
        }

        TouchZoom.ZoomType zoomType = _touchZoom.Update();
        switch (zoomType) {
            case TouchZoom.ZoomType.Large:
                blank.localScale = blank.localScale * 1.1f;
                if (blank.localScale.x > 2.4f) {
                    Completion();
                }
                break;
            case TouchZoom.ZoomType.Small:
                blank.localScale = blank.localScale * 0.9f;
                break;
            case TouchZoom.ZoomType.None:
            default: break;
        }
    }

    public override void Refresh() {
        base.Refresh();
        blank.localScale = Vector3.one;
    }
}
