using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragOutSceneLevel : LevelBasePage {

    public List<DragMove> dragMoveList;
    private List<Vector3> positons = new List<Vector3>();

    protected override void Start() {
        base.Start();
        for (int i = 0; i < dragMoveList.Count; i++) {
            positons.Add(dragMoveList[i].transform.localPosition);
            dragMoveList[i].onClick = () => {
                ShowErrorWithMousePosition();
            };
        }

        var size = dragMoveList[0].GetComponent<RectTransform>().sizeDelta;
        var move = dragMoveList[0];
        move.onDragEnd = () => {
            float x = dragMoveList[0].transform.localPosition.x;
            float y = dragMoveList[0].transform.localPosition.y;
            if (Math.Abs(x) >= RectTransformExtensions.ScreenWidth() * 0.5f || Math.Abs(y) >= RectTransformExtensions.ScreenHeight() * 0.5f) {
                Completion();
            }
        };
    }

    public override void Refresh() {
        base.Refresh();
        for (int i = 0; i < dragMoveList.Count; i++) {
            dragMoveList[i].transform.localPosition = positons[i];
        }
    }
}
