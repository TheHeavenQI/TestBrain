using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using BaseFramework;
using DG.Tweening;

public class Level258DragMove : DragMoveEventTrigger
{
    public List<RectTransform> blocks;

    protected float minDis;
    protected bool isBlocked;

    protected float[] randomMoveDir;
    protected int randomMoveDirCount = 8;
    protected float maxMoveDis = 300;

    protected override void Start()
    {
        base.Start();
        randomMoveDir = new float[randomMoveDirCount];
        for (int i = 0; i < randomMoveDirCount; ++i)
        {
            randomMoveDir[i] = i * 360 / randomMoveDirCount;
        }

        if (blocks.Contains(this.rectTransform))
        {
            blocks.Remove(this.rectTransform);
        }
        minDis = (this.rectTransform.sizeDelta.x + blocks[0].sizeDelta.x) * this.rectTransform.lossyScale.x * 0.5f;
    }

    public override void OnBeginDrag(PointerEventData data)
    {
        base.OnBeginDrag(data);
        isBlocked = false;
    }

    public override void OnDrag(PointerEventData data)
    {
        onDrag?.Invoke(data);
        if (enableDragMove)
        {
            if (RectTransformUtility.ScreenPointToWorldPointInRectangle(this.rectTransform, data.position,
                                                               Camera.main,
                                                               out Vector3 worldPoint))
            {
                if (isBlocked)
                {
                    return;
                }
                Vector3 aim = worldPoint - offset;
                aim = CheckBlock(aim);
                this.rectTransform.position = aim;
            }
        }
    }

    protected Vector3 CheckBlock(Vector3 aim)
    {
        foreach (RectTransform trans in blocks)
        {
            float distance = Vector3.Distance(trans.position, aim);
            if (distance < minDis)
            {
                isBlocked = true;
                //Vector3 dir = aim - trans.position;
                //return dir.normalized * minDis;
                return this.rectTransform.position;
            }
        }
        return aim;
    }

    public bool RandomMove()
    {
        randomMoveDir.Shuffle();

        //是否被包围
        bool isClosed = true;
        foreach (RectTransform trans in blocks)
        {
            float distance = Vector3.Distance(trans.position, rectTransform.position);
            if (distance >= 1.3f * minDis)
            {
                isClosed = false;
                break;
            }
        }
        if (isClosed)
        {
            return false;
        }


        for (int i = 0; i < randomMoveDir.Length; ++i)
        {
            float angle = randomMoveDir[i];
            Vector2 dir = new Vector2(Mathf.Sin(angle), Mathf.Cos(angle));
            RaycastHit2D[] hits = Physics2D.RaycastAll(rectTransform.position, dir, 200);

            if (hits == null || hits.Length == 0 || hits.Length == 1 && hits[0].collider.gameObject == this.gameObject)
            {
                Vector3 pos = dir * maxMoveDis;
                pos += rectTransform.localPosition;
                pos.x = Mathf.Clamp(pos.x, -310, 300);
                pos.y = Mathf.Clamp(pos.y, -580, 324);
                if (Vector3.Distance(pos, rectTransform.localPosition) * this.rectTransform.lossyScale.x < 1.5f * minDis)
                {
                    continue;
                }
                rectTransform.DOLocalMove(pos, 0.2f);
                return true;
            }

            foreach (RaycastHit2D hit in hits)
            {
                if (hit.collider.gameObject == this.gameObject)
                {
                    continue;
                }
                float distance = Vector3.Distance(hit.collider.transform.position, rectTransform.position);
                if (distance > 1.5f * minDis)
                {
                    rectTransform.position += (hit.collider.transform.position - rectTransform.position).normalized * (distance - minDis) * 0.7f;
                    return true;
                }
            }
        }
        return false;
    }
}
