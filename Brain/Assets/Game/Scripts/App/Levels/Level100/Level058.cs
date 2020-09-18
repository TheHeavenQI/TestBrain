using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level058 : LevelBasePage
{
    public RectTransform[] boxs;
    public DragMove[] correctNums;
    public RectTransform pointBox;
    public DragMove[] points;
    public DragMove[] dragMoves;

    protected override void Start()
    {
        base.Start();

        foreach (DragMove dragMove in correctNums)
        {
            dragMove.onDragEnd = OnCorrectNumDragEnd;
        }
        foreach (DragMove dragMove in points)
        {
            dragMove.onDragEnd = OnCorrectNumDragEnd;
        }
    }

    public override void Refresh()
    {
        base.Refresh();

        foreach (DragMove dragMove in dragMoves)
        {
            dragMove.Return2OriginPos();
        }
    }

    protected void OnCorrectNumDragEnd()
    {
        for (int i = 0; i < boxs.Length; ++i)
        {
            if (!RectTransformExtensions.Overlaps(boxs[i], correctNums[i].rectTransform))
            {
                return;
            }
        }

        bool isPointBoxEnter = false;
        foreach (DragMove dragMove in points)
        {
            if (RectTransformExtensions.Overlaps(pointBox, dragMove.rectTransform))
            {
                isPointBoxEnter = true;
                break;
            }
        }

        if (isPointBoxEnter)
        {
            Completion();
        }
    }

    public override void LanguageSwitch()
    {
        base.LanguageSwitch();
        levelQuestionText.text = levelQuestionText.text
                                 .Replace(".", "<color=#00000000>.</color>")
                                 .Replace("¡£", "<color=#00000000>¡£</color>");
    }
}
