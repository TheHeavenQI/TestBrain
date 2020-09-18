using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level039 : LevelBasePage {

    public Button textBtn;
    public Button jButton;
    protected override void Start() {
        base.Start();
        jButton.onClick.AddListener(() => {
            if (levelIndex == GuidenceManager.Instance().guideCount)
            {
                GuidenceManager.Instance().guideCount += 1;
            }
            CompletionWithMousePosition();
        });
        textBtn.onClick.AddListener(() => {
            ShowErrorWithMousePosition();
        });
    }
}
