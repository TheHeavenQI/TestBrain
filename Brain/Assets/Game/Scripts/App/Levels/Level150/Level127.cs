using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Level127 : LevelBasePage
{
    public InputField mInput;
    int answer = 43215;
    public void ClickCommit()
    {
        if (string.IsNullOrEmpty(mInput.text))
        {
            ShowError();
            return;
        }
        int value = int.Parse(mInput.text);
        if (value == answer)
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
        mInput.text = "";
    }
}   
