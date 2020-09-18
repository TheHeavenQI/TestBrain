using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchZoom {

    public enum ZoomType {
        None,
        Large,
        Small
    }

    private Vector2 _oldPos1;
    private Vector2 _oldPos2;

    /// <summary>
    /// ����MonoBehaviour.Update()�е���
    /// </summary>
    /// <returns></returns>
    public ZoomType Update() {

#if UNITY_EDITOR
        float zoomValue = Input.GetAxis("Mouse ScrollWheel");
        return zoomValue == 0 ? ZoomType.None : zoomValue < 0 ? ZoomType.Small : ZoomType.Large;
#endif

        if (Input.touchCount == 2) {
            if (Input.GetTouch(0).phase == TouchPhase.Moved || Input.GetTouch(1).phase == TouchPhase.Moved) {
                Vector2 newPos1 = Input.GetTouch(0).position;
                Vector2 newPos2 = Input.GetTouch(1).position;
                if (IsEnlarge(_oldPos1, _oldPos2, newPos1, newPos2)) {
                    _oldPos1 = newPos1;
                    _oldPos2 = newPos2;
                    return ZoomType.Large;
                } else {
                    _oldPos1 = newPos1;
                    _oldPos2 = newPos2;
                    return ZoomType.Small;
                }
            }
        }
        return ZoomType.None;
    }

    private bool IsEnlarge(Vector2 oP1, Vector2 oP2, Vector2 nP1, Vector2 nP2) {
        if (Vector2.Distance(oP1, oP2) < Vector2.Distance(nP1, nP2)) {
            return true;
        } else {
            return false;
        }
    }
}
