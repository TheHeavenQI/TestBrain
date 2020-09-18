using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinishAllLevelPopUp : BasePopUp {
    public Button btn;
    public override void Start() {
        base.Start();
        btn.onClick.AddListener(() => {
            Hide();
        });
    }
}
