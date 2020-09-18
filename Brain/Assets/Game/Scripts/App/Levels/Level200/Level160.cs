using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Level160 : LevelBasePage {

    public DragMoveEventTrigger seedDM;
    public RectTransform treeTrans;

    protected override void Start() {
        base.Start();

        float bottom = -(RectTransformExtensions.ScreenHeight() * 0.5f - 10);
        seedDM.onEndDrag += (d) => {
            if (seedDM.rectTransform.anchoredPosition.y <= bottom) {
                seedDM.gameObject.SetActive(false);
                treeTrans.anchoredPosition = new Vector2(seedDM.rectTransform.anchoredPosition.x, treeTrans.anchoredPosition.y);
                treeTrans.DOAnchorPosY(0, 1).OnComplete(() => Completion());
            }
        };
    }

    public override void Refresh() {
        base.Refresh();
       
        DOTween.Kill(treeTrans);
        treeTrans.anchoredPosition = new Vector2(0, -treeTrans.sizeDelta.y);
        
        seedDM.gameObject.SetActive(true);
        seedDM.RefreshOriginPos();
    }
}
