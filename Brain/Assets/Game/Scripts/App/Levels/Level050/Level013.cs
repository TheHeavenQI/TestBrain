using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Level013 : LevelBasePage {
    public Sprite CloseSprite;
    public Sprite OkSprite;
    public List<GameObject> list;
    private List<Image> _images = new List<Image>();
    private List<DragMove> _buttons = new List<DragMove>();
    private List<int> _points = new List<int>();
    public List<Image> lines = new List<Image>();
    public Color CloseColor;
    public Color OKColor;
    private bool finish;
    protected override void Start() {
        base.Start();
        for (int i = 0; i < lines.Count; i++) {
            lines[i].gameObject.SetActive(false);
        }
        for (int i = 0; i < list.Count; i++) {
            _points.Add(0);
            var btn = list[i].GetComponent<DragMove>();
            btn.enabelDrag = false;
            var image = list[i].GetComponent<Image>();
            _images.Add(image);
            _buttons.Add(btn);
        }
        Refresh();
        
        for (int i = 0; i < _images.Count; i++) {
            var btn = _buttons[i];
            if ((i >= 2 && i <=4) || i == 8) {
                var img = _images[i];
                img.sprite = null;
                var k = i;
                btn.enabled = true;
                btn.onPointerDown = () => { PointDown(k); };
                btn.onPointerUp = () => { PointUp(k); };
            }
            else {
                btn.enabled = false;
            }
        }
    }

    private void PointUp(int k) {
        ///编辑器下直接通关
#if UNITY_EDITOR
        Completion();
        return;;
#endif
        if (finish) {
            return;
        }
        _points[k] = 0;
        _images[k].sprite = OkSprite;
        ShowError(new Vector3(-7,-205,0));
        Image line = null;
                    
        if (k == 2) {
            line = lines[3];
            _images[3].sprite = CloseSprite;
            _images[3].SetNativeSize();
        }else if (k == 3 || k == 4 || k == 8) {
            line = lines[0];
            _images[2].sprite = CloseSprite;
            _images[2].SetNativeSize();
        }
                    
        if (line != null) {
            line.color = CloseColor;
            line.gameObject.SetActive(true);
        }

        SetEnableClick(false);
        After(() => {
            Refresh();
            SetEnableClick(true);
            if (line != null) {
                line.gameObject.SetActive(false);
            }
        },1);
    }

    private void PointDown(int k) {
        _points[k] = 1;
        if (_points[2] == 1 && _points[8] == 1) {
            SetEnableClick(false);
            _images[2].sprite = OkSprite;
            _images[8].sprite = OkSprite;
            var line = lines[5];
            line.color = OKColor;
            line.gameObject.SetActive(true);
            finish = true;
            Completion(new Vector3(-7,-205,0));
        }
        if(_points[3] == 1 && _points[4] == 1) {
            SetEnableClick(false);
            var line = lines[1];
            line.color = OKColor;
            line.gameObject.SetActive(true);
            _images[3].sprite = OkSprite;
            _images[4].sprite = OkSprite;
            finish = true;
            Completion(new Vector3(-7,-205,0));
        }
    }

    public override void Refresh() {
        base.Refresh();
        for (int i = 0; i < _images.Count; i++) {
            var img = _images[i];
            if (i == 0 || i == 1 || i == 6) {
                img.sprite = CloseSprite;
            }else if (i == 5 || i == 7) {
                img.sprite = OkSprite;
            }
            else {
                img.sprite = null;
            }
            img.SetNativeSize();
        }
    }
}
