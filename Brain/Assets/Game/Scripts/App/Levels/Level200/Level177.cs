
using System;
using UnityEngine;
using UnityEngine.UI;

public class Level177 : LevelBasePage
{
    public Image normal;
    public GameObject fast;
    public Shake mShake;
    protected override void Start() {
        base.Start();
        normal.enabled = true;
        fast.SetActive(false);
        mShake.shakeAction = () =>
        {
            normal.enabled = (false);
            fast.SetActive(true);
            After(()=> { Completion(); },1);        
        };
        
    }
    public override void Refresh()
    {
        base.Refresh();
        fast.SetActive(false);
        normal.enabled = true;
    }
    public void ClickDianchi()
    {
        ShowError();
    }
    
}
