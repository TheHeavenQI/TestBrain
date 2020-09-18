using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using DG.Tweening;
using clientCore;
using Random = UnityEngine.Random;
using SceneManager = UnityEngine.SceneManagement.SceneManager;

public class ContentController : BaseController
{
    protected override bool autoShow => false;
    [HideInInspector]
    public LevelBasePage currentLevelPage;
    [HideInInspector]
    public int levelIndex;
    private Text _tipNum;
    private Text _strengthNum;
    public GameObject menuPrefab;
    public static ContentController Instance;
    private Button _skip;
    private DOTweenAnimation _watchADTipsAnim;
    private Button _christButton;
    private GameObject _christDotGameObject;

    private GameObject _skip_ad;
    private GameObject _skip_ad_hide;


    public override void Awake()
    {
        base.Awake();
        Instance = this;
    }

    public override void Start()
    {
        StartCoroutine(Init());
#if Brain_Hero
        GameManager.instance.Role.OnStrenthUpdate += ShowStrength;
#endif

        After(() =>
        {
            Utils.GiveTips();
        }, 3);
    }
    void ShowStrength(int num)
    {
        if (_strengthNum != null)
            _strengthNum.text = string.Format("Strength:{0}", num);
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        var a = transform.GetComponent<Canvas>();
        if (a)
        {
            a.sortingOrder = 1;
        }
    }
    private void OnDestroy()
    {
        EventCenter.RemoveListener<int>(UtilsEventType.OnTipNumModify, TipNumModify);
        EventCenter.RemoveListener(UtilsEventType.NextLevel, mShowLevelCall);
        EventCenter.RemoveListener(UtilsEventType.OnQuestionMutiError, OnQuestionMutiError);
#if Brain_Hero
        GameManager.instance.Role.OnStrenthUpdate -= ShowStrength;
#endif
    }
    void mShowLevelCall()
    {
        levelIndex++;
        ShowLevel();
    }
    Transform tipTrans;
    public Transform mTipLightTrans
    {
        get
        {
            return tipTrans;
        }
    }
    private IEnumerator Init()
    {
        yield return 0;

        var menuGameObject = Instantiate(menuPrefab);
        menuGameObject.transform.SetParent(transform, false);
        menuGameObject.name = "Menu";
        yield return 0;

        var refresh = transform.Find("Menu/Refresh").GetComponent<Button>();
        refresh.onClick.AddListener(Refresh);

        var menu = transform.Find("Menu/Menu").GetComponent<Button>();
        menu.onClick.AddListener(Menu);

        var select = transform.Find("Menu/Select").GetComponent<Button>();
        select.onClick.AddListener(Select);

        var tipNum = transform.Find("Menu/TipNum").GetComponent<Button>();
        tipNum.onClick.AddListener(TipNum);
        tipTrans = tipNum.transform.GetChild(1);

        _skip = transform.Find("Menu/Skip").GetComponent<Button>();
        _skip.onClick.AddListener(Skip);

        _skip_ad = transform.Find("Menu/Skip/AD").gameObject;
        _skip_ad_hide = transform.Find("Menu/Skip/ADHide").gameObject;


        _tipNum = transform.Find("Menu/TipNum/Text").GetComponent<Text>();
#if Brain_Hero
        _strengthNum = transform.Find("Menu/Strength").GetComponent<Text>();
#endif
        _christButton = transform.Find("Menu/Christ").GetComponent<Button>();
        _christDotGameObject = transform.Find("Menu/Christ/dot").gameObject;

        yield return 0;

        var model = UserModel.Get();
        var level = model.levelId;
        if (level <= 1)
        {
            level = 1;
        }
        levelIndex = level;
        _tipNum.text = $"X {model.keyCount}";
        yield return 0;

        ShowLevelFirstLevel();
        yield return 0;
        
        _christButton.onClick.AddListener(() =>
        {
            ControllerManager.Instance.GetController<LevelSelectChristmasController>().gameObject.SetActive(true);
        });

        EventCenter.AddListener(UtilsEventType.OnQuestionMutiError, OnQuestionMutiError);
        CheckAD();
        ADManager.onRewardADChange += CheckAD;
        

#if Brain_Hero
        ShowStrength(GameManager.instance.Role.StrengthCount);
#endif

    }

