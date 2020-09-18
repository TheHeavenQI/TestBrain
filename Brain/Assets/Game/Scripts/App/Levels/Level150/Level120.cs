using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Level120 : LevelBasePage {

    public DragMove pointDM;
    public Image pointImg;

    public Button clockBtn;
    private Tweener _shakeTween;

    protected override void Start() {
        base.Start();

        _shakeTween = clockBtn.transform.DOShakeRotation(0.1f, new Vector3(0, 0, 40), 35, 90, false).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
        clockBtn.onClick.AddListener(() => {
            _shakeTween.Pause();
            clockBtn.transform.localEulerAngles = Vector3.zero;
            pointImg.raycastTarget = true;

            After(() => {
                if (pointDM.transform.position == pointDM.GetOriginPos() && !pointDM.isDraging) {
                    _shakeTween.Restart();
                    pointImg.raycastTarget = false;
                }
            }, 3);
        });

        pointDM.onDragEnd = () => {
            if (Vector3.Distance(pointDM.transform.position, pointDM.GetOriginPos()) > 1) {
                CompletionWithMousePosition();
            }
        };
    }

    public override void Refresh() {
        base.Refresh();
        clockBtn.transform.localEulerAngles = Vector3.zero;
        pointDM.Return2OriginPos();
        pointImg.raycastTarget = false;
        _shakeTween.Restart();
    }
}
