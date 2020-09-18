using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Level214 : LevelBasePage
{
    public AnimalMove cat;
    public RectTransform fish;
    public Button leftBtn;
    public Button rightBtn;
    public Button jumpBtn;

    private float _moveSpeed = 50;
    private float _jumpSpeed = 200;
    /// <summary>
    /// ×¹Âä
    /// </summary>
    private bool _isFalling;

    protected override void Start()
    {
        base.Start();

        leftBtn.onClick.AddListener(OnLeftClick);
        rightBtn.onClick.AddListener(OnRightClick);
        jumpBtn.onClick.AddListener(OnJumpClick);

        cat.jumpUpOffset = new Vector2(0, _jumpSpeed);
        cat.jumpDownOffset = -cat.jumpUpOffset;
        cat.correctAction = () => Completion();
    }

    public override void Refresh()
    {
        base.Refresh();
        cat.Refresh();
        _isFalling = false;
    }

    private void OnLeftClick()
    {
        if (cat.transform.localPosition.x < -370)
        {
            return;
        }
        cat.Moveto(new Vector2(-_moveSpeed, 0));
    }

    private void OnRightClick()
    {
        if (cat.transform.localPosition.x > 370)
        {
            return;
        }
        cat.Moveto(new Vector2(_moveSpeed, 0));
    }

    private void OnJumpClick()
    {
        cat.JumpUp();
    }

    private void Update()
    {
        if (isLevelComplete)
        {
            return;
        }
        if (Input.deviceOrientation == DeviceOrientation.PortraitUpsideDown
            || Input.GetKeyDown(KeyCode.Space))
        {
            if (_isFalling)
            {
                return;
            }
            _isFalling = true;
            cat.enabled = false;
            cat.transform.DOLocalMoveY(500, 1)
                .OnUpdate(() => {
                    if (RectTransformExtensions.IsRectTransformOverlap(cat.rectTransform, fish))
                    {
                        DOTween.Kill(cat.transform);
                        Completion();
                    }
                })
                .OnComplete(() => {
                    ShowError();
                    After(Refresh, 1f);
                });
        }
    }
}