    private void CheckAD()
    {
        _skip_ad_hide.SetActive(false);
        _skip_ad.SetActive(false);
    }

    public void RefreshChristButton()
    {
        int enterChrist = PlayerPrefs.GetInt(Constance.storage_enterChrist, 0);
        _christDotGameObject.SetActive(enterChrist == 0);
        //_christButton.gameObject.SetActive(!Global.christmas);
    }
    private void OnQuestionMutiError()
    {
        int clickGetTip = PlayerPrefs.GetInt(Constance.storage_clickGetTip, 0);
        if (_watchADTipsAnim == null && clickGetTip == 0)
        {
            _watchADTipsAnim = transform.Find("Menu/TipNum/WatchAD").GetComponent<DOTweenAnimation>();
            _watchADTipsAnim.gameObject.SetActive(true);
            _watchADTipsAnim.DOPlay();
        }
    }

    private void ShowLevelFirstLevel()
    {
        if (Utils.FirstOpenApp())
        {
            AnalyticsUtil.Log("ShowFirstLevel");
        }
        AnalyticsUtil.Log("ShowContent");
        ShowLevel();
        EventCenter.AddListener<int>(UtilsEventType.OnTipNumModify, TipNumModify);
        EventCenter.AddListener(UtilsEventType.NextLevel, mShowLevelCall);
    }
    /// <summary>
    /// 跳过
    /// </summary>
    private void Skip()
    {
        _skip.enabled = false;
        After(() => { _skip.enabled = true; }, 1);
        LevelBasePage.Instance.Click_skip();
        var model = UserModel.Get();

        if (model.keyCount >= 2)
        {
            levelIndex++;
            ShowLevel();
            EventCenter.Broadcast(UtilsEventType.OnTipNumModify, -2);
            LevelBasePage.Instance.Ad_use_skip();
            AnalyticsUtil.Log($"{Global.GetAnalyticsPrefix()}scn_all_skip");
        }
        else
        {
            PopUpManager.Instance.ShowSkip();
        }
    }
    /// <summary>
    /// 提示
    /// </summary>
    private void TipNum()
    {
        if (_watchADTipsAnim != null)
        {
            _watchADTipsAnim.DOKill();
            Destroy(_watchADTipsAnim.gameObject);
            _watchADTipsAnim = null;
        }
        PlayerPrefs.SetInt(Constance.storage_clickGetTip, 1);
        LevelBasePage.Instance.Click_tips();
        var model = UserModel.Get();
        EventCenter.Broadcast(UtilsEventType.OnTipsDialogShow);
        if (model.keyCount >= 1)
        {
            EventCenter.Broadcast(UtilsEventType.OnTipNumModify, -1);
            PopUpManager.Instance.ShowTip(levelIndex);
            LevelBasePage.Instance.Ad_use_tips();
            AnalyticsUtil.Log($"{Global.GetAnalyticsPrefix()}scn_all_tips");
        }
        else
        {
            PopUpManager.Instance.ShowGetTip();
        }
    }
    private void Select()
    {
#if Brain_Hero
        SceneManager.LoadScene("MainScene");
#else
        LevelBasePage.Instance.Click_levels();
        ControllerManager.Instance.GetController<LevelSelectController>().gameObject.SetActive(true);
#endif
        //if (Global.christmas) {
        //    ControllerManager.Instance.GetController<LevelSelectChristmasController>().gameObject.SetActive(true);
        //}
    }
    private void Menu()
    {
        LevelBasePage.Instance.Click_menu();
        ControllerManager.Instance.GetController<MenuController>().gameObject.SetActive(true);
    }

