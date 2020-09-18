using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level005 : LevelBasePage {
    public List<Button> btnList;
    private List<Vector3> positons = new List<Vector3>();
    protected override void Start() {
        base.Start();
        for (int i = 0; i < btnList.Count; i++) {
            positons.Add(btnList[i].transform.localPosition);
            btnList[i].gameObject.GetComponent<DragMove>().onClick = () => {
                ShowErrorWithMousePosition();
            };
        }
        
        var size = btnList[0].GetComponent<RectTransform>().sizeDelta;
        var move = btnList[0].gameObject.GetComponent<DragMove>();
        move.onDragEnd = () => {
            float x = btnList[0].transform.localPosition.x;
            float y = btnList[0].transform.localPosition.y;
            if (Math.Abs(x) > RectTransformExtensions.ScreenWidth()/2 - size.x/2.0f || Math.Abs(y) > RectTransformExtensions.ScreenHeight()/2 - size.y/2.0f) {
                Completion();
            }
        };
    }

    public override void Refresh() {
        base.Refresh();
        for (int i = 0; i < btnList.Count; i++) {
            btnList[i].transform.localPosition = positons[i];
        }
    }
}
