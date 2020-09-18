using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Level219 : LevelBasePage
{
    public AnimalMove snowMan;
    public Image snowManDead;
    public Button leftBtn;
    public Button rightBtn;
    public Button jumpBtn;

    public LongPressEventTrigger[] fires;

    private float _moveSpeed = 50;
    private float _jumpSpeed = 200;

    private int _fireOutCount = 0;
    private bool _isDead;

    protected override void Start()
    {
        base.Start();

        leftBtn.onClick.AddListener(OnLeftClick);
        rightBtn.onClick.AddListener(OnRightClick);
        jumpBtn.onClick.AddListener(OnJumpClick);

        snowMan.jumpUpOffset = new Vector2(0, _jumpSpeed);
        snowMan.jumpDownOffset = -snowMan.jumpUpOffset;

        for (int i = 0; i < fires.Length; ++i)
        {
            int j = i;
            fires[j].onLongPress += () => {
                fires[j].transform.DOScale(0, 0.3f).OnComplete(() => {
                    if (++_fireOutCount >= fires.Length)
                    {
                        Completion();
                    }
                });
            };
        }
    }

    public override void Refresh()
    {
        base.Refresh();
        snowMan.Refresh();

        for (int i = 0; i < fires.Length; ++i)
        {
            fires[i].ResetFinish();
            fires[i].transform.localScale = Vector3.one;
        }

        snowMan.gameObject.SetActive(true);
        snowManDead.gameObject.SetActive(false);

        _isDead = false;
        _fireOutCount = 0;
    }

    private void OnLeftClick()
    {
        if (snowMan.transform.localPosition.x < -370)
        {
            return;
        }
        snowMan.Moveto(new Vector2(-_moveSpeed, 0));
    }

    private void OnRightClick()
    {
        if (snowMan.transform.localPosition.x > 370)
        {
            return;
        }
        snowMan.Moveto(new Vector2(_moveSpeed, 0));
    }

    private void OnJumpClick()
    {
        snowMan.JumpUp();
    }

    private void Update()
    {
        if (isLevelComplete || _isDead)
        {
            return;
        }
        foreach (var fire in fires)
        {
            if (fire.transform.localScale.x > 0.1f
                && RectTransformExtensions.IsRectTransformOverlap(snowMan.rectTransform, fire.rectTransform))
            {
                _isDead = true;
                snowMan.gameObject.SetActive(false);
                Vector3 pos = snowMan.transform.localPosition;
                pos.y -= 27.5f;
                snowManDead.transform.localPosition = pos;
                snowManDead.gameObject.SetActive(true);

                ShowError();
                After(Refresh, 0.5f);
            }
        }
    }
}
