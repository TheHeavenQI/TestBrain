using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level098 : LevelBasePage
{
    public DragMove dm_1;
    public DragMove dm_2;
    public Image commom;
    public Image suc;
    protected override void Start()
    {
        base.Start();
        commom.enabled = true;
        suc.enabled = false;
        dm_1.onDragEnd = () =>
        {
            dm_1.Return2OriginPos(0.5f);
            ShowError();
        };
        dm_2.onDragEnd = () =>
        {
            dm_2.Return2OriginPos(0.5f);
            ShowError();
        };
    }
    private void Update()
    {
        if (Input.acceleration.y > 0.8f)
        {
            commom.enabled = false;
            suc.enabled = true;
            base.After(()=> { Completion(); },0.8f);
          
        }
    }
    public override void Refresh()
    {
        base.Refresh();
        dm_1.Return2OriginPos();
        dm_2.Return2OriginPos();
    }
}
