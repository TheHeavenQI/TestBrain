using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class Level016 : InputNumLevel
{

    public Sprite showBefore;
    public Sprite showAfter;

    private Vector3 heirPos;

    public Image bg;

    public DragMove heir;

    protected override void Awake()
    {
        base.Awake();

        // 34, 103

        heirPos = heir.rectTransform.localPosition;

        heir.onDragEnd = () =>
        {

            if (Vector3.Distance(heir.rectTransform.localPosition, heirPos) >= 30)
            {
                bg.sprite = showAfter;
            }

        };

    }

    public override void Refresh()
    {
        base.Refresh();
        bg.sprite = showBefore;
        heir.Return2OriginPos();
    }

    protected override void OnCompletion()
    {
        base.OnCompletion();
    }

}
