using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class Level070 : LevelBasePage
{

    public Button tapButton;

    public Sprite sprite;
    public Button correctBtn;

    protected override void Start()
    {
        base.Start();

        correctBtn.onClick.AddListener(() =>
        {
            correctBtn.image.sprite = sprite;
            CompletionWithMousePosition();
        });

    }

}
