using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level057 : LevelBasePage {
    public DragMove dogDragMove;
    protected override void Awake() {
        base.Awake();
        dogDragMove.offsetX = -dogDragMove.rectTransform.sizeDelta.x*0.5f;
        dogDragMove.offsetY = -dogDragMove.rectTransform.sizeDelta.y*0.5f;
        var rect = transform.GetComponent<RectTransform>().rect;
        dogDragMove.onDragEnd = () => {
            var pos = dogDragMove.transform.localPosition;
            if (Math.Abs(pos.x) > rect.width / 2.0 || Math.Abs(pos.y) > rect.height / 2.0) {
                Completion();
            }
//            RectTransformExtensions.IsRectTransformOverlap()
        };
    }

    public override void Refresh() {
        base.Refresh();
        dogDragMove.Return2OriginPos();
    }
}
