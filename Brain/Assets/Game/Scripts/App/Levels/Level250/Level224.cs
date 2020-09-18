
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level224 : LevelBasePage {
    public List<DragMove> dots;
    public List<Button> qiqiu;
    protected override void Start() {
        base.Start();
        for (int i = 0; i < qiqiu.Count; i++) {
            var btn = qiqiu[i];
            btn.onClick.AddListener(() => { Judge(btn); });
        }
    }

    private void Judge(Button btn) {
        int count = 0;
        for (int i = 0; i < dots.Count; i++) {
            var loc1 = dots[i].transform.localPosition;
            var loc2 = btn.transform.localPosition;
            if (Vector2.Distance(loc1, loc2) < 150) {
                count++;
            }
        }

        if (count >= 3) {
            CompletionWithMousePosition();
        }
        else {
            ShowErrorWithMousePosition();
        }
    }
}
