
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Level091 : LevelBasePage {
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
    public DragMove circle;
    private int _circleIndex;
    protected override void Start() {
        base.Start();
        for (int i = 0; i < lines.Count; i++) {
            lines[i].gameObject.SetActive(false);
        }
        for (int i = 0; i < list.Count; i++) {
            var btn = list[i].GetComponent<DragMove>();
            btn.enabelDrag = false;
            var image = list[i].GetComponent<Image>();
            _images.Add(image);
            _buttons.Add(btn);
        }
        ResetImages();
        for (int i = 0; i < _images.Count; i++) {
            var btn = _buttons[i];
            if ((i >= 2 && i <=4) || i == 8) {
                var img = _images[i];
                img.sprite = null;
                var k = i;
                btn.enabled = true;
                btn.onPointerUp = () => {
                    Click(k);
                };
            }
            else {
                btn.enabled = false;
            }
        }
        
        circle.onDragEnd = () => {
            for (int i = 0; i < _images.Count; i++) {
                var im = _images[i];
                if (Vector3.Distance(im.transform.localPosition,circle.transform.localPosition) < 80) {
                    circle.transform.DOLocalMove(im.transform.localPosition, 0.5f);
                    _points[i] = 1;
                    _circleIndex = i;
                    return;
                }
            }
            circle.Return2OriginPos(0.5f);
        };
    }
    
    private void Click(int k) {
        _points[k] = 1;
        if (_points[2] == 1 && _points[5] == 1 && _points[8] == 1) {
            SetEnableClick(false);
            if (_circleIndex != 2) {
                _images[2].sprite = OkSprite;
            }
            if (_circleIndex != 8) {
                _images[8].sprite = OkSprite;
            }
            var line = lines[5];
            line.color = OKColor;
            line.gameObject.SetActive(true);
            finish = true;
            Completion(new Vector3(-7,-205,0));
            return;
        }
        if(_points[3] == 1 && _points[4] == 1 && _points[5] == 1) {
            SetEnableClick(false);
            var line = lines[1];
            line.color = OKColor;
            line.gameObject.SetActive(true);
            if (_circleIndex != 3) {
                _images[3].sprite = OkSprite;
            }
            if (_circleIndex != 4) {
                _images[4].sprite = OkSprite;
            }
            finish = true;
            Completion(new Vector3(-7,-205,0));
            return;
        }
        _points[k] = 0;
        ShowError();
    }

    public override void Refresh() {
        base.Refresh();
        ResetImages();
        circle.Return2OriginPos();
    }

    private void ResetImages() {
        _circleIndex = -1;
        _points = new List<int>();
        for (int i = 0; i < _images.Count; i++) {
            var img = _images[i];
            _points.Add(0);
            if (i == 0 || i == 1 || i == 6) {
                img.sprite = CloseSprite;
            }else if (i == 5 || i == 7) {
                _points[i] = 1;
                img.sprite = OkSprite;
            }
            else {
                img.sprite = null;
            }
            img.SetNativeSize();
        }
    }
}
