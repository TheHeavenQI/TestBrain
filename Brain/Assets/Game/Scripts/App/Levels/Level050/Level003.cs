using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level003 : LevelBasePage {
   public Button btn;
   public Button errorBtn;
   protected override void Start() {
       base.Start();
      btn.onClick.AddListener(() => {
            CompletionWithMousePosition();
      });
      errorBtn.onClick.AddListener(() => {
            ShowErrorWithMousePosition();
      });
   }
}
