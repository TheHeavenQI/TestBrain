using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class Level184 : LevelBasePage {

    public DragMoveEventTrigger[] dragMoves;
    public RectTransform correctPoint;
    public Image mHeart;
    protected override void Start() {
        base.Start();
        dragMoves[0].onEndDrag += (d) => OnEndDrag();
        dragMoves[1].onEndDrag += (d) => OnEndDrag();
    }

    public override void Refresh() {
        base.Refresh();
        mHeart.color = new Color(1,1,1,1);
        mHeart.enabled = false;
        foreach (DragMoveEventTrigger dragMove in dragMoves) {
            dragMove.Return2OriginPos();
        }
    }

    private void OnEndDrag() {
        float offsetX = dragMoves[1].rectTransform.localPosition.x - dragMoves[0].rectTransform.localPosition.x;
        float offsetY = dragMoves[1].rectTransform.localPosition.y - dragMoves[0].rectTransform.localPosition.y;

        if (Mathf.Abs(offsetY) <= 30) {
            if (offsetX >= 151 && offsetX <= 219) {
                mHeart.transform.position = correctPoint.position;
                mHeart.enabled = true;
                mHeart.DOFade(0,1.2f);
                After(()=> {
                    Completion(correctPoint.position);
                }, 1f);
            }
        }
    }
}
