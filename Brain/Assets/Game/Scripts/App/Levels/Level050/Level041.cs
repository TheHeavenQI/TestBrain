using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level041 : LevelBasePage {
    public List<Button> allButton;
    private Vector3 _loc;
    protected override void Start() {
        base.Start();
        
        _loc = levelQuestionText.transform.localPosition;
        for (int i = 0; i < allButton.Count; i++) {
            var k = i;
            allButton[i].onClick.AddListener((UnityEngine.Events.UnityAction)(() => {
                if (k == 0) {
                    if (allButton[0].transform.localPosition.y > base.levelQuestionText.transform.localPosition.y) {
                        CompletionWithMousePosition();
                        return;
                    }
                }
                ShowErrorWithMousePosition();
            }));
        }

    }

    public override void Refresh() {
        base.Refresh();
        levelQuestionText.transform.localPosition = _loc;
    }
}
