using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level193 : LevelBasePage {
    public DragMove gou;
    public DragMove ren;
    public Transform mubiao1;
    public Transform mubiao2;
    public Button startButton;
    private bool _started;

    protected override void Start() {
        base.Start();
        startButton.onClick.AddListener(() => { _started = true; });
        ren.enabelDrag = false;
        gou.enabelDrag = false;
    }

    private void Update() {
        if (_started) {
            gou.transform.localPosition += new Vector3(200*Time.deltaTime,0,0);
            if (!ren.isPressing) {
                ren.transform.localPosition += new Vector3(300*Time.deltaTime,0,0);
            }
            if (Vector2.Distance(gou.transform.localPosition,mubiao1.localPosition) < 100) {
                _started = false;
                Completion();
            }
            if (Vector2.Distance(ren.transform.localPosition,mubiao2.localPosition) < 100) {
                ShowError();
                _started = false;
                After(() => {
                    Refresh();
                },0.5f);
            }
        }
    }

    public override void Refresh() {
        base.Refresh();
        _started = false;
        ren.Return2OriginPos();
        gou.Return2OriginPos();
    }
}
