using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Level163 : LevelBasePage
{
    public InputField mInput;
    int correctValue = 410;



    public void ClickCommit()
    {
        if (string.IsNullOrEmpty(mInput.text))
        {
            ShowError();
            return;
        }
        int value = int.Parse(mInput.text);
        if (value == correctValue)
            Completion();
        else
            ShowError();
    }
    public override void Refresh()
    {
        base.Refresh();
        mInput.text = "";
    }
}
