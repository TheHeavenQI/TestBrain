using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Debug = System.Diagnostics.Debug;

[DisallowMultipleComponent]
public class LevelBasePage : MonoBehaviour {

    public static LevelBasePage Instance;
    private AnalyticsLevelModel levelAnalytics = new AnalyticsLevelModel();
    /// <summary>
    /// 关卡索引 从1开始
    /// </summary>
    [HideInInspector]
    public int levelIndex;
    /// <summary>
    /// 关卡标题
    /// </summary>
    protected Text levelTitleText;
    /// <summary>
    /// 关卡问题
    /// </summary>
    protected Text levelQuestionText;
    /// <summary>
    /// 背景图片
    /// </summary>
    protected Image contentBGImg;
    /// <summary>
    /// RectTransfom
    /// </summary>
    protected RectTransform rectTransform;
    /// <summary>
    /// 阻止点击的GameObject
    /// </summary>
    protected GameObject preventClickGo;
    /// <summary>
    /// 显示成功或失败的动画时间
    /// </summary>
    protected readonly float animTime = 0.7f;
    /// <summary>
    /// 关卡是否已完成
    /// </summary>
    protected bool isLevelComplete;
    /// <summary>
    /// 弹出过关页的间隔时间
    /// </summary>
    protected float delayComplete = 0.5f;
    /// <summary>
    /// 不显示Banner广告的关卡
    /// </summary>
    private HashSet<string> hideBanners = new HashSet<string>()
    {
        "011",
        "043",
        "067",
        "075",
        "105",
        "159",
        "160",
        "210",
        "214",
        "215",
        "219",
        "225",
        "262"
    };

    private int _errorCount;

    protected bool showNativeADWhenFinish = true; 
    protected virtual void Awake() {

        Global.tipSprite = null;
        isLevelComplete = false;
        /// 新关卡出现，停止上一个关卡的成功或错误提示声音
        MusicManager.Instance.StopTipMusic();
        /// 内容背景
        contentBGImg = transform.Find("ContentBG").GetComponent<Image>();
        contentBGImg.transform.SetAsFirstSibling();
        DragMove dragMove = contentBGImg.GetComponent<DragMove>();
        dragMove.enabelDrag = false;
        dragMove.onPointerDown = ShowTap;

        /// 关卡标题
        levelTitleText = contentBGImg.transform.Find("Level").GetComponent<Text>();
        /// 关卡问题
        levelQuestionText = transform.Find("ContentName").GetComponent<Text>();

        /// 阻止点击
        preventClickGo = transform.Find("PreventClick").gameObject;

        rectTransform = transform as RectTransform;

        EventCenter.AddListener(UtilsEventType.LanguageSwitch, LanguageSwitch);

    }

    private Dictionary<string, object> levelParam => new Dictionary<string, object>() { { "level", levelIndex } };

    public void Click_menu() {
        levelAnalytics.click_menu = true;
        AnalyticsUtil.Log("click_menu", levelParam);
    }

    public void Click_levels() {
        levelAnalytics.click_levels = true;
        AnalyticsUtil.Log("click_levels", levelParam);
    }

    public void Click_refresh() {
        levelAnalytics.click_refresh = true;
        AnalyticsUtil.Log("click_refresh", levelParam);
    }

    public void Click_skip() {
        levelAnalytics.click_skip = true;
        AnalyticsUtil.Log("click_skip", levelParam);
    }

    public void Click_tips() {
        levelAnalytics.click_tips = true;
        AnalyticsUtil.Log("click_tips", levelParam);
    }

    public void Ad_tips_skip_show() {
        levelAnalytics.ad_tips_skip_show = true;
        AnalyticsUtil.Log("ad_tips_skip_show", levelParam);
    }

    public void Ad_tips_skip_finish() {
        levelAnalytics.ad_tips_skip_finish = true;
        AnalyticsUtil.Log("ad_tips_skip_finish", levelParam);
    }
    public void Ad_tips_skip_skip() {
        levelAnalytics.ad_tips_skip_skip = true;
        AnalyticsUtil.Log("ad_tips_skip_skip", levelParam);
    }
    public void Ad_tips_skip_close() {
        levelAnalytics.ad_tips_skip_close = true;
        AnalyticsUtil.Log("ad_tips_skip_close", levelParam);
    }
    public void Ad_tips_finish_show() {
        levelAnalytics.ad_tips_finish_show = true;
        AnalyticsUtil.Log("ad_tips_finish_show", levelParam);
    }

