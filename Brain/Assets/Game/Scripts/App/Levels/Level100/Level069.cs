using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Level069 : LevelBasePage
{
    public DragMoveEventTrigger[] dragMoves;
    public RectTransform frame;

    public Vector3 endOffset;

    public DragMove xiangpian;


    protected override void Start()
    {
        base.Start();
        for (int i = 0; i < dragMoves.Length; ++i)
        {
            int j = i;
            dragMoves[j].onEndDrag = (d) => OnDragEnd(dragMoves[j]);
        }

        xiangpian.onDragEnd = () => { xiangpian.Return2OriginPos(0.5f); };
    }

    private void OnDragEnd(DragMoveEventTrigger moveItem)
    {

        if (RectTransformExtensions.IsRectTransformOverlap(moveItem.rectTransform, frame))
        {
            moveItem.transform.DOLocalMove(frame.localPosition + endOffset, 0.5f).OnComplete(() => {
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

    public override void LanguageSwitch() {
        base.LanguageSwitch();
        levelQuestionText.text = levelQuestionText.text
                .Replace("photo", "<color=#00000000>photo</color>")
                .Replace("foto", "<color=#00000000>foto</color>")
                .Replace("Foto", "<color=#00000000>Foto</color>")
                .Replace("写真", "<color=#00000000>写真</color>");
    }
}
