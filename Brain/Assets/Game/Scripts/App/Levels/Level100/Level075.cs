using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level075 : LevelBasePage {
    public Text okText;
    public Button okBtn;
    public Text showText;
    public List<Button> numList;
    private int _currentNum = 0;
    protected override void Start() {
        base.Start();
        for (int i = 0; i < numList.Count; i++) {
            var btn = numList[i];
            btn.onClick.AddListener(() => {
                var a = int.Parse(btn.name);
                _currentNum = _currentNum * 10 + a;
                showText.text = $"{_currentNum}";
            });
        }
        okBtn.onClick.AddListener(() => {
            if (_currentNum == 909) {
                Completion();
            }
            else {
                ShowError();
                _currentNum = 0;
                showText.text = $"{_currentNum}";
            }
        });
    }

    public override void Refresh() {
        base.Refresh();
        _currentNum = 0;
        showText.text = $"{_currentNum}";
    }
}
