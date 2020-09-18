using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Level113 : LevelBasePage
{

    public Shake mShake;

    public Image mCommon;
    public Image mSuc;

    protected override void Start()
    {
        base.Start();
        mShake.shakeAction = onShake;
        mCommon.enabled = true;
        mSuc.enabled = false;
    }
    void onShake()
    {
        mSuc.enabled = true;
        mCommon.enabled = false;
        Completion();
    }
}