    public void Ad_tips_finish_finish() {
        levelAnalytics.ad_tips_finish_finish = true;
        AnalyticsUtil.Log("ad_tips_finish_finish", levelParam);
    }
    public void Ad_tips_finish_skip() {
        levelAnalytics.ad_tips_finish_skip = true;
        AnalyticsUtil.Log("ad_tips_finish_skip", levelParam);
    }
    public void Ad_tips_finish_close() {
        levelAnalytics.ad_tips_finish_close = true;
        AnalyticsUtil.Log("ad_tips_finish_close", levelParam);
    }

    public void Ad_tips_show() {
        levelAnalytics.ad_tips_show = true;
        AnalyticsUtil.Log("ad_tips_show", levelParam);
    }

    public void Ad_tips_finish() {
        levelAnalytics.ad_tips_finish = true;
        AnalyticsUtil.Log("ad_tips_finish", levelParam);
    }
    public void Ad_tips_skip() {
        levelAnalytics.ad_tips_skip = true;
        AnalyticsUtil.Log("ad_tips_skip", levelParam);
    }
    public void Ad_tips_close() {
        levelAnalytics.ad_tips_close = true;
        AnalyticsUtil.Log("ad_tips_close", levelParam);
    }
    public void Ad_use_tips() {
        levelAnalytics.ad_use_tips = true;
        AnalyticsUtil.Log("ad_use_tips", levelParam);
    }
    public void Ad_use_skip() {
        levelAnalytics.ad_use_skip = true;
        AnalyticsUtil.Log("ad_use_skip", levelParam);
    }

    protected virtual void Start() {
#if !Brain_Hero
        ContentController.Instance.RefreshChristButton();
#endif
        levelAnalytics.start_time = Time.realtimeSinceStartup;
        levelQuestionText.transform.SetAsLastSibling();
        preventClickGo.SetActive(false);
        preventClickGo.transform.SetAsLastSibling();
        RefreshBanner();
        if (levelIndex >= 6) {
            Notifications.Instance.RegisterNoti();
        }
        /// 每天第二关，弹一次圣诞提示，第一次安装不弹
        if (Global.playCount == 1) {
            string day = new DateTime().ToShortDateString();
            string oldday = PlayerPrefs.GetString("ShowChristTimeKey", "");
            string oldfirstday = PlayerPrefs.GetString("ShowChristFirstTimeKey", "");
            if (oldfirstday == "") {
                PlayerPrefs.SetString("ShowChristTimeKey",oldfirstday);
            }
            if (day != oldday && oldfirstday != oldday) {
                PlayerPrefs.SetString("ShowChristTimeKey",day);
                PopUpManager.Instance.ShowChrist();
            }
        }

        if (levelIndex == 20 || levelIndex > 20 && Global.playCount % 8 == 0 && Global.playCount > 1) {
            int limitedTime = UtilsLimitedTime.LimitedTime();
            if (limitedTime > 10) {
                PopUpManager.Instance.ShowLimitedTime(limitedTime);
            }
        }
    }
    
    
    public void RefreshBanner() {
        string name = prefabName;
        if (Global.christmas || hideBanners.Contains(name)) {
            ADManager.CloseAD(GameAdID.Banner);
        } else {
            ADManager.ShowAD(GameAdID.Banner);
        }
    }
    public void InitLevel(int level) {
        levelIndex = level;
        QuestionModel model =  ConfigManager.Current().GetQuestionModel(levelIndex-1);
        int id = model.id;
        prefabName = model.name;
        levelTitleText.text = $"Lv.{id}";
        LanguageSwitch();
    }
    public string prefabName {
        get;
        private set;
    }

    public void SetEnableClick(bool enable) {
        preventClickGo.SetActive(!enable);
    }

    /// <summary>
    /// 切换语言
    /// </summary>
    public virtual void LanguageSwitch() {
        QuestionModel model = ConfigManager.Current().GetQuestionModel(levelIndex - 1);
        levelQuestionText.text = model.question;
    }

    /// <summary>
    /// 关卡刷新
    /// </summary>
    public virtual void Refresh() {
        isLevelComplete = false;
#if Brain_Hero
        int rIndex = UnityEngine.Random.Range(0,2);
        if(rIndex == 0)
            ADManager.ShowAD(GameAdID.Interstitial);
#endif
    }

