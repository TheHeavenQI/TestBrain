using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Level115 : LevelBasePage {
    public Transform pos;
    public Transform pos_1;
    public DragMove dragMove;
    protected override void Start() {
        base.Start();
        dragMove.onDragEnd = () => {
            if (Vector3.Distance(dragMove.transform.localPosition, pos.transform.localPosition) < 20) {
                dragMove.transform.DOLocalMove(pos.transform.localPosition, 0.5f).OnComplete(() => {
                    Completion();
                });
            }
            else if (Vector3.Distance(dragMove.transform.localPosition, pos_1.transform.localPosition) < 20)
            {
                dragMove.transform.DOLocalMove(pos_1.transform.localPosition, 0.5f).OnComplete(() => {
                    Completion();
                });
            }
            else {
                dragMove.Return2OriginPos(0.5f);
            }
        };
    }

    public override void Refresh() {
        base.Refresh();
        dragMove.Return2OriginPos();
    }
}
