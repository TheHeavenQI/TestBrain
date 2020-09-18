
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class chrismasMan : MonoBehaviour
{
    public Action onTriggerSnowMan;
    Canvas mCanvas;
    private void Start()
    {
        mCanvas = GetComponent<Canvas>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        onTriggerSnowMan?.Invoke();
    }
    public void SetSortOrder(int depth)
    {
        mCanvas.sortingOrder = depth;
    }
}
