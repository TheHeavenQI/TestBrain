using UnityEngine;

public class Level238 : LevelBasePage
{
    public RectTransform hand;
    public DragMoveEventTrigger[] dragMoves;
    public DragMoveEventTrigger[] beans;

    protected override void Start()
    {
        base.Start();
        for (int i = 0; i < dragMoves.Length; ++i)
        {
            int j = i;
            dragMoves[j].onEndDrag = (d) => {
                if (hand.IsRectTransformOverlap(dragMoves[j].rectTransform))
                {
                    Completion();
                }
            };
        }
    }

    public override void Refresh()
    {
        base.Refresh();
        foreach (var dm in dragMoves)
        {
            dm.Return2OriginPos();
        }
        foreach (var dm in beans)
        {
            dm.Return2OriginPos();
        }
    }

    public override void LanguageSwitch()
    {
        base.LanguageSwitch();
        levelQuestionText.text = levelQuestionText.text.Replace("4", "<color=#00000000>4</color>");
    }
}
