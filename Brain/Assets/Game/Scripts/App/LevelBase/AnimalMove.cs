using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalMove : MonoBehaviour
{
    public List<RectTransform> Blocks;
    public RectTransform correctRect;
    public RectTransform rectTransform { get; private set; }
    private bool _isJumpUp;
    private bool _isJumpDown;
    public Action errorAction;
    public Action correctAction;

    public Vector2 jumpDownOffset { set; get; } = new Vector2(100, -200);
    public Vector2 jumpUpOffset { set; get; } = new Vector2(100, 200);
    private float _jumpTime = 1f;
    private bool _isFinish;

    private void Awake()
    {
        rectTransform = transform as RectTransform;
    }

    public Vector3 orgPosition { private set; get; }

    private void Start()
    {
        orgPosition = transform.position;
    }

    public void JumpUp()
    {
        if (!enabled)
        {
            return;
        }
        if (!_isJumpDown && !_isJumpUp)
        {
            _isFinish = false;
            _isJumpUp = true;
            _jumpTime = 0;
        }
    }

    public void CheckDown()
    {
        _isJumpDown = true;
    }

    public void Moveto(Vector2 offset)
    {
        if (!enabled)
        {
            return;
        }
        if (!IsTriggerBlock(offset))
        {
            rectTransform.localPosition += new Vector3(offset.x, offset.y, 0);
            CheckDown();
            CheckFinish();
        }
        else
        {
            if (Vector2.Distance(offset, Vector2.zero) > 10)
            {
                Moveto(offset * 0.5f);
            }
            else
            {
                CheckFinish();
            }
        }
    }

    private void CheckFinish()
    {
        if (correctRect != null && RectTransformExtensions.IsRectTransformOverlap(rectTransform, correctRect))
        {
            _isFinish = true;
            correctAction?.Invoke();
        }
    }

    private bool IsTriggerBlock(Vector2 vector2)
    {
        bool isOverlap = false;
        for (int i = 0; i < Blocks.Count; i++)
        {
            var rect = Blocks[i];
            if (IsRectTransformOverlap(rectTransform.anchoredPosition + vector2, rectTransform.sizeDelta,
                  rect.anchoredPosition, rect.sizeDelta))
            {
                isOverlap = true;
            }
        }
        return isOverlap;
    }

    private bool IsRectTransformOverlap(Vector2 pos1, Vector2 size1, Vector2 pos2, Vector2 size2)
    {
        float rect1MinX = pos1.x - size1.x / 2;
        float rect1MaxX = pos1.x + size1.x / 2;
        float rect1MinY = pos1.y - size1.y / 2;
        float rect1MaxY = pos1.y + size1.y / 2;

        float rect2MinX = pos2.x - size2.x / 2;
        float rect2MaxX = pos2.x + size2.x / 2;
        float rect2MinY = pos2.y - size2.y / 2;
        float rect2MaxY = pos2.y + size2.y / 2;
        bool xNotOverlap = rect1MaxX <= rect2MinX || rect2MaxX <= rect1MinX;
        bool yNotOverlap = rect1MaxY <= rect2MinY || rect2MaxY <= rect1MinY;
        bool notOverlap = xNotOverlap || yNotOverlap;
        return !notOverlap;
    }

    public void Refresh()
    {
        enabled = true;
        _isFinish = false;
        transform.position = orgPosition;
        DOTween.Kill(transform);
    }

    private void Update()
    {
        if (!_isFinish)
        {
            if (_isJumpUp)
            {
                _jumpTime += Time.deltaTime;
                if (_jumpTime > 0.5f)
                {
                    _isJumpUp = false;
                    _isJumpDown = true;
                    _jumpTime = 0;
                }
                rectTransform.localPosition += new Vector3(jumpUpOffset.x, jumpUpOffset.y, 0) * Time.deltaTime;
            }
            else if (_isJumpDown)
            {
                var offset = jumpDownOffset * Time.deltaTime;
                if (IsTriggerBlock(offset))
                {
                    _isJumpDown = false;
                    _jumpTime = 0;
                    return;
                }
                _jumpTime += Time.deltaTime;
                if (_jumpTime >= 2f)
                {
                    _isJumpDown = false;
                    _jumpTime = 0;
                    errorAction?.Invoke();
                }
                Moveto(offset);
            }
        }
    }
}
