using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class Level078 : SelectNumLevel
{

    public Image line;

    public override void Refresh()
    {
        base.Refresh();
        line.enabled = false;
    }

    protected override void OnCompletion()
    {
        base.OnCompletion();

        line.enabled = true;

    }
}
