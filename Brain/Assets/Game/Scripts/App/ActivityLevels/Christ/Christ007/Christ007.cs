using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Christ007 : ChristBaseLevel
{
    /// <summary>
    /// 糖果预制体
    /// </summary>
    public Christ007Candy candyPrefab;
    /// <summary>
    /// 雪人预制体
    /// </summary>
    public GameObject snowmanPrefab;
    /// <summary>
    /// 雪球
    /// </summary>
    public Christ007Snowball snowball;
    /// <summary>
    /// 剩余糖果数量文本
    /// </summary>
    public Text leftcandyText;
    /// <summary>
    /// 发射糖果的按钮
    /// </summary>
    public Button moveCandyBtn;



    /// <summary>
    /// 所有糖果
    /// </summary>
    private List<Christ007Candy> _candies = new List<Christ007Candy>();
    /// <summary>
    /// 跟随旋转的糖果
    /// </summary>
    private List<Christ007Candy> _rotateCandies = new List<Christ007Candy>();
    /// <summary>
    /// 创建的用于移动的雪人
    /// </summary>
    private GameObject _snowman = null;
    /// <summary>
    /// 剩余需要插入的糖果数量
    /// </summary>
    private int _leftCandy = 9;
    /// <summary>
    /// 剩余变大的次数
    /// </summary>
    private int _leftBigger = 5;
    /// <summary>
    /// 雪球旋转方向
    /// </summary>
    private readonly Vector3 _rotateAxis = new Vector3(0, 0, 1);
    /// <summary>
    /// 每秒钟雪球旋转角度
    /// </summary>
    private float _rotateAngle = 150;
    /// <summary>
    /// 每一次tips减小旋转角度
    /// </summary>
    private float _rotateAngleCut = 10;
    /// <summary>
    /// 糖果插入雪球偏移量
    /// </summary>
    private float _candyBallOffset = 20;
    /// <summary>
    /// 糖果插入雪球的移动时间
    /// </summary>
    private float _candyMoveDuration = 0.1f;
    /// <summary>
    /// 是否已死亡
    /// </summary>
    private bool _isDead = false;

    protected override void Start()
    {
        base.Start();
        candyPrefab.gameObject.SetActive(false);
        CreatCandy();
        leftcandyText.text = $"x{_leftCandy}";

        moveCandyBtn.onClick.AddListener(() => {
            if (_candies.Count > 0)
            {
                OnCandyClick(_candies[_candies.Count - 1]);
            }
        });
    }

    private void Update()
    {
        snowball.rectTransform.RotateAround(snowball.rectTransform.position, _rotateAxis, _rotateAngle * Time.deltaTime);
        foreach (Christ007Candy candy in _rotateCandies)
        {
            candy.rectTransform.RotateAround(snowball.rectTransform.position, _rotateAxis, _rotateAngle * Time.deltaTime);
        }
    }


    public override void Refresh()
    {
        base.Refresh();

        foreach (var candy in _candies)
        {
            Object.DestroyImmediate(candy.gameObject);
        }
        _candies.Clear();
        _rotateCandies.Clear();
        //snowball.ResetSize();
        CreatCandy();

        _leftCandy = 9;
        leftcandyText.text = $"x{_leftCandy}";

        if (_snowman != null)
        {
            Object.Destroy(_snowman);
        }

        _isDead = false;
    }

    protected override void OnTipsDialogCloseWithTipsUsed()
    {
        base.OnTipsDialogCloseWithTipsUsed();

        SnowBallBigger();
    }

    private void SnowBallBigger()
    {
        if (_leftBigger <= 0)
        {
            return;
        }
        --_leftBigger;

        _snowman = Instantiate<GameObject>(snowmanPrefab);
        _snowman.transform.SetParent(snowmanPrefab.transform.parent);
        _snowman.transform.SetAsLastSibling();
        _snowman.transform.position = snowmanPrefab.transform.position;
        _snowman.transform.localScale = snowmanPrefab.transform.localScale;

        _snowman.transform.DOMove(snowball.transform.position, 1f)
            .OnComplete(() => {
                Object.DestroyImmediate(_snowman);
                _snowman = null;
                snowball.rectTransform.DOSizeDelta(snowball.rectTransform.sizeDelta * 1.2f, 0.5f)
                .SetUpdate(true)
                .OnUpdate(() => {
                    float distance = snowball.radius + candyPrefab.halfHight - _candyBallOffset;
                    foreach (var candy in _rotateCandies)
                    {
                        Vector3 direction = candy.rectTransform.localPosition - snowball.rectTransform.localPosition;
                        direction = direction.normalized;
                        candy.rectTransform.localPosition = snowball.rectTransform.localPosition + direction * distance;
                    }
                })
                .OnComplete(() => {
                    _rotateAngle -= _rotateAngleCut;
                });
            });
    }

    private void CreatCandy()
    {
        Christ007Candy candy = Instantiate<Christ007Candy>(candyPrefab);
        candy.transform.SetParent(candyPrefab.transform.parent);
        candy.transform.SetSiblingIndex(candyPrefab.transform.GetSiblingIndex());
        candy.transform.position = candyPrefab.transform.position;
        candy.transform.localScale = candyPrefab.transform.localScale;
        candy.onClick = OnCandyClick;
        candy.onCollisionEnter = OnCandyCollision;
        candy.gameObject.SetActive(true);
        _candies.Add(candy);
    }

    private void OnCandyClick(Christ007Candy candy)
    {
        if (candy.state != Christ007Candy.State.Idle)
        {
            return;
        }
        CandyMove(candy);
    }

    private void CandyMove(Christ007Candy candy)
    {
        candy.state = Christ007Candy.State.Moving;
        float aimY = snowball.rectTransform.position.y - (snowball.radius + candy.halfHight - _candyBallOffset) * snowball.rectTransform.lossyScale.y;
        candy.transform.DOMoveY(aimY, _candyMoveDuration)
            .OnComplete(() => {
                candy.rigibody.velocity = Vector2.zero;
                candy.state = Christ007Candy.State.Rotate;
                _rotateCandies.Add(candy);

                --_leftCandy;
                leftcandyText.text = $"x{_leftCandy}";

                if (_isDead)
                {
                    return;
                }

                if (_leftCandy == 0)
                {
                    Completion();
                }
                else
                {
                    CreatCandy();
                }
            });
    }

    private void OnCandyCollision()
    {
        if (_isDead)
        {
            return;
        }
        _isDead = true;
        ShowError();
        After(Refresh, 0.5f);
    }
}
