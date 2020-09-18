using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level121 : LevelBasePage
{
    public List<EventCallBack> items;



    protected override void Start()
    {
        base.Start();
        foreach (var item in items)
        {
            item.onClick = onClickItem;
            item.onPointerDown = onPointDownItem;
        }
    }
    void onClickItem()
    {
        ShowError();
    }
    void onPointDownItem()
    {
        bool suc = true;
        foreach (var item in items)
        {
            if (!item.isPressing)
            {
                suc = false;
                break;
            }
        }
        if (suc)
            Completion();
    }
}
