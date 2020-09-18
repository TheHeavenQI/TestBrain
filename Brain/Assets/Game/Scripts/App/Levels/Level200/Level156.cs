using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level156 : CorrectErrorButtonsLevel {

    private Shake _shake;

    protected override void Start() {
        base.Start();

        correctButton.gameObject.SetActive(false);

        _shake = GetComponent<Shake>();
        _shake.shakeAction += () => correctButton.gameObject.SetActive(true);
    }
    public override void Refresh()
    {
        base.Refresh();
        correctButton.gameObject.SetActive(false);
    }
}
