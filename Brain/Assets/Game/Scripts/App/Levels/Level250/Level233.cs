
using UnityEngine;
using UnityEngine.UI;

public class Level233 : LevelBasePage {
    public EventCallBack ren;
    private Image _renImage;
    public Sprite image1;
    public Sprite image2;
    public Sprite image3;
    private int _timeCount;
    protected override void Start() {
        base.Start();
        _renImage = ren.GetComponent<Image>();
        _timeCount = 0;
        ren.onSwipe = (a) => {
            _timeCount++;
            if (_timeCount == 20) {
                _renImage.sprite = image2;
            }
            if (_timeCount == 40) {
                _renImage.sprite = image3;
                After(() => {Completion();}, 0.5f);
            }
        };

    }

    public override void Refresh() {
        base.Refresh();
        _timeCount = 0;
        _renImage.sprite = image1;
    }
}
