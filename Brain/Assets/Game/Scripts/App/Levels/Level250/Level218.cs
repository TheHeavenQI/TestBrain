
using System;
using DG.Tweening;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.UI;

public class Level218 : LevelBasePage {
    public Button shotButton;
    public Sprite qiqiu1;
    public Sprite qiqiu2;
    public Balloon_ qiu;
    public DragMove biao;
    private bool _isShot;
    protected override void Start() {
        base.Start();
        shotButton.onClick.AddListener(() => { _isShot = true; });
    }

    private void Update() {
        if (_isShot && !isLevelComplete) {
            float diff = Vector3.Distance(biao.transform.localPosition, qiu.transform.localPosition);
            if (diff < 150) {
                Image im = qiu.GetComponent<Image>();
                im.sprite = qiqiu2;
                im.SetNativeSize();
                biao.gameObject.SetActive(false);
                Completion();
                return;
            }
            biao.transform.localPosition += new Vector3(375*Time.deltaTime,0,0);
            if (biao.transform.localPosition.x > 375) {
                _isShot = false;
                ShowError();
                Refresh();
            }
        }
    }

    public override void Refresh() {
        base.Refresh();
        _isShot = false;
        biao.Return2OriginPos();
        qiu.Refresh();
        Image im = qiu.GetComponent<Image>();
        im.sprite = qiqiu1;
        im.SetNativeSize();
        biao.gameObject.SetActive(true);
    }
}
