
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using DG.Tweening;

public class Level001 : LevelBasePage {
    public List<Button> buttons;
    private List<Sprite> _sprites = new List<Sprite>();
    private Sprite _sprite;

    private float shakeTime = 2.5f;
    private float currentTime = 0;

    protected override void Start() {
        base.Start();
        for (int i = 0; i < buttons.Count; i++) {
            Button btn = buttons[i];
            btn.onClick.AddListener(() => ClickButton(btn));
            _sprites.Add(btn.image.sprite);
        }
        _sprite = _sprites[0];
    }

    private void FixedUpdate()
    {

        if (Time.time - currentTime >= shakeTime)
        {
            int i = Random.Range(0, buttons.Count);
            buttons[i].transform.DOShakeRotation(0.5f, 30f).SetLoops(2);
            currentTime = Time.time;
        }

    }

    private void ClickButton(Button btn) {
        if (btn.image.sprite == _sprite) {
            CompletionWithMousePosition();
            // 关闭引导
            if (levelIndex == GuidenceManager.Instance().guideCount + 1)
            {
                GuidenceManager.Instance().guideCount += 1;
            }
        }
        else {
            ShowErrorWithMousePosition();
        }
    }
    public override void Refresh() {
        base.Refresh();
        _sprites = _sprites.RandomList();
        for (int i = 0; i < buttons.Count; i++) {
            buttons[i].image.sprite = _sprites[i];
            buttons[i].image.SetNativeSize();
        }
    }
    
    
}
