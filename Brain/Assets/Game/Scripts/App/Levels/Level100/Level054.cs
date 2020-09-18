using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level054 : LevelBasePage {
    public DragMove dragMove;
    private Vector3 _originPos;

    protected override void Start() {
        base.Start();
        _originPos = dragMove.transform.localPosition;
        dragMove.onDrag = () => {
            if (dragMove.transform.localPosition.x >= -390 && dragMove.transform.localPosition.x <= -373) {
                Completion();
            }
        };
    }

    public override void Refresh() {
        base.Refresh();
        dragMove.transform.localPosition = _originPos;
    }
}
