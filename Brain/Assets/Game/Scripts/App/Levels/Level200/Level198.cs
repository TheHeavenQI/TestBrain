using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level198 : LevelBasePage {

    public LimitDragMoveEventTrigger dragMove;
    public GameObject nbaInfoGo;
    public Button plugOffBtn;
    public GameObject plugOnGo;
    public Image signalImg;
    public Sprite[] signalSprites;
    public Button switchBtn;

    private bool _isPowerOn;

    protected override void Start() {
        base.Start();

        signalImg.sprite = signalSprites[0];
        plugOffBtn.gameObject.SetActive(true);
        plugOnGo.SetActive(false);
        nbaInfoGo.SetActive(false);

        plugOffBtn.onClick.AddListener(() => {
            _isPowerOn = true;
            plugOffBtn.gameObject.SetActive(false);
            plugOnGo.SetActive(true);
            signalImg.sprite = signalSprites[1];
        });

        switchBtn.onClick.AddListener(() => {
            if (!_isPowerOn) {
                return;
            }
            signalImg.sprite = signalSprites[2];
            nbaInfoGo.SetActive(true);
            CompletionWithMousePosition();
        });
    }

    public override void Refresh() {
        base.Refresh();

        dragMove.Return2OriginPos();
        signalImg.sprite = signalSprites[0];
        plugOffBtn.gameObject.SetActive(true);
        plugOnGo.SetActive(false);
        nbaInfoGo.SetActive(false);
        _isPowerOn = false;
    }
}
