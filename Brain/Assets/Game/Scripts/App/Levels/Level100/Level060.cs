using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level060 : LevelBasePage {

    public Text numFlag;
    public LongPressEventTrigger[] eventTriggers;
    public Button tapBtn;

    private Text[] _texts;
    private int _clear00Step = 0;
    private int _curTapCount = 0;
    private int _maxTapCount = 100;

    protected override void Start() {
        base.Start();
        

        tapBtn.onClick.AddListener(() => {
            ++_curTapCount;
            if (_curTapCount >= _maxTapCount) {
                CompletionWithMousePosition();
            }
            numFlag.text = _curTapCount.ToString();
        });

        _texts = new Text[eventTriggers.Length];
        for (int i = 0; i< eventTriggers.Length; ++i)
        {
            int j = i;
            _texts[j] = eventTriggers[j].GetComponent<Text>();
            eventTriggers[j].onPointerClick += (d) => OnTrigger(j);
            eventTriggers[j].onLongPress += () => {
                eventTriggers[j].ResetFinish();
                OnTrigger(j);
            };
        }

        
        numFlag.text = "";
    }

    private void OnTrigger(int j) {

        if (_clear00Step >= 3) {
            return;
        }

        ++_clear00Step;

        if (_clear00Step >= 3) {
            if (_curTapCount > 0)
            {
                numFlag.text = "0";
            }
            _texts[j].color = Color.clear;
            _maxTapCount = 1;
        } else {
            Color color = new Color(0, 0, 0, (3 - _clear00Step) * 0.33f);
            _texts[j].color = color;
        }
    }


    public override void LanguageSwitch() {
        base.LanguageSwitch();
        levelQuestionText.text = levelQuestionText.text.Replace("00", "<color=#00000000>00</color>");
    }

    public override void Refresh() {
        base.Refresh();
        _maxTapCount = 100;
        _curTapCount = 0;

        _clear00Step = 0;
        for (int i = 0; i < eventTriggers.Length; ++i)
        {
            _texts[i].color = Color.black;
        }

        numFlag.text = "";
    }
}
