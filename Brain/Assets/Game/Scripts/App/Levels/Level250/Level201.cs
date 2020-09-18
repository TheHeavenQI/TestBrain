
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level201 : LevelBasePage {
    public Sprite normal_sprite;
    public Sprite rev_sprite;
    public Sprite rev2_sprite;
    public List<Button> buttons;
    private List<Image> _images = new List<Image>();
    private bool _rev;
    private bool _finish;
    protected override void Start() {
        base.Start();
        _finish = false;
        _rev = false;
        for (int i = 0; i < buttons.Count; i++) {
            var index = i;
            _images.Add(buttons[i].GetComponent<Image>());
            buttons[i].onClick.AddListener(() => {
                if (index == 2 && _rev) {
                    Completion();
                }
                else {
                    ShowErrorWithMousePosition();
                }
            });
        }
    }

    private void Update() {
        if (!_finish) {
            if (Input.acceleration.y > 0.7 && !_rev) {
                _rev = true;
                RefreshImages();
            }
            if (Input.acceleration.y < -0.7 && _rev) {
                _rev = false;
                RefreshImages();
            }
        }
    }

    public void RefreshImages() {
        for (int i = 0; i < _images.Count; i++) {
            if (_rev) {
                if (i == 2) {
                    _images[i].sprite = rev2_sprite;
                }
                else {
                    _images[i].sprite = rev_sprite;
                }
            }
            else {
                _images[i].sprite = normal_sprite;
            }
        }
    }
    public override void Refresh() {
        base.Refresh();
        _finish = false;
        RefreshImages();
    }
}
