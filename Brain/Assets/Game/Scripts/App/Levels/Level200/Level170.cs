using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Level170 : LevelBasePage {

    public DragMoveEventTrigger dragMove;
    public RectTransform target;

    protected override void Start() {
        base.Start();

        dragMove.onEndDrag += (d) => {
            if (Vector3.Distance(dragMove.transform.localPosition, target.localPosition) <= 5) {
                dragMove.transform.DOLocalMove(target.localPosition, 0.1f).OnComplete(() => {
                    Completion();
                });
            }
        };
    }

    public override void Refresh() {
        base.Refresh();
        dragMove.Return2OriginPos();
    }
}
