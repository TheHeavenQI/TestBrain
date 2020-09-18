using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Christ001OutDoor : MonoBehaviour
{
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject == Christ001.instance.giftBox.gameObject)
        {
            //Debug.LogError($"Christ001 {collision.collider.name}");
            float ydis = transform.localPosition.y - collision.gameObject.transform.localPosition.y;
            ydis = Mathf.Abs(ydis);
            if (ydis <= 10f)
            {
                Christ001.instance.Completion();
                Christ001.instance.giftBox.OnLevelCompletion();
                Christ001.instance.giftBox.transform.DOMove(this.transform.position, 0.1f);
            }
        }
    }
}
