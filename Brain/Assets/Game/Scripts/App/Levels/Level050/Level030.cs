using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Level030 : LevelBasePage
{
    public DragMoveEventTrigger[] dragMoves;
    public RectTransform sign;

    public Vector3 endOffset;
    public RectTransform car;

    protected override void Start()
    {
        base.Start();
        for (int i = 0; i < dragMoves.Length; ++i)
        {
            int j = i;
            dragMoves[j].onEndDrag = (d) => OnDragEnd(dragMoves[j]);
        }

        car.gameObject.SetActive(false);
    }

    private void OnDragEnd(DragMoveEventTrigger moveItem)
    {

        if (RectTransformExtensions.IsRectTransformOverlap(moveItem.rectTransform, sign))
        {
            moveItem.transform.DOLocalMove(sign.localPosition + endOffset, 0.5f).OnComplete(() => {
                Completion();
            });
        }
        else
        {
            ShowError();
            moveItem.Return2OriginPos(0.3f);
        }
    }

    public override void Refresh()
    {
        base.Refresh();
        foreach (DragMoveEventTrigger dm in dragMoves)
        {
            dm.Return2OriginPos();
        }
    }

    protected override void OnCompletion()
    {
        base.OnCompletion();
        car.gameObject.SetActive(true);
        car.DOLocalMoveX(600, delayComplete)
            .From()
            .SetEase(Ease.OutQuad);
    }

    public override void LanguageSwitch()
    {
        base.LanguageSwitch();
        levelQuestionText.text = levelQuestionText.text
                                .Replace("STOP", "<color=#00000000>STOP</color>")
                                .Replace("stop", "<color=#00000000>STOP</color>")
                                .Replace("Stopp-Wort", "<color=#00000000>Stopp-Wort</color>")
                                .Replace("Stoppschild", "<color=#00000000>Stoppschild</color>")
                                .Replace("停止", "<color=#00000000>停止</color>");
    }
}
