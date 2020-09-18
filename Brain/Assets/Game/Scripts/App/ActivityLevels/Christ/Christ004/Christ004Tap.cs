using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Christ004Tap : MonoBehaviour
{
    public Action<GameObject> OnGetGift;
    public Action<GameObject> OnTriggerShit;
    string giftName = "gift";
    string shitName = "shit";
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Contains(giftName))
            OnGetGift?.Invoke(collision.gameObject);
        if (collision.gameObject.name.Contains(shitName))
            OnTriggerShit?.Invoke(collision.gameObject);
    }
}
