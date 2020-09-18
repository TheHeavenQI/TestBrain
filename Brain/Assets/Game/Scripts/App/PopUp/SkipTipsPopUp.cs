using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class SkipTipsPopUp : BasePopUp {
    private Button _receive;
    public Button storeBtn;
    public override void Awake() {
        base.Awake();
        _receive = transform.Find("Content/Content/Receive/Receive").GetComponent<Button>();
        _receive.onClick.AddListener(ReceiveEvent);
        storeBtn.onClick.AddListener(() => {
            Hide();
        });
    }

    protected override void OnEnable() {
        base.OnEnable();
        CheckAD();
        Utils.ADRewardSuceessRate();
        ADManager.onRewardADChange += CheckAD;
    }

    private void OnDisable() {
        ADManager.onRewardADChange -= CheckAD;
    }

    private void CheckAD() {
        if (ADManager.IsCanShowAD(GameAdID.Reward)) {
            _receive.gameObject.SetActive(true);
        }
        else {
            _receive.gameObject.SetActive(false);
        }
    }
    
    private void CloseEvent() {
        Hide();
    }

    private void ReceiveEvent() {
        var a = new RewardADNotify();
        a.onAdReward = () => {
            Hide();
            LevelBasePage.Instance.Ad_tips_skip_finish();
            EventCenter.Broadcast(UtilsEventType.OnTipNumModify,1);
            AnalyticsUtil.Log($"{Global.GetAnalyticsPrefix()}ad_ad_skip_finish");
        };
        a.onAdSkip = () => {
            Hide();
            AnalyticsUtil.Log($"{Global.GetAnalyticsPrefix()}ad_ad_skip_skip");
            LevelBasePage.Instance.Ad_tips_skip_skip();
        };
        if (ADManager.ShowAD(GameAdID.Reward, a)) {
            LevelBasePage.Instance.Ad_tips_skip_show();
            AnalyticsUtil.Log($"{Global.GetAnalyticsPrefix()}ad_ad_skip_show");
        }
    }

    public override void ClickBack() {
        base.ClickBack();
        LevelBasePage.Instance.Ad_tips_skip_close();
    }
}
