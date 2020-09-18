using DG.Tweening;
using UnityEngine;

public class Level028 : CorrectErrorButtonsLevel
{
    public RectTransform head;

    protected override void OnCompletion()
    {
        base.OnCompletion();

        Tweener t = head.DOAnchorPos3DY(500, 0.5f);
        t.onComplete = () =>
        {
            CompletionWithMousePosition();
        };
    }
}
