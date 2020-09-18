using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragNearLevel : LevelBasePage {

    public DragMove dragMove1;
    public bool dragMove1EnableDrag = true;
    public DragMove dragMove2;
    public bool dragMove2EnableDrag = true;

    public Vector3 endOffset;

    protected override void Start() {
        base.Start();

        dragMove1.enabelDrag = dragMove1EnableDrag;
        dragMove2.enabelDrag = dragMove2EnableDrag;

        dragMove1.onDragEnd = () => {
            OnDragEnd(dragMove1);
        };
        dragMove2.onDragEnd = () => {
            OnDragEnd(dragMove2);
        };
    }


    public override void Refresh() {
        base.Refresh();
        dragMove1.Return2OriginPos();
        dragMove2.Return2OriginPos();
    }


    private void OnDragEnd(DragMove moveItem) {

        if (RectTransformExtensions.IsRectTransformOverlap(dragMove1.rectTransform, dragMove2.rectTransform)) {
            DragMove other = moveItem == dragMove1 ? dragMove2 : dragMove1;
            moveItem.transform.DOLocalMove(other.transform.localPosition + endOffset, 0.5f).OnComplete(() => {
                Completion();
            });
        } else {
            ShowError();
            Refresh();
        }
    }
}
