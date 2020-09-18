
using System;
using UnityEngine;
using UnityEngine.UI;

public class Level099 : LevelBasePage {
    public Button correct;
    public GameObject tipImage;
    public Sprite tipSprite;
    protected override void Start() {
        base.Start();
        tipImage.SetActive(false);
        correct.onClick.AddListener(() => {
            tipImage.SetActive(true);
            Completion();
        });
        Global.tipSprite = tipSprite;
    }
    
    public override void Refresh() {
        base.Refresh();
        tipImage.SetActive(false);
    }
}
