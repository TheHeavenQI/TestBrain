
using UnityEngine;
using UnityEngine.UI;

public class Level217 : LevelBasePage {
    public Sprite imageOpen;
    public Sprite imageClose;
    public EventCallBack yachi;
    public EventCallBack tou;
    public Image eyu;
    protected override void Start() {
        base.Start();
        yachi.onDragDraging = (pos) => {
            if (tou.isPressing) {
                yachi.transform.position = pos;
            }
        };
        yachi.onDragBegin = () => {
            if (!tou.isPressing) {
                eyu.sprite = imageClose;
                eyu.SetNativeSize();
                ShowError();
                After(() => {
                    Refresh();
                },2);
            }
        };
        yachi.onDragEnd = () => {
            if (tou.isPressing) {
                Completion();
            }
        };
    }

    public override void Refresh() {
        base.Refresh();
        eyu.sprite = imageOpen;
        eyu.SetNativeSize();
    }
}
