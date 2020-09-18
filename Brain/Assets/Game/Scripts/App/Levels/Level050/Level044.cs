using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level044 : LevelBasePage
{
    public Image seeNothingImg;
    public DragMove hideDM;
    public float hideTime = 2;
    protected float _pointDownTime;
    protected bool _isPress;
    private bool _finish;
    protected override void Start() {
        base.Start();

        hideDM.enabelDrag = false;
        hideDM.onPointerDown = () => {
            _isPress = true;
            _pointDownTime = Time.time;
        };

        hideDM.onPointerUp = () => {
            _isPress = false;
        };

        seeNothingImg.gameObject.SetActive(false);
    }

    protected void Update() {
        if (!_finish) {
            if (_isPress) {
                if (Time.time - _pointDownTime > hideTime) {
                    Completion();
                    _finish = true;
                }
            }
        }
    }

    protected override void OnCompletion() {
        seeNothingImg.gameObject.SetActive(true);
    }

    public override void Refresh() {
        base.Refresh();
        _finish = false;
        seeNothingImg.gameObject.SetActive(false);
    }
}
