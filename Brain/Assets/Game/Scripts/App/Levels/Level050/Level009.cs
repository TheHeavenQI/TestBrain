using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BaseFramework;

public class Level009 : LevelBasePage {

    public List<Button> listBtn;
    public Sprite sprite1;
    public Sprite sprite2;
    private List<Image> imageList = new List<Image>();
    private int _index = -1;

    private int[] points = new int[] { 10, 20, 30, 40, 100 };

    protected override void Start() {
        base.Start();
        for (int i = 0; i < listBtn.Count; i++) {
            var btn = listBtn[i];
            var img = btn.transform.Find("Image").GetComponent<Image>();
            imageList.Add(img);
            var k = i;
            btn.GetComponentInChildren<Text>().text = $"{points[i]} {Localization.GetText("level_009_point")}";
            btn.onClick.AddListener(() => {
                _index = k;
                reset();
                if (k == 4) {
                    if (AppSetting.isIOS)
                    {
                        CompletionWithMousePosition();
                    }
                    else
                    {
                        ShowSuccessWithMousePosition();
                        SetEnableClick(false);
                        After(() =>
                        {
                            PopUpManager.Instance.ShowScore(false, () =>
                            {
                                PopUpManager.Instance.ShowFinish(levelIndex);
                            });
                        }, animTime);
                    }
                }
                else {
                    ShowErrorWithMousePosition();
                }
            });
        }
        Refresh();
    }

    public override void Refresh() {
        base.Refresh();
        _index = -1;
        reset();
    }

    private void reset() {
        for (int i = 0; i < imageList.Count; i++) {
            if (i == _index) {
                imageList[i].sprite = sprite2;
            }
            else {
                imageList[i].sprite = sprite1;
            }
        }
    }
}
