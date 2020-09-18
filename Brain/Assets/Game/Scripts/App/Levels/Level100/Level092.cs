using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level092 : CorrectErrorButtonsLevel {
    public Image centerImg;

    protected override void OnCompletion() {
        base.OnCompletion();
        centerImg.enabled = false;
    }
}
