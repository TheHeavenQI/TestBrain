using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level147 : LevelBasePage
{
    public EventCallBack mPingkou;
    public Shake shakeScript;
    public Sprite afterShake;
    public Sprite beforeShake;

    public Image bottle;

    protected override void Start()
    {
        base.Start();
        shakeScript.shakeAction = () =>
        {
            if (mPingkou.isPressing)
                bottle.sprite = afterShake;
                Completion();
        };
    }

    public override void Refresh()
    {
        base.Refresh();
        bottle.sprite = beforeShake;
    }

}
