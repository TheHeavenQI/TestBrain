using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public enum Christ010TriggerType
{
    borad,
    shit,
    top,
    bottom,
    ScreenBottom,
}
public class Christ010Man : MonoBehaviour
{
    public Animation mAni;
    public Christ010 mRoot;
    /// <summary>
    /// 长按计算间隔
    /// </summary>
    [SerializeField]
    public float perOpDuration = 0.1f;
    [SerializeField]
    public float perDurationMove = 2f;
    /// <summary>
    /// 下落速度
    /// </summary>
    [SerializeField]
    public float perFrameGrave = 0.2f;
    public float minRotateAngele = -10;
    public float maxRotateAngele = 30;
    Vector3 orginPos;

    Transform manImage;
    private void Start()
    {
        orginPos = transform.localPosition;
        manImage = transform.GetChild(0);
    }

    public void Refresh()
    {
        transform.localPosition = orginPos;
        isInMoveState = false;
        manImage.localEulerAngles = Vector3.zero;
    }
    public Action<Christ010TriggerType> OnTriggerObj;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        string _name = collision.gameObject.name;

        if (_name.Contains("shit"))
        {
            OnTriggerObj?.Invoke(Christ010TriggerType.shit);
            return;
        }
        if (_name.Contains("bottom"))
        {
            OnTriggerObj?.Invoke(Christ010TriggerType.bottom);
            return;
        }
        if (_name.Contains("top"))
        {
            OnTriggerObj?.Invoke(Christ010TriggerType.top);
            return;
        }
        if (_name.Contains("screenBottom"))
        {
            OnTriggerObj?.Invoke(Christ010TriggerType.ScreenBottom);
            return;
        }
        if (transform.localPosition.y - collision.transform.localPosition.y >30)
        {
            isOnBoard = true;
        }
    }

    private bool isOnBoard = false;
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.name.Contains("shit")&& !collision.name.Contains("bottom")&& !collision.name.Contains("top") &&
            !collision.name.Contains("screenBottom"))
            isOnBoard = false;
    }
    private bool gameIsIng
    {
        get { return mRoot.IsGameIng; }
    }
    int moveStateCount = 0;
    private void FixedUpdate()
    {
        if (!gameIsIng)
            return;
        if (!isOnBoard)
        {
            transform.localPosition += new Vector3(0, -perFrameGrave, 0);
        }
        if (isInMoveState)
        {
            mAni.Play("bread");
        }
    }
    bool isInMoveState = false;
    /// <summary>
    /// 
    /// </summary>
    /// <param name="index">0.抬起1.左2.右</param>
    public void OnPointDownMove(int index)
    {
        StopCoroutine("Move");
        if (!gameIsIng)
            return;
        if (index != 0)
        {
            moveStateCount = 0;
            isInMoveState = true;
            Left = index == 1;
            StartCoroutine("Move");
//transform.DOShakeRotation();
        }
        else
            isInMoveState = false;
    }
    private bool Left = false;
    IEnumerator Move()
    {
        while (true)
        {
            if (!gameIsIng)
                yield break;
            float xArgu = Left ? -perDurationMove : perDurationMove;
            transform.localPosition += new Vector3(xArgu, 0, 0);
            Vector3 temp = transform.localPosition;
            if (temp.x < mRoot.screenLeftEdge)
                transform.localPosition =new Vector3(mRoot.screenLeftEdge,temp.y,0);
            if (temp.x > mRoot.screeRightEdge)
                transform.localPosition = new Vector3(mRoot.screeRightEdge, temp.y, 0);
            yield return new WaitForSeconds(perOpDuration);

        }
    }
}
