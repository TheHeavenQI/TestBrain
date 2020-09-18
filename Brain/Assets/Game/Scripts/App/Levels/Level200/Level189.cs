using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Level189 : LevelBasePage
{
    public Image normal;
    public Image suc;
    public Image line;
    public ConnectLine mConnect;
    protected override void Start()
    {
        base.Start();
        mConnect.TriggerConnect = () =>
        {
            line.enabled = true;
            Completion();
            After(()=> 
            {
                line.enabled = false;
                normal.enabled = false;
                suc.enabled = true;
                Completion();
            }, 0.5f);
        };
    }
}
