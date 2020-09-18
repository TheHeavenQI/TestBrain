using UnityEngine;

public class Level046 : LevelBasePage
{

    public RectTransform[] areasTrans;
    public DragMove[] dragMoves;

    private RectTransform[] _dragMovesTrans;
    private Vector3[] _dragMovesPos;

    protected override void Start()
    {
        base.Start();

        _dragMovesTrans = new RectTransform[dragMoves.Length];
        _dragMovesPos = new Vector3[dragMoves.Length];
        for (int i = 0; i < dragMoves.Length; ++i)
        {
            dragMoves[i].onDragEnd = OnDragEnd;
            _dragMovesTrans[i] = dragMoves[i].transform as RectTransform;
            _dragMovesPos[i] = _dragMovesTrans[i].localPosition;
        }
    }

    protected void OnDragEnd()
    {
        foreach (RectTransform areas in areasTrans)
        {
            bool isOverlap = false;
            foreach (RectTransform dragMove in _dragMovesTrans)
            {
                if (RectTransformExtensions.IsRectTransformOverlap(dragMove, areas))
                {
                    isOverlap = true;
                    break;
                }
            }
            if (!isOverlap)
            {
                return;
            }
        }
        Completion();
    }

    public override void Refresh()
    {
        base.Refresh();
        for (int i = 0; i < dragMoves.Length; ++i)
        {
            _dragMovesTrans[i].localPosition = _dragMovesPos[i];
        }
    }

    public override void LanguageSwitch()
    {
        base.LanguageSwitch();
        levelQuestionText.text = levelQuestionText.text
            .Replace("bread", "<color=#00000000>bread</color>")
            .Replace("pão", "<color=#00000000>pão</color>")
            .Replace("pan", "<color=#00000000>pan</color>")
            .Replace("Brot", "<color=#00000000>Brot</color>")
            .Replace("パン", "<color=#00000000>パン</color>");
    }
}
