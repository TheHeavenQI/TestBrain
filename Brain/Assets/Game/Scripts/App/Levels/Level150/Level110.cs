using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level110 : LevelBasePage {

    public RectTransform barbellTop;
    public RectTransform barbellBottom;

    public GameObject leftHand;
    public GameObject rightHand;

    private int _touchCount = 2;

    private bool _isTouched;
    private Vector2 _lastTouchPos;
    private Vector3 _originPos;

    protected override void Start() {
        base.Start();
        _originPos = barbellBottom.position;
    }

    private void Update() {

#if UNITY_EDITOR
        if (Input.GetMouseButton(0)) {
            if (!_isTouched) {
                _isTouched = true;
                _lastTouchPos = Input.mousePosition;
                leftHand.SetActive(true);
                rightHand.SetActive(true);
                return;
            }
            Vector3 offset = Input.mousePosition - new Vector3(_lastTouchPos.x, _lastTouchPos.y);
            barbellBottom.transform.localPosition += offset;
            _lastTouchPos = Input.mousePosition;
            
        } else {
            if (_isTouched) {
                if (RectTransformExtensions.IsRectTransformOverlap(barbellTop, barbellBottom)) {
                    barbellBottom.DOMove(barbellTop.transform.position, 0.1f).OnComplete(() => {
                        CompletionWithMousePosition();
                    });
                }
            }
            _isTouched = false;
        }
        return;
#endif


        if (Input.touchCount == _touchCount) {
            leftHand.SetActive(true);
            rightHand.SetActive(true);
            Touch touch = Input.GetTouch(0);
            if (!_isTouched) {
                _isTouched = true;
                _lastTouchPos = touch.position;
                return;
            }
            Vector3 offset = touch.position - _lastTouchPos;
            barbellBottom.transform.localPosition += offset;
            _lastTouchPos = touch.position;
        } else if(Input.touchCount == 1) {
            leftHand.SetActive(true);
            rightHand.SetActive(false);
        }
        else {
            if (_isTouched) {
                if (RectTransformExtensions.IsRectTransformOverlap(barbellTop, barbellBottom)) {
                    barbellBottom.DOMove(barbellTop.transform.position, 0.1f).OnComplete(() => {
                        CompletionWithMousePosition();
                    });
                }
            }
            leftHand.SetActive(false);
            rightHand.SetActive(false);
            _isTouched = false;
        }
    }

    public override void Refresh() {
        base.Refresh();
        barbellBottom.position = _originPos;
        leftHand.SetActive(false);
        rightHand.SetActive(false);
    }
}
