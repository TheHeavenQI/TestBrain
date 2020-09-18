using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class CorrectErrorButtonsLevel : LevelBasePage
{
    public List<Button> allButton;
    public Button correctButton;
    public List<Button> correctButtons;

    protected override void Start()
    {
        base.Start();

        if (correctButton != null && !allButton.Contains(correctButton))
        {
            allButton.Add(correctButton);
        }

        if (correctButtons != null)
        {
            foreach (Button temp in correctButtons)
            {
                if (temp != null && !allButton.Contains(temp))
                {
                    allButton.Add(temp);
                }
            }
        }

        for (int i = 0; i < allButton.Count; i++)
        {
            var btn = allButton[i];
            btn.onClick.AddListener(() => {
                if ((correctButton != null && btn == correctButton)
                    || (correctButtons != null && correctButtons.Contains(btn)))
                {
                    CompletionWithMousePosition();
                }
                else
                {
                    ShowErrorWithMousePosition();
                }
            });
        }
    }
}
