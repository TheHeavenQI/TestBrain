using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class Level190 : LevelBasePage {
    public Button dartBtn;
    public Image balloon;
    public Image breakBalloon;

    private bool _isFlying;
    private Vector3 _dartOriginPos;

    private DeviceOrientation _orient1;
    private DeviceOrientation _orient2;

    protected override void Start() {
        base.Start();

        _dartOriginPos = dartBtn.transform.position;

        dartBtn.onClick.AddListener(() => {
            if (_isFlying) {
                return;
            }
            _orient1 = Input.deviceOrientation;
            dartBtn.transform.DOLocalMoveX(500, 0.7f)
                .SetEase(Ease.Linear)
                .OnComplete(() => {
                    _isFlying = false;
                    _orient2 = Input.deviceOrientation;
                    if (_orient1 == DeviceOrientation.PortraitUpsideDown
                        && _orient2 == DeviceOrientation.PortraitUpsideDown) {
                        Completion();
                    } else {
                        ShowError();
                        After(Refresh, 0.5f);
                    }
                });

            After(() => {
                balloon.gameObject.SetActive(false);
                breakBalloon.gameObject.SetActive(true);
            }, 0.1f);
        });
    }

    public override void Refresh() {
        base.Refresh();

        StopAllCoroutines();

        _isFlying = false;
        DOTween.Kill(dartBtn.transform);
        dartBtn.transform.position = _dartOriginPos;

        balloon.gameObject.SetActive(true);
        breakBalloon.gameObject.SetActive(false);
    }
}
