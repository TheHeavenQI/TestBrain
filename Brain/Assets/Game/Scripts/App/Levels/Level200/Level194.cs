using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level194 : InputNumLevel
{
    public GameObject winTip;

    protected override void OnCompletion()
    {
        winTip.SetActive(true);
        base.OnCompletion();
    }

    public override void Refresh()
    {
        base.Refresh();
        winTip?.SetActive(false);
    }
}
