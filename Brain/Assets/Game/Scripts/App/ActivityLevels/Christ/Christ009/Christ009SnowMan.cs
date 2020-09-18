using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Christ009SnowMan : MonoBehaviour
{

    public Action OnTreeTrigger;
    public Animation mAnimation;

    public GameObject deadObj;
    public GameObject[] activeObj;
    bool bornLeft;
    Vector3 leftpos = Vector3.zero;
    Vector3 rightPos = Vector3.zero;
    private void Start()
    {
        curStayLeft = bornLeft;
        Vector3 temp = transform.localPosition;
        bornLeft = (transform as RectTransform).anchoredPosition.x < 0;
        leftpos = bornLeft? temp : new Vector3(-temp.x,temp.y,0);
        rightPos = bornLeft? new Vector3(-temp.x, temp.y, 0): temp;
        foreach (AnimationState state in mAnimation)
        {
            state.speed = 3;
        }

    }
    public void showDead()
    {
        deadObj.SetActive(true);
        foreach (var obj in activeObj)
            obj.SetActive(false);
    }
    public void Reset()
    {
        transform.localPosition = bornLeft ? leftpos : rightPos;
        int scalex = bornLeft ? -1 : 1;
        transform.localScale = new Vector3(scalex,1,1);
        mAnimation.Stop();
        mAnimation.Rewind();
        deadObj.SetActive(false);
        foreach (var obj in activeObj)
            obj.SetActive(true);
    }
    private bool curStayLeft = false; 
    /// <summary>
    /// 
    /// </summary>
    /// <param name="btnDirIndex">0.左边，1.右边</param>
    public void CutAction(int btnDirIndex)
    {
        transform.localPosition = btnDirIndex == 0 ? leftpos : rightPos;
        int scalex = btnDirIndex == 0 ? -1 : 1;
        transform.localScale = new Vector3(scalex, 1, 1);
        playAni();
    }
    public bool isPlayAnimation
    {
        get
        {
            return mAnimation.isPlaying;
        }
    }
    void playAni()
    {
        mAnimation.Play();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnTreeTrigger?.Invoke();
    }
}
