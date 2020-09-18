using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Christ002 : LevelBasePage
{
    /// <summary>
    /// 初始速度
    /// </summary>
    [SerializeField]
    public float DefSpeed = 1;
    /// <summary>
    /// 当前移动速度
    /// </summary>
    private float curMoveSpeed = 1;
    /// <summary>
    /// 默认加速度
    /// </summary>
    [SerializeField]
    public float DefAccSpeed = 0.01f;
    [SerializeField]
    public float VipAccSpeed = 0.005f;

    public GameObject mStartBtn;

    /// <summary>
    /// 移动背景
    /// </summary>
    public RectTransform mMoveBg;
    /// <summary>
    /// 完成任务目标点
    /// </summary>
    float TargetPoint;
    float bgDefY;
    public int perLineDistance = 120;
    /// <summary>
    /// 圣诞老人
    /// </summary>
    public chrismasMan mChrismasMan;
    RectTransform manRect;
    Vector3 chrismasDefPos = Vector3.zero;
    protected override void Start()
    {
        base.Start();
        curMoveSpeed = DefSpeed;
        TargetPoint = -mMoveBg.sizeDelta.x + 800;
        bgDefY = mMoveBg.anchoredPosition.y;
        manRect = mChrismasMan.transform as RectTransform;
        chrismasDefPos = manRect.anchoredPosition;
        mChrismasMan.onTriggerSnowMan = onTriggerSnowMan;
    }
    bool isGameIng = false;
    bool usedVip = false;
    public void ClickStartBtn()
    {
        if (isGameIng)
            return;
        isGameIng = true;
        mStartBtn.SetActive(false);
    }
    /// <summary>
    /// 圣诞老人所处于得格子，-1.down,0.middle,1.up
    /// </summary>
    private int ManStayIndex = 0;
    public void ClickUp()
    {
        if (ManStayIndex == 1)
            return;
        ManStayIndex += 1;
        manRect.anchoredPosition += new Vector2(0,perLineDistance);
        SetManDepth();
    }
    public void ClickDown()
    {
        if (ManStayIndex == -1)
            return;
        ManStayIndex -= 1;
        manRect.anchoredPosition -= new Vector2(0, perLineDistance);
        SetManDepth();
    }
    void SetManDepth()
    {
        switch (ManStayIndex)
        {
            case 1:
                mChrismasMan.SetSortOrder(103);
                break;
            default:
                mChrismasMan.SetSortOrder(105);
                break;
        }
    }
    bool isShowTip = false;
    /// <summary>
    /// 是否触发胜利或失败
    /// </summary>
    void onTriggerSnowMan()
    {
        ShowError();
        isGameIng = false;
        After(() => { Refresh(); }, 1);
    }
    public override void Refresh()
    {
        base.Refresh();
        isGameIng = false;
        mStartBtn.SetActive(true);
        manRect.anchoredPosition = chrismasDefPos;
        mMoveBg.anchoredPosition = new Vector2(0,bgDefY);
        curMoveSpeed = DefSpeed;
        ManStayIndex = 0;
        usedVip = false;
        SetManDepth();
        moveFrameCount = 0;
    }
    /// <summary>
    /// 移动帧数
    /// </summary>
    int moveFrameCount = 0;
    private void FixedUpdate()
    {
        if (!isGameIng || isShowTip)
            return;
        mMoveBg.anchoredPosition -= new Vector2(curMoveSpeed,0);
        float perFrameSpeed = usedVip ? VipAccSpeed : DefAccSpeed;
        moveFrameCount += 1;
        if(moveFrameCount %30 == 0)
            curMoveSpeed += perFrameSpeed;
        if (mMoveBg.anchoredPosition.x < TargetPoint)
        {
            showSuc();
        }
    }
    void showSuc()
    {
        isGameIng = false;
        After(()=> { Completion(); },1f);
    }
    private void OnEnable()
    {
        EventCenter.AddListener(UtilsEventType.OnTipsDialogShow, OnShowTip);
        EventCenter.AddListener<int>(UtilsEventType.OnTipNumModify, OnTipNumModify);
        EventCenter.AddListener(UtilsEventType.OnTipsDialogClose, OnCloseLightTip);
    }
    private void OnDisable()
    {
        EventCenter.RemoveListener(UtilsEventType.OnTipsDialogShow, OnShowTip);
        EventCenter.RemoveListener<int>(UtilsEventType.OnTipNumModify, OnTipNumModify);
        EventCenter.RemoveListener(UtilsEventType.OnTipsDialogClose, OnCloseLightTip);
    }
    void OnShowTip()
    {
        isShowTip = true;
    }
    void OnTipNumModify(int modNum)
    {
        if (modNum == -1)
        {
            usedVip = true;
            if (isGameIng)
            {
                curMoveSpeed = DefSpeed;
            }
        }
    }
    void OnCloseLightTip()
    {
        isShowTip = false;
    }
}
