using UnityEngine;

public class Level049 : LevelBasePage {
    public DragMove obj1;
    public DragMove obj2;
    private bool _finish;
    private bool pressobj1;
    private bool pressobj2;
    private float totalTime = 0;
    protected override void Start() {
        base.Start();
        obj1.enabelDrag = false;
        obj2.enabelDrag = false;
        obj1.onPointerDown = () => { pressobj1 = true; };
        obj1.onPointerUp = () => {
            pressobj1 = false;
            if (!pressobj2 && !_finish) {
                ShowError();
            }
        };
        obj2.onPointerDown = () => { pressobj2 = true; };
        obj2.onPointerUp = () => {
            pressobj2 = false;
            if (!pressobj1 && !_finish) {
                ShowError();
            }
        };
    }

    private void Update() {
        if (!_finish) {
            if (pressobj1 && pressobj2) {
                totalTime += Time.deltaTime;
                if (totalTime > 0.5) {
                    _finish = true;
                    Completion();
                }
            }
            else {
                totalTime = 0;
            }
        }
    }
}
