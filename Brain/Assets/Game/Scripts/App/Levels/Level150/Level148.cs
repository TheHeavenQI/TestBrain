using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Level148 : LevelBasePage {

    public DragMoveEventTrigger[] dragMoves;
    public RectTransform boxTrans;
    public RectTransform boxEntranceTrans;
    /// <summary>
    /// 拖入箱子的物体个数
    /// </summary>
    private int _collectCount;

    protected override void Start() {
        base.Start();

        for (int i = 0; i < dragMoves.Length; ++i) {
            int k = i;
            DragMoveEventTrigger dragMove = dragMoves[k];
            dragMove.onEndDrag += (d) => {
                if (RectTransformExtensions.IsRectTransformOverlap(boxTrans, dragMove.rectTransform)) {
                    dragMove.enableDragMove = false;
                    dragMove.rectTransform.DOMove(boxEntranceTrans.transform.position, 0.5f);
                    dragMove.rectTransform.DOScale(0, 0.5f).OnComplete(() => {
                        ++_collectCount;
                        if (_collectCount == dragMoves.Length) {
                            Completion();
                        }
                    });
                }
            };
        }
    }

    public override void Refresh() {
        base.Refresh();
        foreach (DragMoveEventTrigger dragMove in dragMoves) {
            DOTween.Kill(dragMove);
            dragMove.transform.localScale = Vector3.one;
            dragMove.Return2OriginPos();
            dragMove.enableDragMove = true;
        }
        _collectCount = 0;
    }
}
