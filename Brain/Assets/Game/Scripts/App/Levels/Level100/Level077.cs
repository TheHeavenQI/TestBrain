using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level077 : LevelBasePage {
    public List<DragMove> list;
    public Transform meinv;
    protected override void Start() {
        base.Start();
        for (int i = 0; i < list.Count; i++) {
            var btn = list[i];
            btn.onDragEnd = () => {
                if (btn.name == "bao") {
                    var dis = btn.transform.localPosition - meinv.localPosition;
                    if (dis.x > 200 || dis.x < -200 || dis.y > 300 || dis.y < -300) {
                        Completion();
                        return;
                    }
                }
                ShowError();
                btn.Return2OriginPos(0.5f);
            };
        }
    }
}