    public void ShowLevel()
    {
        if (levelIndex > ConfigManager.Current().GetQuestionCount())
        {
            levelIndex = 1;
        }
        var a = GetLevel(levelIndex);
        if (currentLevelPage)
        {
            currentLevelPage.Destroy();

            Destroy(currentLevelPage.gameObject);
        }
        UserModel.SaveLevel(levelIndex);
        currentLevelPage = a.GetComponent<LevelBasePage>();
        LevelBasePage.Instance = currentLevelPage;
        currentLevelPage.InitLevel(levelIndex);

        int ran = Random.Range(0, 100);
        if (ran <= 65)
        {
            ShowInterstitialAD(levelIndex);
        }

        notifyUseTip(currentLevelPage.prefabName);
    }

    private void ShowInterstitialAD(int levelIndex)
    {
        if (levelIndex > 10 || Global.christmas)
        {
            var b = new ADNotify();
            b.onAdClick = () =>
            {
                AnalyticsUtil.Log($"{Global.GetAnalyticsPrefix()}ad_ad_interstitial_click");
            };
            b.onAdClose = () =>
            {
                AnalyticsUtil.Log($"{Global.GetAnalyticsPrefix()}ad_ad_interstitial_close");
            };
            Utils.ADInterstitialSuceessRate();
            if (ADManager.ShowAD(GameAdID.Interstitial, b))
            {
                AnalyticsUtil.Log($"{Global.GetAnalyticsPrefix()}ad_ad_interstitial_show");
            }
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="i"></param>
    /// <returns></returns>
    private GameObject GetLevel(int i)
    {
        var model = ConfigManager.Current().GetQuestionModel(i - 1);
        string name = $"Level{i}";
        GameObject prefab = null;
        UtilsLog.LogWarning($"model.name:{model.name}");
        if (Global.christmas)
        {
            prefab = Resources.Load<GameObject>($"ActivityLevels/Christ/{model.name}");
        }
        else
        {
            int nameNum = int.Parse(model.name);
            string levelfolder = (((nameNum / 50) + 1) * 50).ToString("000");
            prefab = Resources.Load<GameObject>($"Levels/Level{levelfolder}/{model.name}");
        }
        if (prefab == null)
        {
            prefab = Resources.Load<GameObject>($"Main/EmptyLevel");
        }
        GameObject obj = Instantiate(prefab);
        obj.name = name;
        obj.transform.SetParent(_content.transform, false);
        return obj;
    }

    private void TipNumModify(int count)
    {
        var model = UserModel.Get();
        model.keyCount += count;
        if (model.keyCount < 0)
        {
            model.keyCount = 0;
            Debug.LogError($"model.keyCount < 0 : {model.keyCount}");
        }
        UserModel.Save(model);
        _tipNum.text = $"X {model.keyCount}";
    }
    private void Refresh()
    {
        LevelBasePage.Instance.Click_refresh();
        currentLevelPage.Refresh();
        notifyUseTip(currentLevelPage.prefabName);
    }
#region noticeTipAni
    bool NotNeedTipAni(string levelName)
    {
        string numStr = levelName.Replace("Level", "");
        return NotNeedTipAniLevels.Contains(numStr);
    }

    readonly List<string> NotNeedTipAniLevels = new List<string>() { "090", "166", "168" };
    Tweener tipTween;
    float refreshLevelTimeStamp;
    void notifyUseTip(string levelName)
    {
        resetTipTween();
        refreshLevelTimeStamp = Time.time;
        if (!NotNeedTipAni(levelName))
            StartCoroutine("shakeTip");
    }
    IEnumerator shakeTip()
    {
        while (Time.time - refreshLevelTimeStamp < 4)
            yield return null;
        tipTween = tipTrans.DOShakeScale(0.5f, new Vector3(0.3f, 0.3f, 0));
        tipTween.SetLoops(-1, LoopType.Yoyo);
    }
    void resetTipTween()
    {
        StopCoroutine("shakeTip");
        if (tipTween != null)
            tipTween.Kill();
        tipTrans.localScale = Vector3.one;
    }
#endregion

    public void GetController<T>()
    {
        throw new NotImplementedException();
    }
}
