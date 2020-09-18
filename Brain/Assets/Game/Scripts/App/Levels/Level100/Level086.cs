using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class Level086 : LevelBasePage
{
    public DragMoveEventTrigger[] dragMoves;
    public RectTransform top;

    public Vector3 endOffset;
    public Image elevatorOpenImg;

    protected override void Start()
    {
        base.Start();
        for (int i = 0; i < dragMoves.Length; ++i)
        {
            int j = i;
            dragMoves[j].onEndDrag = (d) => OnDragEnd(dragMoves[j]);
        }

        elevatorOpenImg.enabled = false;
    }

    private void OnDragEnd(DragMoveEventTrigger moveItem)
    {

        if (RectTransformExtensions.IsRectTransformOverlap(moveItem.rectTransform, top))
        {
            moveItem.transform.DOLocalMove(top.localPosition + endOffset, 0.5f).OnComplete(() => {
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
        elevatorOpenImg.enabled = true;
    }

    public override void LanguageSwitch()
    {
        base.LanguageSwitch();
        levelQuestionText.text = levelQuestionText.text
                                .Replace("elevator", "<color=#00000000>elevator</color>")
                                .Replace("elevador", "<color=#00000000>elevador</color>")
                                .Replace("Aufzug", "<color=#00000000>Aufzug</color>")
                                .Replace("¥¨¥ì¥Ù©`¥¿©`", "<color=#00000000>¥¨¥ì¥Ù©`¥¿©`</color>");
    }
}
