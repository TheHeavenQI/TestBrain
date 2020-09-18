
using UnityEngine;

public class Level008 : SelectNumLevel {
    public DragMove car;
    protected override void Awake() {
        car.RefreshOriginPos();
        base.Awake();
    }

    public override void Refresh() {
        base.Refresh();
        car.Return2OriginPos();
    }
}
