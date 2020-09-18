using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;

public class Christ003Bread : MonoBehaviour
{
    /// <summary>
    /// 是否可以拖拽
    /// </summary>
    public bool enableDragMove { get; set; } = true;

    public Action<bool> collisionCallBack;

    public new Rigidbody2D rigidbody { get; protected set; }
    public new Collider2D collider { get; protected set; }

    public RectTransform rectTransform { get; protected set; }

    /// <summary>
    /// 开始拖拽是手指偏移量
    /// </summary>
    protected Vector3? offset;

    /// <summary>
    /// 物体起始位置
    /// </summary>
    protected Vector3 originPos;

    protected void Awake()
    {
        rigidbody = this.GetComponent<Rigidbody2D>();
        collider = this.GetComponent<Collider2D>();
        rectTransform = transform as RectTransform;
    }

    private void Start()
    {
        originPos = transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.LogError(collision.collider.name);
        if (collision.collider.name == "socks")
        {
            collisionCallBack?.Invoke(true);
        }
        else
        {
            collisionCallBack?.Invoke(false);
        }
        offset = null;
        rigidbody.velocity = Vector2.zero;
    }


    private void OnTriggerEnter2D(Collider2D collider)
    {
        //Debug.LogError(collider.name);
        if (collider.name == "socks")
        {
            collisionCallBack?.Invoke(true);
        }
        else
        {
            collisionCallBack?.Invoke(false);
        }
        offset = null;
        rigidbody.velocity = Vector2.zero;
    }


    private void Update()
    {
        if (!enableDragMove)
        {
            return;
        }

        if (Input.GetMouseButton(0))
        {
            if (offset == null)
            {
                if (RectTransformUtility.ScreenPointToWorldPointInRectangle(this.rectTransform, Input.mousePosition,
                                                       Camera.main,
                                                       out Vector3 worldPoint))
                {
                    offset = worldPoint - transform.position;
                }
            }
            else
            {
                if (RectTransformUtility.ScreenPointToWorldPointInRectangle(this.rectTransform, Input.mousePosition,
                                       Camera.main,
                                       out Vector3 worldPoint))
                {
                    rigidbody.MovePosition(worldPoint - offset.Value);
                }
            }
        }
        else
        {
            offset = null;
            rigidbody.velocity = Vector2.zero;
        }
    }


    /// <summary>
    /// 回到最开始的位置
    /// </summary>
    public void Return2OriginPos()
    {
        rigidbody.MovePosition(originPos);
    }

    /// <summary>
    /// 回到最开始的位置
    /// </summary>
    public Tweener Return2OriginPos(float duration)
    {
        return rigidbody.DOMove(originPos, duration);
    }
}
