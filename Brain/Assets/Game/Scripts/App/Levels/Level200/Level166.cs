using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class Level166 : LevelBasePage
{

    public LongPressEventTrigger press;

    public Button successBtn;

    protected override void Start()
    {
        base.Start();
        levelQuestionText.rectTransform.sizeDelta = new Vector2(430, 520);

        press.onLongPress += ChangeShow;

    }

    public override void Refresh()
    {
        base.Refresh();
        contentBGImg.GetComponent<Image>().color = Color.white;

        successBtn.gameObject.SetActive(false);

        press.ResetFinish();

    }

    

    public void ChangeShow()
    {
        contentBGImg.GetComponent<Image>().color = Color.black;

        successBtn.gameObject.SetActive(true);
    }

    public void tapToShowSuccess()
    {
        Completion();
    }

    public override void LanguageSwitch()
    {
        base.LanguageSwitch();
        levelQuestionText.text = levelQuestionText.text.Replace("\\n", "\n");
    }

}
