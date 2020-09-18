using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class GetTipsPopUp : BasePopUp
{
    private Button _receive;
    public Button storeBtn;
    protected override float marginY => FBNativeAD.FBNativeADsIsAdValid() ? 160 : 0;
    private void Awake() {
        base.Awake();
        _receive = transform.Find("Content/Receive/Receive").GetComponent<Button>();
        _receive.onClick.AddListener(ReceiveEvent);
        storeBtn.onClick.AddListener(() => {
            Hide();
        });
    }

    private void OnEnable() {
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
    
    private void ReceiveEvent() {
        var a = new RewardADNotify();
       a.onAdReward = () => {
           Hide();
           LevelBasePage.Instance.Ad_tips_finish();
           EventCenter.Broadcast(UtilsEventType.OnTipNumModify,1);
           
           AnalyticsUtil.Log($"{Global.GetAnalyticsPrefix()}ad_ad_tips_finish");
       };
       a.onAdSkip = () => {
           Hide();
           AnalyticsUtil.Log($"{Global.GetAnalyticsPrefix()}ad_ad_tips_skip");
           LevelBasePage.Instance.Ad_tips_skip();
       };
       
       if (ADManager.ShowAD(GameAdID.Reward, a)) {
           LevelBasePage.Instance.Ad_tips_show();
           AnalyticsUtil.Log($"{Global.GetAnalyticsPrefix()}ad_ad_tips_show");
       }
    }
    public override void Hide()
    {
        base.Hide();
        EventCenter.Broadcast(UtilsEventType.OnTipsDialogClose);
    }
    public override void ClickBack() {
        base.ClickBack();
        LevelBasePage.Instance.Ad_tips_close();
    }
    
}
