using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Level032 : LevelBasePage {

    public DragMoveEventTrigger saw;
    public RectTransform tree;
    public Transform snail;
    private bool _isTreeDown;

    protected override void Start() {
        base.Start();

        saw.onEndDrag += (d) => {
            if (_isTreeDown) {
                return;
            }
            if (RectTransformExtensions.IsRectTransformOverlap(saw.rectTransform, tree)) {
                if (Input.deviceOrientation == DeviceOrientation.LandscapeLeft
                    || Input.acceleration.x <= -0.5f) {
                    tree.DORotate(new Vector3(0, 0, 90), 1f).OnComplete(() => {
                        Tweener t= snail.DOLocalMoveX(280,0.5f);
                        t.onComplete = () => { Completion(); };
                    });
                } else {
                    tree.DORotate(new Vector3(0, 0, -90), 1f).OnComplete(() => {
                        ShowError();
                    });
                }
            }
            _isTreeDown = true;
        };
    }

    public override void Refresh() {
        base.Refresh();
        DOTween.Kill(tree);
        tree.localEulerAngles = Vector3.zero;
        saw.Return2OriginPos();
        _isTreeDown = false;
    }
}