    /// <summary>
    /// 显示错误,muti 是否显示多点
    /// </summary>
    public void ShowErrorWithMousePosition(bool muti = false) {
        if (muti) {
            for (int i = 0; i < Input.touchCount; i++) {
                if (RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, Input.GetTouch(i).position, Camera.main, out Vector2 outVec)) {
                    ShowError(outVec);
                }
            }
        } else if (Input.touchCount <= 1) {
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, Input.mousePosition, Camera.main, out Vector2 outVec)) {
                ShowError(outVec);
            }
        }
    }
#if Brain_Hero
    private uint MaxErrorCount = 2;
#else
    private uint MaxErrorCount = 5;
#endif
    /// <summary>
    /// 显示错误
    /// </summary>
    /// <param name="localPos">相对与LevelBasePage的本地坐标</param>
    public void ShowError(Vector3 localPos = default) {
        _errorCount++;
        UtilsHud.ShowError(transform, localPos);
        MusicManager.Instance.PlayErrorMusic();
        if(_errorCount > MaxErrorCount && _errorCount % MaxErrorCount == 0)
        {
            ADManager.ShowAD(GameAdID.Interstitial);
        }
        if (_errorCount == 6) {
            EventCenter.Broadcast(UtilsEventType.OnQuestionMutiError);
        }
    }

    /// <summary>
    /// 显示成功
    /// </summary>
    /// <param name="localPos">相对与LevelBasePage的本地坐标</param>
    public void ShowSuccess(Vector3 localPos = default) {
        UtilsHud.ShowSuccess(transform, localPos);
        MusicManager.Instance.PlaySuccessMusic();
    }

    /// <summary>
    /// 显示点击屏幕效果
    /// </summary>
    private void ShowTap() {
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, Input.mousePosition, Camera.main, out Vector2 outVec)) {
            UtilsHud.ShowTap(transform, outVec);
        }
    }

    /// <summary>
    /// 显示成功（成功提示显示在鼠标位置）
    /// </summary>
    public void ShowSuccessWithMousePosition() {
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, Input.mousePosition, Camera.main, out Vector2 outVec)) {
            ShowSuccess(outVec);
        }
    }

    /// <summary>
    /// 过关时回调，在完成页出现之前调用
    /// </summary>
    protected virtual void OnCompletion() {
        
    }

    /// <summary>
    /// 显示成功，并显示完成页面（成功提示显示在鼠标位置）
    /// </summary>
    public void CompletionWithMousePosition() {
        if (isLevelComplete) {
            return;
        }
        isLevelComplete = true;

        OnCompletion();
        ShowSuccessWithMousePosition();
        SetEnableClick(false);
        After(() => {
            ShowFinishPopUpAnim();
        }, delayComplete);
    }

    /// <summary>
    /// 显示成功，并显示完成页面（成功提示显示在屏幕中心）
    /// </summary>
    /// <param name="localPos">相对与LevelBasePage的本地坐标</param>
    public void Completion(Vector3 localPos = default) {
        if (isLevelComplete) {
            return;
        }
        isLevelComplete = true;
        OnCompletion();
        ShowSuccess(localPos);
        SetEnableClick(false);
        After(() => {
            ShowFinishPopUpAnim();
        }, delayComplete);
    }
    /// <summary>
    /// 延迟执行
    /// </summary>
    public Coroutine After(Action action, float delay) {
        return StartCoroutine(AfterDoEvent(action, delay));
    }

    private void ShowFinishPopUpAnim() {
        UserModel.SaveLevel(levelIndex+1);
        Global.playCount++;
        levelAnalytics.end_time = Time.realtimeSinceStartup;
        AnalyticsUtil.Log($"{Global.GetAnalyticsPrefix()}scn_lv_show_finish_{levelIndex}", levelAnalytics.ToDict());
        After(() => {
            UtilsLog.LogWarning($"showNative: Finish{showNativeADWhenFinish}");
            PopUpManager.Instance.ShowFinish(levelIndex,showNativeADWhenFinish);
        }, animTime);
    }
    private IEnumerator AfterDoEvent(Action action, float delay) {
        yield return new WaitForSeconds(delay);
        action?.Invoke();
    }

    public void Destroy() {
        levelAnalytics.end_time = Time.realtimeSinceStartup;
        AnalyticsUtil.Log($"{Global.GetAnalyticsPrefix()}scn_lv{levelIndex}", levelAnalytics.ToDict());
        EventCenter.RemoveListener(UtilsEventType.LanguageSwitch, LanguageSwitch);
    }
}
