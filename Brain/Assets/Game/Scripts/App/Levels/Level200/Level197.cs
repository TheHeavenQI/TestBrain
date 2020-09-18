
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level197 : LevelBasePage {
    
    public List<Button> list;
    public List<bool> follow = new List<bool>();
    public List<Vector3> posList = new List<Vector3>();
    public EventCallBack drag;
    protected override void Start() {
        base.Start();
        
        for (int i = 0; i < list.Count; i++) {
            var a = list[i];
            posList.Add(a.transform.position);
            follow.Add(false);
            a.onClick.AddListener(() => {
                ShowErrorWithMousePosition();
                Refresh();
            });
        }
        
        drag.onDragDragingWithOutOffset = (pos) => {
            int count = 0;
            for (int i = 0; i < list.Count; i++) {
                var a = list[i];
                if (Vector2.Distance(a.transform.position,pos) < 0.5f) {
                    follow[i] = true;
                }
                if (follow[i]) {
                    a.transform.position = pos;
                    count += 1;
                }

                if (count == list.Count) {
                    Completion();
                }
            }
        };
        drag.onDragEnd = () => {
            for (int i = 0; i < list.Count; i++) {
                follow[i] = false;
            }
        };
    }

    public override void Refresh() {
        base.Refresh();
        for (int i = 0; i < list.Count; i++) {
            var a = list[i];
            follow[i] = false;
            a.transform.position = posList[i];
        }
    }
}