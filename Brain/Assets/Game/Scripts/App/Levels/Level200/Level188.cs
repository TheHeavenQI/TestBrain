using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level188 : CorrectErrorButtonsLevel {
    private Shake _shake;

    protected override void Start() {
        base.Start();
        _shake = this.GetComponent<Shake>();
        _shake.shakeAction += () => {
            correctButton.gameObject.SetActive(true);
        };
    }
}
