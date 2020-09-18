using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Christ007Snowball : MonoBehaviour
{
    public RectTransform rectTransform;

    public float radius => rectTransform.sizeDelta.x * 0.5f;

    private Vector2 _originSize;

    private void Reset()
    {
        rectTransform = this.transform as RectTransform;
    }

    private void Start()
    {
        _originSize = rectTransform.sizeDelta;
    }

    public void ResetSize()
    {
        DOTween.Kill(rectTransform);
        rectTransform.sizeDelta = _originSize;
    }
}
