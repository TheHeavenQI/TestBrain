
using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class FinishPopUp : BasePopUp
{
    private int _level;
    public Button receiveBtn;
    public GameObject receiveObject;
    public Button nextBtn;
    public ParticleSystem particle;
    public Image nextBtnImage;
    public Text finishText;
    public override void Awake()
    {
        base.Awake();
#if Brain_Hero
        receiveObject.SetActive(false);
#endif
        nextBtn.onClick.AddListener(() =>
        {
#if Brain_Hero
            if (clientCore.GameManager.instance.CostStrenth())
            {
#endif
            Hide();
            LevelBasePage.Instance.Ad_tips_finish_close();
            EventCenter.Broadcast(UtilsEventType.NextLevel);

#if Brain_Hero
            }
            else
            {
                var a = new RewardADNotify();
                a.onAdReward = () =>
                {
                    Hide();
                    LevelBasePage.Instance.Ad_tips_finish_finish();
                   // EventCenter.Broadcast(UtilsEventType.OnTipNumModify, 1);
                    EventCenter.Broadcast(UtilsEventType.NextLevel);
                };
                if (ADManager.ShowAD(GameAdID.Reward, a))
                {
                    LevelBasePage.Instance.Ad_tips_finish_show();
                }
            }
#endif
        });

        receiveBtn.onClick.AddListener(() =>
        {
            var a = new RewardADNotify();
            a.onAdReward = () =>
            {
                Hide();
                LevelBasePage.Instance.Ad_tips_finish_finish();
                EventCenter.Broadcast(UtilsEventType.OnTipNumModify, 1);
                EventCenter.Broadcast(UtilsEventType.NextLevel);
            };
            a.onAdSkip = () =>
            {
                Hide();
                LevelBasePage.Instance.Ad_tips_finish_skip();
                EventCenter.Broadcast(UtilsEventType.NextLevel);
            };

            if (ADManager.ShowAD(GameAdID.Reward, a))
            {
                LevelBasePage.Instance.Ad_tips_finish_show();
            }
        });
    }

    public void SetLevel(int value)
    {
        _level = value;
        AnalyticsUtil.Log("ad_all_topic");
        finishText.text = ConfigManager.Current().GetQuestionModel(value - 1).completeTip;
        var loc1 = receiveObject.transform.localPosition;
        var loc2 = nextBtn.transform.localPosition;
        receiveObject.transform.localPosition = loc2;
        nextBtn.gameObject.transform.localPosition = loc1;
#if Brain_Hero
        nextBtn.transform.localPosition = new Vector3(0,loc1.y,0);
#endif
        if (ADManager.IsCanShowAD(GameAdID.Reward))
        {
            nextBtnImage.transform.localScale = Vector3.zero;
            After(() => {
                nextBtnImage.transform.DOScale(1f, 0.5f);
            }, 1f);
        }
    }
    public void SetLevel(int value, bool showNativeAD)
    {
        showNative = showNativeAD;
        UtilsLog.LogWarning($"showNative:set {showNative}");
        SetLevel(value);
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        CheckAD();
        Utils.ADRewardSuceessRate();
        ADManager.onRewardADChange += CheckAD;
        particle.gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        ADManager.onRewardADChange -= CheckAD;
    }

    private void CheckAD()
    {
        if (ADManager.IsCanShowAD(GameAdID.Reward))
        {
            receiveBtn.gameObject.SetActive(true);
        }
        else
        {
            receiveBtn.gameObject.SetActive(false);
        }
    }

    public override void Hide()
    {
        particle.gameObject.SetActive(false);
        base.Hide();

    }

}
