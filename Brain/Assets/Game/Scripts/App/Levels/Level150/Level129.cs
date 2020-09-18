using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Level129 : LevelBasePage
{


    int correctAnswer = 8;
    public InputField mInput;
    public void ClickCommit()
    {
        if (string.IsNullOrEmpty(mInput.text))
        {
            ShowError();
            return;
        }
        int value = int.Parse(mInput.text);
        if (value == correctAnswer)
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
