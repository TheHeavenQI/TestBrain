using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level038 : OrderClickLevel
{
    public Image mStamp;

    protected override void Start()
    {
        base.Start();
        mStamp.gameObject.SetActive(false);
    }

    public override void Refresh()
    {
        base.Refresh();
        mStamp.gameObject.SetActive(false);
    }

    protected override void OnCompletion()
    {
        base.OnCompletion();
        mStamp.gameObject.SetActive(true);
    }
}
