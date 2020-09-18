using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class Level260 : LevelBasePage
{
    public RectTransform goldBall;
    public RectTransform ltBall;
    public Button bottomBall;
    public Button switchBtn;
    public RectTransform slit;
    public RectTransform breakBall;
    public DragMoveEventTrigger coin;

    /// <summary>
    /// [0],goldball;[1],ltBall;[2],bottomBall
    /// </summary>
    private Vector3[] _originPos;

    /// <summary>
    /// 0,初始状态；1，第一颗球下落；2，再次放入硬币；3再次摇奖
    /// </summary>
    private int _step = 0;

    private readonly float _doorY = -450;

    protected override void Start()
    {
        base.Start();

        _originPos = new Vector3[] { goldBall.position, ltBall.position, bottomBall.transform.position };

        breakBall.gameObject.SetActive(false);
        coin.gameObject.SetActive(false);

        switchBtn.onClick.AddListener(() => {
            switch (_step)
            {
                case 0:
                    switchBtn.transform.DOLocalRotate(new Vector3(0, 0, 90), 0.3f)
                        .OnComplete(() => {
                            ltBall.DOMove(_originPos[0], 0.5f);
                            goldBall.DOMove(_originPos[2], 0.5f);
                            bottomBall.transform.DOLocalMoveY(_doorY, 0.5f);
                        });
                    _step = 1;
                    break;
                case 2:
                    switchBtn.transform.DOLocalRotate(new Vector3(0, 0, 90), 0.3f)
                        .OnComplete(() => {
                            ltBall.DOMove(_originPos[2], 0.5f);
                            goldBall.DOLocalMoveY(_doorY, 0.5f).OnComplete(() => Completion());
                        });
                    _step = 3;
                    break;
            }
        });

        bottomBall.onClick.AddListener(() => {
            bottomBall.gameObject.SetActive(false);
            breakBall.gameObject.SetActive(true);
            coin.gameObject.SetActive(true);
        });

        coin.onEndDrag = (d) => {
            if (coin.rectTransform.IsRectTransformOverlap(slit))
            {
                coin.gameObject.SetActive(false);
                breakBall.gameObject.SetActive(false);
                _step = 2;
                switchBtn.transform.DOLocalRotate(Vector3.zero, 0.3f);
            }
        };
    }

    public override void Refresh()
    {
        base.Refresh();

        goldBall.position = _originPos[0];
        ltBall.position = _originPos[1];
        bottomBall.transform.DOKill();
        bottomBall.transform.position = _originPos[2];
        bottomBall.gameObject.SetActive(true);

        breakBall.gameObject.SetActive(false);
        coin.gameObject.SetActive(false);
        coin.Return2OriginPos();

        _step = 0;
    }
}
