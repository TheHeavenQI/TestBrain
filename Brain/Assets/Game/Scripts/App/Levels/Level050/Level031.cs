using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level031 : LevelBasePage {
    public List<Button> list;
    protected override void Start() {
        base.Start();
        for (int i = 0; i < list.Count; i++) {
            var btn = list[i];
            btn.onClick.AddListener(() => {
                if (btn.name == "panzi") {
                    CompletionWithMousePosition();
                }
                else {
                    ShowErrorWithMousePosition();
                }
            });
        }
    }
}
