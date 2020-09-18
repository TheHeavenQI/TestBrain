using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Level225 : LevelBasePage
{

    public AnimalMove cat;
    public RectTransform fish;
    public RectTransform perplexBlock;
    public RectTransform perplexBlockTrigger;
    public Button leftBtn;
    public Button rightBtn;
    public Button jumpBtn;

    private float _moveSpeed = 50;
    private Vector2 _jumpSpeed = new Vector2(255, 400);

    private Vector3 _perplexOriginPos;
    private bool _isBlockDown;

    protected override void Start()
    {
        base.Start();

        leftBtn.onClick.AddListener(OnLeftClick);
        rightBtn.onClick.AddListener(OnRightClick);
        jumpBtn.onClick.AddListener(OnJumpClick);

        cat.jumpUpOffset = _jumpSpeed;
        cat.jumpDownOffset = new Vector2(_jumpSpeed.x, -_jumpSpeed.y);
        cat.correctAction = () => Completion();
        cat.errorAction = () => {
            ShowError();
            After(Refresh, 0.3f);
        };

        _perplexOriginPos = perplexBlock.position;
    }

    public override void Refresh()
    {
        base.Refresh();
        cat.Refresh();

        perplexBlock.DOKill();
        perplexBlock.position = _perplexOriginPos;
        _isBlockDown = false;
    }

    private void Update()
    {
        if (_isBlockDown)
        {
            Vector3 offset = cat.jumpDownOffset * Time.deltaTime;
            cat.rectTransform.localPosition += offset;
        }
        else
        {
            Vector3 catPos = cat.transform.localPosition;
            if (RectTransformExtensions.IsRectTransformOverlap(cat.rectTransform, perplexBlockTrigger))
            {
                _isBlockDown = true;
                perplexBlock.DOLocalMoveY(-750, 1);
                cat.enabled = false;
                After(() => {
                    ShowError();
                    After(Refresh, 0.5f);
                }, 1);
            }
        }
    }

    private void LateUpdate()
    {
        if (cat.transform.localPosition.x > 360)
        {
            Vector3 pos = cat.transform.localPosition;
            pos.x = 360;
            cat.transform.localPosition = pos;
        }
    }

    private void OnLeftClick()
    {
        if (cat.transform.localPosition.x < -360)
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
}
