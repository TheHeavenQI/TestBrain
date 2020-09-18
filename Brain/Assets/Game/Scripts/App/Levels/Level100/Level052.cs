using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using BaseFramework;

using DG.Tweening;

public class Level052 : LevelBasePage {

    public float disX = 750;
    public float aimMin;
    public float aimMax;

    public LimitDragMoveEventTrigger rightCar;
    public LimitDragMoveEventTrigger leftCar;

    protected override void Start() {
        base.Start();

        rightCar.onDrag += OnRightDrag;

        leftCar.onDrag += OnLeftDrag;
        leftCar.onEndDrag += OnLeftDragEnd;

        leftCar.rectTransform.DOAnchorPosY(leftCar.rectTransform.anchoredPosition.y - 12, 0.5f).SetLoops(-1, LoopType.Yoyo);
        
        rightCar.rectTransform.DOAnchorPosY(rightCar.rectTransform.anchoredPosition.y - 12, 0.5f).SetLoops(-1, LoopType.Yoyo);
        
    }

    private void OnRightDrag(PointerEventData data) {
        leftCar.transform.localPosition = leftCar.transform.localPosition.NewX(rightCar.transform.localPosition.x - disX);
    }

    private void OnLeftDrag(PointerEventData data) {
        rightCar.transform.localPosition = rightCar.transform.localPosition.NewX(rightCar.transform.localPosition.x + disX);
    }

    private void OnLeftDragEnd(PointerEventData data) {
        if (leftCar.transform.localPosition.x <= aimMax && leftCar.transform.localPosition.x >= aimMin) {
            Completion();
        }
    }

    public override void Refresh() {
        base.Refresh();
        rightCar.Return2OriginPos();
        leftCar.Return2OriginPos();
    }
}
