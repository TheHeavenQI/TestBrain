using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Itemmove : MonoBehaviour
{
    public Action<Itemmove> onComplete;
    [SerializeField]
    public float speed = 2f;
    [SerializeField]
    public float Target = -2000;
    // Update is called once per frame
    RectTransform sefRect;
    private void Start()
    {
        sefRect = transform as RectTransform;
    }
    void FixedUpdate()
    {
        if (!active)
            return;
        sefRect.anchoredPosition -= new Vector2(0,speed);
        if (sefRect.anchoredPosition.y < Target)
            onComplete.Invoke(this);
    }
    private bool active = true;
    public void Pause()
    {
        active = false;
    }
    public void Play()
    {
        active = true;
    }
}
