using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using System;

public class GiftBoxCollider : MonoBehaviour
{
    public Action<bool, string> collisionCallBack;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.LogError(collision.name);
        if (collision.name == "box")
        {
            collisionCallBack?.Invoke(true, collision.name);
        }
        else
        {
            collisionCallBack?.Invoke(false, collision.name);
        }

    }
}
