using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Level111 : LevelBasePage
{

    public void ClickAnswerItem(bool correct)
    {
        if (correct)
            Completion();
        else
        {
            ShowError();
        }
    }
    
}
