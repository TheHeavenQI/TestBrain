using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class Christ007Candy : MonoBehaviour
{
    public enum State
    {
        Idle,
        Moving,
        Rotate
    }

    public State state = State.Idle;

    public Rigidbody2D rigibody;

    public RectTransform rectTransform;

    public float halfHight => rectTransform.sizeDelta.y * 0.5f;

    public Action<Christ007Candy> onClick;

    public Action onCollisionEnter;

    private void Reset()
    {
        rigibody = this.GetComponent<Rigidbody2D>();
        rectTransform = this.transform as RectTransform;
    }

    private void Start()
    {
        this.GetComponent<Button>().onClick.AddListener(() => onClick?.Invoke(this));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        onCollisionEnter?.Invoke();
    }
}
