using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class Level034 : SelectNumLevel
{

    public Image boom;

    protected override void OnCompletion()
    {
        base.OnCompletion();

        boom.enabled = true;

    }

    public override void Refresh()
    {
        base.Refresh();
        boom.enabled = false;
    }

}
