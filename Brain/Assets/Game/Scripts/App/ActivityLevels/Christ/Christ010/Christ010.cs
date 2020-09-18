using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

public class Christ010 : LevelBasePage
{
    [SerializeField]
    public float screenLeftEdge = -375;
    [SerializeField]
    public float screeRightEdge = 375;
    /// <summary>
    /// 初始速度
    /// </summary>
    [SerializeField]
    public float DefSpeed = 1;
    /// <summary>
    /// 当前移动速度
    /// </summary>
    private float curMoveSpeed = 1;
    float TargetPoint;
    /// <summary>
    /// 默认加速度
    /// </summary>
    [SerializeField]
    public float DefAccSpeed = 0.01f;
    [SerializeField]
    public float VipAccSpeed = 0.005f;
    public RectTransform mMap;


    public Transform marryObj;



    public EventCallBack leftBtn;
    public EventCallBack rightBtn;
    public GameObject mStartBtn;
    public Christ010Man mMan;



    private bool _isGameIng = false;
    public bool IsGameIng
    {
        get
        {
            return _isGameIng;
        }
    }

    private void Update()
    {

    }
    private bool isToBottom = false;
    /// <summary>
    /// 移动帧数
    /// </summary>
    int moveFrameCount = 0;
    private void FixedUpdate()
    {
        if (!_isGameIng || isShowTip)
            return;
        if (isToBottom)
            return;
        mMap.anchoredPosition += new Vector2(0, curMoveSpeed);
        float perFrameSpeed = usedVip ? VipAccSpeed : DefAccSpeed;
        moveFrameCount += 1;
        if (moveFrameCount % 30 == 0)
            curMoveSpeed += perFrameSpeed;
        if (mMap.anchoredPosition.y > TargetPoint)
        {
           isToBottom = true;
        }
    }
    protected override void Start()
    {
        base.Start();
        showNativeADWhenFinish = false;
        curMoveSpeed = DefSpeed;
        RectTransform bottomLand = mMap.GetChild(mMap.childCount -1) as RectTransform;
        TargetPoint = -bottomLand.anchoredPosition.y - 150;//预留150像素
        AddBtnListener();
        mMan.OnTriggerObj = trigger;
    }
    #region 触发物体
    void trigger(Christ010TriggerType _type)
    {
        switch (_type)
        {
            case Christ010TriggerType.bottom:
                mCompletion();
                break;
            case Christ010TriggerType.ScreenBottom:
            case Christ010TriggerType.shit:
            case Christ010TriggerType.top:
                //mCompletion();
                ShowError();
                _isGameIng = false;
                After(() => { Refresh(); }, 1f);
                break;
                
        }
    }
    
    #endregion
    public void ClickStartBtn()
    {
        _isGameIng = true;
        mStartBtn.SetActive(false);
    }
    public override void Refresh()
    {
        base.Refresh();
        mStartBtn.SetActive(true);
        mMan.Refresh();
        curMoveSpeed = DefSpeed;
        mMap.anchoredPosition = Vector2.zero;
        moveFrameCount = 0;
        _isGameIng = false;
        usedVip = false;
        isToBottom = false;
        isShowTip = false;
        marryObj.gameObject.SetActive(false);
    }
    #region 按钮
    void AddBtnListener()
    {
        leftBtn.OnPointDown = onLeftBtnPointDown;
        leftBtn.onPointerUp = onDirBtnPointUp;
        rightBtn.OnPointDown = onRightBtnPointDown;
        rightBtn.onPointerUp = onDirBtnPointUp;
    }
    void onLeftBtnPointDown(PointerEventData dt)
    {
        if (rightBtn.isPressing)
            return;
        mMan.OnPointDownMove(1);
    }
    void onDirBtnPointUp()
    {
        mMan.OnPointDownMove(0);
    }
    void onRightBtnPointDown(PointerEventData dt)
    {
        if (leftBtn.isPressing)
            return;
        mMan.OnPointDownMove(2);
    }
    #endregion
    public void ClickLeftBtn()
    {
        mMan.transform.localPosition += new Vector3(-5,0,0);
    }
    public void ClickRightBtn()
    {
        mMan.transform.localPosition += new Vector3(5, 0, 0);
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
    bool isShowTip = false;
    bool usedVip = false;
    void OnShowTip()
    {
        isShowTip = true;
    }
    void OnTipNumModify(int modNum)
    {
        if (modNum == -1)
        {
            usedVip = true;
            if (_isGameIng)
            {
                curMoveSpeed = DefSpeed;
            }
        }
    }
    void OnCloseLightTip()
    {
        isShowTip = false;
    }
    #region marry
    readonly string acMarryKey = "actMarryGetReward";
    void mCompletion()
    {
        _isGameIng = false;
        Completion();
        if (!PlayerPrefs.HasKey(acMarryKey))
        {
            After(()=> { showMarry(); },0.5f);
        }
    }

    protected override void Awake() {
        base.Awake();
        showNativeADWhenFinish = false;
    }

    void showMarry()
    {
        Transform temp = Resources.Load<Transform>("marry");
        Transform marry = Instantiate(temp);
        marry.SetParent(PopUpManager.Instance.transform);
        marry.localScale = Vector3.one;
        marry.localPosition = Vector3.zero;
        marry.localEulerAngles = Vector3.zero;
        marry.gameObject.SetActive(true);
        marry.SetAsLastSibling();
        marryObj.gameObject.SetActive(true);
        marryObj.transform.SetAsLastSibling();
        Transform targetPoint = ContentController.Instance.mTipLightTrans;
        
        foreach (Transform child in marryObj)
        {
            Tweener t = child.DOMove(targetPoint.position, 0.3f);
            t.onComplete = () =>
            {
                child.gameObject.SetActive(false);
            };
        }
        PlayerPrefs.SetInt(acMarryKey, 1);
        EventCenter.Broadcast(UtilsEventType.OnTipNumModify,5);
    }
    #endregion
}
