using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class Christ004 : LevelBasePage
{
    [SerializeField]
    public float leftEdge = -300;
    [SerializeField]
    public float rightEdge = 300;
    [SerializeField]
    public float refreshDuration = 1f;
    public Christ004Tap mTap;
    RectTransform tapRect;
    public Transform mGiftItem;
    public Transform mShitItem;
    public GameObject mStartBtn;
    [SerializeField]
    public int needGetGiftNum = 5;
    private int curGetNum = 0;
    protected override void Awake()
    {
        base.Awake();
        mGiftItem.gameObject.SetActive(false);
        mShitItem.gameObject.SetActive(false);
    }
    protected override void Start()
    {
        base.Start();
        tapRect = mTap.transform as RectTransform;
        mTap.OnGetGift = OnGetGift;
        mTap.OnTriggerShit = OnTriggerShit;
        levelQuestionText.text = string.Format("Catch {0} candies.",needGetGiftNum);
        Canvas titleCa = levelQuestionText.gameObject.AddComponent<Canvas>();
        titleCa.overrideSorting = true;
        titleCa.sortingOrder = 5;
    }
    public void ClickStart()
    {
        IsGameIng = true;
        mStartBtn.SetActive(false);
        levelTitleText.enabled = false;
        mTap.GetComponent<LimitDragMoveEventTrigger>().Return2OriginPos();
    }
    /// <summary>
    /// 礼物掉落权重，初始值为1
    /// </summary>
    private int GiftDropWeight = 1;
    /// <summary>
    /// 是否弹窗中
    /// </summary>
    private bool isShowTip = false;
    /// <summary>
    /// 是否游戏中
    /// </summary>
    private bool IsGameIng = false;

    public override void Refresh()
    {
        base.Refresh();
        GiftDropWeight = 1;
        curGetNum = 0;
        isShowTip = false;
        IsGameIng = false;
        mStartBtn.SetActive(true);
        levelTitleText.enabled = true;
        mTap.GetComponent<LimitDragMoveEventTrigger>().Return2OriginPos();
        levelQuestionText.text = string.Format("Catch {0} candies.", needGetGiftNum);
        ClearItem();
    }
    /// <summary>
    /// 接到一个礼物
    /// </summary>
    void OnGetGift(GameObject go)
    {
        curGetNum += 1;
        if (curGetNum >= needGetGiftNum)
        {
            PauseTween();
            IsGameIng = false;
            After(() => { Completion(); }, 0.5f);
        }
        go.SetActive(false);
        Tweener t = tapRect.DOShakeScale(0.2f,0.5f);
        t.onComplete = () =>
        {
            tapRect.localScale = Vector3.one;
        };
        levelQuestionText.text = string.Format("Catch {0} candies.", needGetGiftNum - curGetNum);
    }
    //
    void OnTriggerShit(GameObject go)
    {
        if (isLevelComplete)
            return;
        ShowError();
        Refresh();
    }
    float ShowTipTimeStamp = 0;
    void OnShowTip()
    {
        isShowTip = true;
        ShowTipTimeStamp = Time.time;
        PauseTween();
    }
    void OnTipNumModify(int modNum)
    {
        if (modNum == -1)
            GiftDropWeight += 1;
    }
    void OnCloseLightTip()
    {
        TimeStamp += (Time.time - ShowTipTimeStamp);
        isShowTip = false;
        PlayTween();
    }
    private void OnEnable()
    {
        EventCenter.AddListener(UtilsEventType.OnTipsDialogShow, OnShowTip);
        EventCenter.AddListener<int>(UtilsEventType.OnTipNumModify,OnTipNumModify);
        EventCenter.AddListener(UtilsEventType.OnTipsDialogClose, OnCloseLightTip);
    }
    private void OnDisable()
    {
        EventCenter.RemoveListener(UtilsEventType.OnTipsDialogShow, OnShowTip);
        EventCenter.RemoveListener<int>(UtilsEventType.OnTipNumModify,OnTipNumModify);
        EventCenter.RemoveListener(UtilsEventType.OnTipsDialogClose,OnCloseLightTip);
    }
    /// <summary>
    /// 生成物体时间戳
    /// </summary>
    private float TimeStamp = 0;
    List<Itemmove> tweeners = new List<Itemmove>();
    List<GameObject> items = new List<GameObject>();
    #region item生成
    private void FixedUpdate()
    {
        if (isLevelComplete)
            return;
        if (!IsGameIng)
            return;
        if (isShowTip)
            return;
        if (Time.time - TimeStamp > refreshDuration)
        {
            ProductItem();
            TimeStamp = Time.time;
        }
    }
    private void ProductItem()
    {
        int randomKey = Random.Range(0,1+GiftDropWeight);
        if (randomKey == 0)//shit
        {
            Transform shitItem = Instantiate(mShitItem);
            normalrizeObj(shitItem,mShitItem);
        }
        else //gift
        {
            Transform giftItem = Instantiate(mGiftItem);
            normalrizeObj(giftItem, mGiftItem);
        }
    }
    void normalrizeObj(Transform newGo,Transform item)
    {
        newGo.SetParent(item.parent);
        float x = Random.Range(leftEdge, rightEdge);
        newGo.localScale = Vector3.one;
        newGo.localEulerAngles = Vector3.zero;
        RectTransform rect = newGo as RectTransform;
        Vector3 orignPos = new Vector3(x, (item as RectTransform).anchoredPosition.y,0);
        rect.anchoredPosition3D = orignPos;
        Itemmove t = rect.GetComponent<Itemmove>();
        tweeners.Add(t);
        items.Add(newGo.gameObject);
        t.onComplete = (thego) =>
        {
            Destroy(newGo.gameObject);
            tweeners.Remove(t);
            items.Remove(newGo.gameObject);
        };
        newGo.gameObject.SetActive(true);
    }
    private void ClearItem()
    {
        tweeners.Clear();
        foreach (var item in items)
        {
            Destroy(item);
        }
        items.Clear();
    }
    /// <summary>
    /// 暂停动画
    /// </summary>
    private void PauseTween()
    {
        foreach (var item in tweeners)
            item.Pause();
    }
    private void PlayTween()
    {
        foreach (var item in tweeners)
            item.Play();
    }
    #endregion
}
