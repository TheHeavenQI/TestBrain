using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Level182 : LevelBasePage {

    public float barrier;
    public float carAimMin;
    public float cloudXMin;
    public float cloudXMax;

    public LimitDragMoveEventTrigger car;
    public LimitDragMoveEventTrigger cloud;
    public LimitDragMoveEventTrigger bridge;

    private bool _isBridgeMove;

    protected override void Start() {

        base.Start();

        car.onEndDrag += (d) => {
            if (car.transform.localPosition.x >= carAimMin) {
                Completion();
            }
        };

        cloud.onEndDrag += (d) => {
            if (_isBridgeMove) {
                return;
            }

            if (cloud.transform.localPosition.x >= cloudXMin && cloud.transform.localPosition.x <= cloudXMax) {
                _isBridgeMove = true;
                bridge.transform.DOLocalMoveY(-296, 0.3f)
                    .OnComplete(() => {
                        if (car.barrierX.Contains(barrier)) {
                            car.barrierX.Remove(barrier);
                        }
                    });
            }
        };
    }


    public override void Refresh() {
        base.Refresh();

        DOTween.Kill(bridge.transform);

        car.Return2OriginPos();
        cloud.Return2OriginPos();
        bridge.Return2OriginPos();

        if (!car.barrierX.Contains(barrier)) {
            car.barrierX.Add(barrier);
        }

        _isBridgeMove = false;
    }
}
