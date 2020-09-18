using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class Christ006Card : MonoBehaviour
{
    public enum State
    {
        Hide,
        Show,
        Collect,
        DoTweening
    }

    public int id { get; private set; }

    public State state { get; private set; }

    public Action<Christ006Card> onClick;

    private Image _image;
    private Sprite _closeSprite;
    private Sprite _openSprite;

    private float _duration = 0.3f;
    private Button _button;

    private void Awake()
    {
        _image = this.GetComponent<Image>();
        _button = this.GetComponent<Button>();
        _button.onClick.AddListener(() => {
            onClick?.Invoke(this);
        });
    }

    public void Init(int id, Sprite closeSprite, Sprite openSprite)
    {
        this.id = id;
        this._closeSprite = closeSprite;
        this._openSprite = openSprite;
        this._image.sprite = _closeSprite;

        DOTween.Kill(this.transform);
        this.state = State.DoTweening;
        this.transform.localEulerAngles = Vector3.zero;
        this.transform.localScale = Vector3.zero;
        this.transform.DOScale(1, _duration).OnComplete(() => {
            this.state = State.Hide;
        });

    }

    public void Show(Action onComplete = null)
    {
        if (state != State.Hide)
        {
            return;
        }
        state = State.DoTweening;

        this.transform.DORotate(new Vector3(0, 90, 0), _duration).OnComplete(() => _image.sprite = _openSprite);
        this.transform.DORotate(Vector3.zero, _duration).SetDelay(_duration).OnComplete(() => {
            state = State.Show;
            onComplete?.Invoke();
        });
    }

    public void Hide(Action onComplete = null)
    {
        if (state != State.Show)
        {
            return;
        }
        state = State.DoTweening;

        this.transform.DORotate(new Vector3(0, 90, 0), _duration).OnComplete(() => _image.sprite = _closeSprite);
        this.transform.DORotate(Vector3.zero, _duration).SetDelay(_duration).OnComplete(() => {
            state = State.Hide;
            onComplete?.Invoke();
        });
    }

    public void Collect(Action onComplete = null)
    {
        if (state != State.Show)
        {
            return;
        }
        state = State.DoTweening;

        this.transform.DOScale(0, _duration).OnComplete(() => {
            state = State.Collect;
            onComplete?.Invoke();
        });
    }
}
