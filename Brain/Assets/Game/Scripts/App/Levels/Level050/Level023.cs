using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level023 : CorrectErrorButtonsLevel {

    public override void LanguageSwitch() {
        base.LanguageSwitch();
        levelQuestionText.text = levelQuestionText.text.Replace("🐰", "<color=#00000000>🐰</color>");
    }
}
