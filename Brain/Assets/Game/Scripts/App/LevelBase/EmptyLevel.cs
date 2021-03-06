﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EmptyLevel : LevelBasePage {
    
    protected override void Start() {
        base.Start();
        transform.Find("Skip").GetComponent<Button>().onClick.AddListener(() => {
            Completion();
        });
    }
}
