using UnityEngine;
using UnityEngine.UI;

public class Level199 : LevelBasePage {
    public Sprite off;
    public Sprite on;
    public Image dian;
    public DragMove dragMove;
    protected override void Start() {
        base.Start();
        dragMove.onDragEnd = () => {
            dragMove.Return2OriginPos(0.5f);
            ShowError();
        };
    }

    private void Update() {
        if (SystemInfo.batteryStatus == BatteryStatus.Charging) {
            dian.sprite = on;
            dian.SetNativeSize();
            After(() => {
                Completion();
            },0.5f);
        }
    }
}
