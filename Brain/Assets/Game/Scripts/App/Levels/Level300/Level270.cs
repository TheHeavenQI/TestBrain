using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Level270 : LevelBasePage
{
    public LimitDragMoveEventTrigger balloon;
    public Sprite balloonNormal;
    public Sprite balloonBreak;
    public CustomEventTrigger[] drats;

    private CustomEventTrigger pressedDrat;

    private Image _balloomImg;

    private readonly float _breakDis = 30;
    private readonly float _winY = 200;
    private readonly float _dratSpeed = 375;
    private readonly float _dratY = 57;

    private bool _isDead;

    protected override void Start()
    {
        base.Start();
        _balloomImg = balloon.GetComponent<Image>();

        for (int i = 0; i < drats.Length; ++i)
        {
            int j = i;
            drats[j].onPointerDown = (d) => {
                if (pressedDrat == null)
                {
                    pressedDrat = drats[j];
                }
            };
            drats[j].onPointerUp = (d) => {
                if (pressedDrat == drats[j])
                {
                    pressedDrat = null;
                }
            };
        }

        balloon.onDrag = (d) => {

            if (balloon.rectTransform.anchoredPosition.y >= _winY)
            {
                Completion();
            }
        };

        InitDrats();
    }

    private void InitDrats()
    {
        for (int i = 0; i < drats.Length; ++i)
        {
            float x = 230 - i * 230;
            drats[i].rectTransform.anchoredPosition = new Vector2(x, _dratY);
        }
    }

    public override void Refresh()
    {
        base.Refresh();
        _isDead = false;
        _balloomImg.sprite = balloonNormal;
        _balloomImg.SetNativeSize();
        balloon.Return2OriginPos();
        balloon.enableDragMove = true;

        InitDrats();
    }

    private void Update()
    {
        if (_isDead)
        {
            return;
        }
        float offset = _dratSpeed * Time.deltaTime;

        for (int i = 0; i < drats.Length; ++i)
        {
            float x = drats[i].rectTransform.anchoredPosition.x;

            if (pressedDrat == null
                || pressedDrat.rectTransform.anchoredPosition.x < x
                || pressedDrat.rectTransform.anchoredPosition.x - x > 3 * offset)
            {
                x += offset;
            }
            if (x > 550)
            {
                x -= 230 * (drats.Length - 1);
            }

            drats[i].rectTransform.anchoredPosition = new Vector2(x, _dratY);

            if (balloon.rectTransform.IsRectTransformOverlap(drats[i].rectTransform))
            {
                ShowError();
                _isDead = true;
                _balloomImg.sprite = balloonBreak;
                _balloomImg.SetNativeSize();
                balloon.enableDragMove = false;

                After(Refresh, 0.5f);
            }
        }
    }
}
