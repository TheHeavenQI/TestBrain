using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Level181 : LevelBasePage
{

    public AnyFingerMove mStone;

    protected override void Start()
    {
        base.Start();
        mStone.onDragEnd = () =>
        {
            if (mStone.distanceToOrign > 100)
                Completion();
        };
    }
    public override void Refresh()
    {
        base.Refresh();
        mStone.ReturnToStart();
    }
}
