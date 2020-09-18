using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Linq;
using BaseFramework;

public class Level140 : LevelBasePage {

    public Button mamBtn;
    public GameObject hand1;
    public GameObject hand2;
    public Transform ball;
    public Transform[] ballPaths;
    public RectTransform basketry;
    public MultClickEventTrigger baseMultClick;

    private Vector3 _ballOriginPos;
    private Vector3 _basketryOriginPos;

    private Vector3[] pathPoints;

    private enum BasketryType {
        Top,
        Downing,
        Bottom
    }

    private BasketryType _basketryType;

    private bool _isBallMoving;

    public DOTweenAnimation anim;
    protected override void Start() {
        base.Start();

        _ballOriginPos = ball.position;
        _basketryOriginPos = basketry.position;
        
        mamBtn.onClick.AddListener(() => {
            if (_basketryType == BasketryType.Downing || _isBallMoving || isLevelComplete) {
                return;
            }
            hand1.SetActive(false);
            hand2.SetActive(true);

            if (pathPoints == null) {
                pathPoints = ballPaths.Select(it => it.localPosition).ToArray();
                pathPoints = LineUtil.GetSmoothPoints(pathPoints);
            }

            _isBallMoving = true;

            ball.transform.DOLocalPath(pathPoints, 1)
                .SetEase(Ease.Linear)
                .OnComplete(() => {

                    if (_basketryType == BasketryType.Bottom) {
                        Completion();
                    } else {
                        ShowError();
                        hand1.SetActive(true);
                        hand2.SetActive(false);
                        ball.transform.position = _ballOriginPos;
                    }
                    _isBallMoving = false;
                });
        });
        
        baseMultClick.onPointerClick += (a) => {
            if (_basketryType == BasketryType.Top) {
                anim.DORestart();
            }
        };
        baseMultClick.onMultClick += () => {
            if (_basketryType != BasketryType.Top || isLevelComplete) {
                return;
            }
            _basketryType = BasketryType.Downing;
            basketry.DOLocalMoveY(-514, 1).OnComplete(() => _basketryType = BasketryType.Bottom);
        };
    }

    public override void Refresh() {

        base.Refresh();

        DOTween.Kill(ball);
        DOTween.Kill(basketry);

        ball.position = _ballOriginPos;
        basketry.position = _basketryOriginPos;
        baseMultClick.ResetFinish();

        _basketryType = BasketryType.Top;

        hand1.SetActive(true);
        hand2.SetActive(false);
    }
}
