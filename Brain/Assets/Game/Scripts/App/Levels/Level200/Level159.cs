
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level159 : LevelBasePage {
    public List<Button> btnList;
    public List<Image> btnImageList;
    public List<Image> bulbList;
    public Sprite blubOff;
    public Sprite blubOn;
    public Sprite btnOn;
    public Sprite btnOff;
    public DragMove dragMove;
    private bool _moveOut;
    private int _lastPressIndex;
    protected override void Start() {
        base.Start();
        for (int i = 0; i < btnList.Count; i++) {
            var btn = btnList[i];
            var k = i;
            btn.onClick.AddListener(() => {
                int off = k;
                _lastPressIndex = k;
                for (int j = 0; j < btnList.Count; j++) {
                    if (off == j) {
                        btnImageList[j].sprite = btnOff;
                        bulbList[j].sprite = blubOff;
                    }
                    else {
                        btnImageList[j].sprite = btnOn;
                        bulbList[j].sprite = blubOn;
                    }
                    btnImageList[j].SetNativeSize();
                    bulbList[j].SetNativeSize();
                }
                Check();
            });
        }

        dragMove.onDragEnd = () => {
            var a = dragMove.transform.localPosition;
            var rect = transform.GetComponent<RectTransform>().rect;
            if (Math.Abs(a.x) > rect.width/2 - 50 || Math.Abs(a.y) > rect.height/2 - 50) {
                _moveOut = true;
            }
            Check();
        };
    }

    private void Check() {
        if (_moveOut && _lastPressIndex == 2) {
            Completion();
        }
    }

    public override void Refresh() {
        base.Refresh();
        _lastPressIndex = -1;
        _moveOut = false;
        dragMove.Return2OriginPos();
    }
}
