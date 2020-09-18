using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class Level204 : LevelBasePage
{
	public Shake shake;
    public GameObject mGuang;
    public Button btn;
    public Sprite mBtnSprite_1;
    public Sprite mBtnSprite_2;
	protected override void Start()
	{
		base.Start();
		shake.shakeAction = ShakeCallback;
    }
	private void ShakeCallback()
	{
        btn.gameObject.SetActive(true);
        StartCoroutine("showLeg");
    }
    IEnumerator showLeg()
    {
        int count = 0;
        Image btnImage = btn.image;
        mGuang.SetActive(true);
        while (true)
        {
            count += 1;
            if (count % 35 == 0)
            {
                if (count % 2 == 1)
                    btnImage.sprite = mBtnSprite_2;
                else
                    btnImage.sprite = mBtnSprite_1;
            }
            if (count >= 1000000)
                count = 0;
            yield return null;
        }
    }
    public void ButtonEvent()
    {
        Completion();
    }

    public override void Refresh()
    {
        base.Refresh();
        mGuang.SetActive(false);
        btn.gameObject.SetActive(false);
        StopCoroutine("showLeg");
    }

}
