using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Level178 : LevelBasePage {

    public float barrier;
    public float carAimMin;

    public LimitDragMoveEventTrigger car;
    public LimitDragMove stone;
    public LimitDragMoveEventTrigger bridge;

    private int _strikeTime;
    private Tweener _stoneDownTween;

    bool theStoneDragTriggerHit = false;
    Vector3 stoneDefPos;

    protected override void Start()
    {
        base.Start();
        stoneDefPos = stone.transform.localPosition;

        bridge.enableDragMove = false;

        car.onEndDrag += (d) =>
        {
            if (car.transform.localPosition.x >= carAimMin)
            {
                Completion();
            }
        };

        stone.onBeginDrag += (d) =>
        {
            _stoneDownTween?.Kill();
            theStoneDragTriggerHit = false;
        };
        stone.onDrag += (d) =>
        {
            bool isHight = stone.transform.localPosition.y >= -160;
            theStoneDragTriggerHit = isHight || theStoneDragTriggerHit;
        };
        stone.onEndDrag += (d) =>
        {
            if (!stone.enableDragMove)
                return;
            bool isOffisite = stone.transform.localPosition.y > stoneDefPos.y;
            if (isOffisite)
            {
                _stoneDownTween = stone.Return2OriginPos(0.3f);
                if (theStoneDragTriggerHit)
                { 
                    _stoneDownTween.SetEase(Ease.InQuad).onComplete = hitBridge;
                    return;
                }
            }
            if (theStoneDragTriggerHit)
            {
                hitBridge();
            }
        };
    }
    void hitBridge()
    {
        ++_strikeTime;
        if (_strikeTime > 0)
        {
            bridge.GetComponent<Image>().enabled = false;
            bridge.transform.GetChild(0).gameObject.SetActive(_strikeTime == 1);
            bridge.transform.GetChild(1).gameObject.SetActive(_strikeTime >1);
        }
        if (_strikeTime >= 3)
        {
            stone.enableDragMove = false;
            stone.transform.DOLocalMoveY(-345.3f, 0.3f);
            bridge.transform.DOLocalMoveY(-420, 0.3f).OnComplete(() =>
            {
                if (car.barrierX.Contains(barrier))
                {
                    car.barrierX.Remove(barrier);
                }
            });
        }
    }
    public override void Refresh() {
        base.Refresh();

        bridge.GetComponent<Image>().enabled = true;
        bridge.transform.GetChild(0).gameObject.SetActive(false);
        bridge.transform.GetChild(1).gameObject.SetActive(false);

        car.Return2OriginPos();
        stone.Return2OriginPos();
        bridge.Return2OriginPos();
        DOTween.Kill(stone.transform);
        DOTween.Kill(bridge.transform);

        if (!car.barrierX.Contains(barrier)) {
            car.barrierX.Add(barrier);
        }

        _strikeTime = 0;
        stone.enableDragMove = true;
    }
}
