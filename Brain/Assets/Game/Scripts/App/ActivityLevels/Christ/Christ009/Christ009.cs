using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Christ009 : LevelBasePage
{
    /// <summary>
    /// 默认能量
    /// </summary>
    [SerializeField]
    public float defEnergy = 100;
    private float curEnermy = 0;
    /// <summary>
    /// 每次损耗能量
    /// </summary>
    [SerializeField]
    public int perSpendEnergy = 1;
    /// <summary>
    /// 能量消耗间隔
    /// </summary>
    [SerializeField]
    public float spendEnergyDistance = 0.2f;
    /// <summary>
    /// 每棵树砍掉增加的能量
    /// </summary>
    [SerializeField]
    public int perTreeAddEnergy = 5;

    
    public GameObject EnegyShowObj;
    public Image enegyValue;


    public Christ009SnowMan mSnowMan;

    public GameObject mStartBtn;

    public Text mLevelLabel;

    public Christ009Tree mTree;
    protected override void Start()
    {
        base.Start();
        curEnermy = defEnergy;
        mSnowMan.OnTreeTrigger = OnTriggerTree;
        levelTitleText.enabled = false;
        mLevelLabel.text = levelTitleText.text;
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
    /// <summary>
    /// 使用了tips
    /// </summary>
    bool usedVip = false;
    public override void Refresh()
    {
        base.Refresh();
        gameIng = false; 
        StopCoroutine("EnegyLogic");
        mStartBtn.SetActive(true);
        curEnermy = defEnergy;
        usedVip = false;
        isShowTip = false;
        mTree.Reset();
        mSnowMan.Reset();
    }
    /// <summary>
    /// 能量计算
    /// </summary>
    /// <returns></returns>
    IEnumerator EnegyLogic()
    {
        while (true)
        {
            yield return new WaitForSeconds(spendEnergyDistance);
            if (usedVip)
            {
                StopCoroutine("EnegyLogic");
                yield break;
            }
            if (!isShowTip)
            {
                curEnermy -= perSpendEnergy;
                float scale = curEnermy / defEnergy;
                enegyValue.fillAmount = scale;
                if (curEnermy <= 0)
                {
                    ShowError();
                    mSnowMan.showDead();
                    StopCoroutine("EnegyLogic");
                    After(() => { Refresh(); }, 0.5f);
                }
            }
        }
    }
    void OnTriggerTree()
    {
        ShowError();
        mSnowMan.showDead();
        After(() => { Refresh(); }, 0.5f);
    }
    bool gameIng = false;
    /// <summary>
    /// 点击开始按钮
    /// </summary>
    public void ClickStartBtn()
    {
        if (!usedVip)
            StartCoroutine("EnegyLogic");
        EnegyShowObj.SetActive(!usedVip);
        mStartBtn.SetActive(false);
        gameIng = true;
    }
    bool isShowTip = false;
    void OnShowTip()
    {
        isShowTip = true;
    }
    void OnTipNumModify(int modNum)
    {
        if (modNum == -1)
        {
            usedVip = true;
            EnegyShowObj.SetActive(false);
        }
    }
    void OnCloseLightTip()
    {
        isShowTip = false;
    }
    
    public void clickLeftBtn()
    {
        if (!gameIng)
            return;
        if (mTree.IsShowAni || mSnowMan.isPlayAnimation)
            return;
        mSnowMan.CutAction(0);
        StartCoroutine(treeBeCut(0));
        //bool finish = mTree.BeCut(0);
        //if (finish)
        //{
        //    After(()=>{ Completion(); },0.5f);
        //}
    }
    IEnumerator treeBeCut(int index)
    {
        while (mSnowMan.isPlayAnimation)
        {
            yield return null;
        }
        curEnermy += perTreeAddEnergy;
        bool finish = mTree.BeCut(index);
        if (finish)
        {
            After(() => { Completion(); }, 0.5f);
        }
    }
    public void clickRightBtn()
    {
        if (!gameIng)
            return;
        if (mTree.IsShowAni ||mSnowMan.isPlayAnimation)
            return;
        mSnowMan.CutAction(1);
        StartCoroutine(treeBeCut(1));
        //bool finish = mTree.BeCut(1);
        //if (finish)
        //{
        //    After(() => { Completion(); }, 0.5f);
        //}
    }
    
}
