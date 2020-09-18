using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Level105 : LevelBasePage
{

    public Text value;
    protected override void Start()
    {
        base.Start();
        value.text = "0";
        curInputIndex = 0;
    }
    int curInputIndex = 0;
    public void ClickKey(int index)
    {
        if (curInputIndex >8)
            return;
        if (curInputIndex == 0)
            value.text = index.ToString();
        else
            value.text += index.ToString();
        curInputIndex += 1;
    }
    public void ClickCommit()
    {
        int valueInt = 0;
        if(!string.IsNullOrEmpty(value.text))
            valueInt = int.Parse(value.text);
        if (valueInt == 31181)
            Completion();
        else
        {
            ShowError();
            Refresh();
        }
    }
    public override void Refresh()
    {
        base.Refresh();
        value.text = "0";
        curInputIndex = 0;
    }
}
