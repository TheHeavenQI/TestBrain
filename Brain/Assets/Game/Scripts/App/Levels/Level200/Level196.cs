using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level196 : LevelBasePage {

    public float barrier;
    public float carAimMin;
    public ParticleSystem mStoneAni;
    public LimitDragMoveEventTrigger car;

    public Button stoneBtn;
    public Image fissureImg;
    public Sprite[] fissureSprites;

    private int _clickTime = 0;
    private readonly int _maxClickTime = 5;

    protected override void Start() {
        base.Start();

        fissureImg.gameObject.SetActive(false);
        fissureImg.sprite = fissureSprites[0];
        fissureImg.SetNativeSize();

        stoneBtn.onClick.AddListener(() => {
            ++_clickTime;
            if (_clickTime >= _maxClickTime) {
                stoneBtn.gameObject.SetActive(false);
                mStoneAni.Play();
                if (car.barrierX.Contains(barrier)) {
                    car.barrierX.Remove(barrier);
                }
            } else {
                fissureImg.gameObject.SetActive(true);
                fissureImg.sprite = fissureSprites[_clickTime - 1];
                fissureImg.SetNativeSize();
            }
        });

        car.onEndDrag += (d) => {
            if (car.transform.localPosition.x >= carAimMin) {
                Completion();
            }
        };
    }

    public override void Refresh() {
        base.Refresh();

        car.Return2OriginPos();
        stoneBtn.gameObject.SetActive(true);
        fissureImg.gameObject.SetActive(false);
        fissureImg.sprite = fissureSprites[0];
        fissureImg.SetNativeSize();

        if (!car.barrierX.Contains(barrier)) {
            car.barrierX.Add(barrier);
        }

        _clickTime = 0;
    }
}
