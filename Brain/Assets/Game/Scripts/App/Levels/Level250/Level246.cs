using UnityEngine;
using UnityEngine.UI;
using BaseFramework;
using DG.Tweening;
using System;

public class Level246 : LevelBasePage
{
    public RectTransform goldBall;
    public RectTransform[] balls;
    public Button switchBtn;

    private Vector3[] _originPos;
    private Vector3[] _tempPos;
    private bool _isDown;

    private Shake246 _shake;

    protected override void Start()
    {
        base.Start();
        base.delayComplete = 1.5f;

        _shake = new Shake246();
        _shake.shakeAction = () => {
            if (_isDown || isLevelComplete)
            {
                return;
            }
            SwapBall();
        };

        _originPos = new Vector3[balls.Length];
        _tempPos = new Vector3[balls.Length];
        for (int i = 0; i < balls.Length; ++i)
        {
            _originPos[i] = balls[i].localPosition;
            _tempPos[i] = balls[i].localPosition;
        }

        switchBtn.onClick.AddListener(() => {
            if (_isDown)
            {
                return;
            }
            _isDown = true;

            switchBtn.transform.DOLocalRotate(new Vector3(0, 0, 90), 0.3f)
                .OnComplete(() => {
                    RectTransform bottomBall = GetBottomBall();
                    bottomBall.DOLocalMoveY(-450, 0.5f).OnComplete(() => {
                        if (bottomBall == goldBall)
                        {
                            Completion();
                        }
                        else
                        {
                            ShowError();
                            After(Refresh, 0.5f);
                        }
                    });
                });
        });
    }

    private void Update()
    {
        if (isLevelComplete)
        {
            return;
        }
        _shake.Update();
    }

    public override void Refresh()
    {
        base.Refresh();

        for (int i = 0; i < balls.Length; ++i)
        {
            balls[i].DOKill();
            balls[i].localPosition = _originPos[i];
        }

        switchBtn.transform.DOKill();
        switchBtn.transform.localEulerAngles = Vector3.zero;

        _isDown = false;
    }

    private void SwapBall()
    {
        _tempPos.Shuffle();

        for (int i = 0; i < balls.Length; ++i)
        {
            balls[i].localPosition = _tempPos[i];
        }
    }

    private RectTransform GetBottomBall()
    {
        RectTransform bottomBall = balls[0];
        for (int i = 1; i < balls.Length; ++i)
        {
            if (balls[i].localPosition.y < bottomBall.localPosition.y)
            {
                bottomBall = balls[i];
            }
        }
        return bottomBall;
    }

    private class Shake246
    {
        public Action shakeAction;
        private Vector3 _laseAcceleration;
        private float _distanceMin = 1;
        private float _distanceMax = 10;

        private float _intervalShake = 1;
        private float _lastActionTime = 0;

        public void Update()
        {
            if (Time.time - _lastActionTime < _intervalShake)
            {
                return;
            }

            Vector3 acceleration = Input.acceleration;
            Vector3 offset = acceleration - _laseAcceleration;
            offset = new Vector3(Mathf.Abs(offset.x), Mathf.Abs(offset.y), Mathf.Abs(offset.y));
            if (Input.GetKey(KeyCode.Space)
                || offset.x >= _distanceMin && offset.x <= _distanceMax
                || offset.y >= _distanceMin && offset.y <= _distanceMax
                || offset.z >= _distanceMin && offset.z <= _distanceMax
                )
            {
                shakeAction?.Invoke();
                _lastActionTime = Time.time;
            }

            _laseAcceleration = acceleration;
        }
    }
}
