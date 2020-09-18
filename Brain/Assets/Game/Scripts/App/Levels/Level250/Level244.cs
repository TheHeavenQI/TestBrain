using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Level244 : LevelBasePage
{
    public RectTransform bug;
    public RectTransform bugSquat;
    public RectTransform trans200m;
    public Scrollbar scrollbar;
    public Image energyImg;
    public CustomEventTrigger jump;

    private Vector3 _bugOriginPos;
    private bool _isPress;
    private float _energy;
    private bool _isJumping;
    private readonly float _energyAdd = 0.5f;
    private readonly float _maxJumpHeight = 530;
    private readonly float _minJumpHeight = 50;
    private readonly float _duration = 1f;

    protected override void Start()
    {
        base.Start();

        _bugOriginPos = bug.transform.localPosition;

        scrollbar.value = 0;
        bug.gameObject.SetActive(true);
        bugSquat.gameObject.SetActive(false);
        _energy = 0;
        energyImg.fillAmount = 0;

        jump.onPointerDown = (d) => {
            if (_isJumping)
            {
                return;
            }
            bug.gameObject.SetActive(false);
            bugSquat.gameObject.SetActive(true);
            _isPress = true;
        };

        jump.onPointerUp = (d) => {
            if (_isJumping)
            {
                return;
            }
            _isJumping = true;



            bug.gameObject.SetActive(true);
            bugSquat.gameObject.SetActive(false);

            float height = _maxJumpHeight * _energy;
            height = height < _minJumpHeight ? _minJumpHeight : height;

            float duration = _duration * height / _maxJumpHeight;
            duration = duration < _duration * 0.3f ? _duration * 0.3f : duration;

            bug.transform.DOLocalMoveY(_bugOriginPos.y + height, duration)
                .SetEase(Ease.OutQuad)
                .OnComplete(() => {
                    if (bug.transform.position.y >= trans200m.position.y)
                    {
                        Completion();
                        return;
                    }
                    else
                    {
                        bug.transform.DOLocalMoveY(_bugOriginPos.y, duration)
                            .SetEase(Ease.InQuad)
                            .OnComplete(() => {
                                bug.gameObject.SetActive(true);
                                bugSquat.gameObject.SetActive(false);
                                _isJumping = false;
                            });
                    }
                });

            _energy = 0;
            energyImg.fillAmount = 0;
            _isPress = false;
        };
    }

    private void Update()
    {
        if (_isPress)
        {
            _energy += _energyAdd * Time.deltaTime;
            _energy = _energy > 1 ? 1 : _energy;
            energyImg.fillAmount = _energy;
        }
    }

    public override void Refresh()
    {
        base.Refresh();
        scrollbar.value = 0;
        bug.DOKill();
        bug.transform.localPosition = _bugOriginPos;
        bug.gameObject.SetActive(true);
        bugSquat.gameObject.SetActive(false);
        _energy = 0;
        energyImg.fillAmount = 0;
    }
}
