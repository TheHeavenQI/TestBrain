using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;
public class Level109 : LevelBasePage
{
    List<RectTransform> mPoints;
    public RectTransform mBox;
    public RectTransform mMoveObj;
    public Button mFly;
    EventCallBack mRectEventScript;
    public GameObject mNormal;
    public GameObject mSuc;
    int curFlyStayPosIndex = 0;
    protected override void Start()
    {
        base.Start();
        mPoints = new List<RectTransform>();
        for(int i= 1;i<10;i++)
        {
            mPoints.Add(mBox.GetChild(i) as RectTransform);
        }
        mMoveObj.localPosition = mPoints[0].localPosition;
        curFlyStayPosIndex = 0;
        mRectEventScript = mBox.GetComponent<EventCallBack>();
        mRectEventScript.OnPointDown = onPointDown;
        mRectEventScript.onClick = onClick;
        mRectEventScript.needPressTime = 1f;
        mRectEventScript.onLongPress = onPress;
        mFly.enabled = false;
    }

    Vector2 pointPos;
    void onPointDown(PointerEventData eventData)
    {
        pointPos =ComponentTool.getPointPos(mBox.gameObject,eventData);
    }
    void onClick()
    {
        int ranKey = UnityEngine.Random.Range(0,9);
        while(ranKey == curFlyStayPosIndex)
            ranKey = UnityEngine.Random.Range(0, 10);
        mMoveObj.DOLocalMove(mPoints[ranKey].localPosition,0.5f);
    }
    void onPress()
    {
       Tweener t=  mMoveObj.DOMove(pointPos, 0.5f);
        t.onComplete = () =>
        {
            mFly.enabled = true;
        };
    }
    public void HitFly()
    {
        mNormal.SetActive(false);
        mSuc.SetActive(true);
        Completion();
    }
    public override void Refresh()
    {
        base.Refresh();
        mNormal.SetActive(false);
        mSuc.SetActive(true);
        mMoveObj.localPosition = mPoints[0].localPosition;
        curFlyStayPosIndex = 0;
        mFly.enabled = false;
    }
}
