using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level116 : LevelBasePage {

    public Image bottleImg;
    public Button bottleOpenBtn;

    private Shake _shake;

    protected override void Start() {
        base.Start();

        _shake = this.GetComponent<Shake>();
        _shake.shakeAction = () => {
            bottleImg.gameObject.SetActive(false);
            bottleOpenBtn.gameObject.SetActive(true);
        };

        bottleOpenBtn.onClick.AddListener(CompletionWithMousePosition);
    }

    public override void Refresh() {
        base.Refresh();

        bottleImg.gameObject.SetActive(true);
        bottleOpenBtn.gameObject.SetActive(false);
    }
}
