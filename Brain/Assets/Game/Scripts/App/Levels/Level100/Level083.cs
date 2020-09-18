
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Level083 : LevelBasePage {
    public DragMove ring;
    public DragMove car;
    public Image dog;
    public Sprite[] Sprites;

   // public MutiClickEventCallBack dogEventCallBack;
    private bool _finish;
    private readonly int needClickCount = 3;
    protected override void Start() {
        base.Start();
        ring.onDragEnd = () => {
            ring.Return2OriginPos(0.5f);
            ShowError();
        };
        car.onDragEnd = () => {
            ring.Return2OriginPos(0.5f);
            ShowError();
        };
    }

    public void ClickDog()
    {
        if (_finish)
            return;
        clickCount += 1;
        dog.sprite = Sprites[clickCount];
        dog.SetNativeSize();
        if (clickCount >= needClickCount)
        {
            _finish = true;
            After(() =>
            {
                Completion();
            }, 0.5f);
        }
    }
    int clickCount = 0;
    public override void Refresh() {
        base.Refresh();
        _finish = false;
        clickCount = 0;
        dog.sprite = Sprites[0];
        dog.SetNativeSize();
    }
}
