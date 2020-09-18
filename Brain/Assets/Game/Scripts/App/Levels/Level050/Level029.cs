using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level029 : LevelBasePage {
    public List<Button> List;
    protected override void Start() {
        base.Start();
        for (int i = 0; i < List.Count; i++) {
            var k = i;
            List[i].onClick.AddListener(() => {
                if (k == 1) {
                    CompletionWithMousePosition();
                }
                else {
                    ShowErrorWithMousePosition();
                }
            });
        }
    }
}
