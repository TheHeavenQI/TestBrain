using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level094 : LevelBasePage
{
    public Image breakImage;
    public Shake shake;

    protected override void Start()
    {
        base.Start();
        shake.shakeAction = ShakeCallback;
        breakImage.enabled = false;
    }

    private void ShakeCallback()
    {
        Completion();
    }

    protected override void OnCompletion() {
        base.OnCompletion();
        breakImage.enabled = true;
    }

    public override void Refresh() {
        base.Refresh();
        breakImage.enabled = false;
    }
}
