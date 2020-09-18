
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using BaseFramework;
using System.Collections.Generic;

public class LimitedTimePopUp : BasePopUp
{
    public Button receive;
    public Button getNowButton;
    protected override bool showNativeAD => false;
    public Text timeLabel;
    private int _leftTime;
    public Button privacyPolicy;
    public Button terms;
    public ScrollRect scrollRect;
    public Button restoreBtn;


    public override void Start() {
        base.Start();
        receive.onClick.AddListener(ReceiveEvent);

       

        getNowButton.onClick.AddListener(() => {
            AnalyticsUtil.Log("limited_Purchase_Click");            
        });

        privacyPolicy.onClick.AddListener(() =>
        {
            Application.OpenURL(AppSetting.UrlPrivacy);
        });
        terms.onClick.AddListener(() =>
        {
            Application.OpenURL(AppSetting.UrlTerms);
        });

        restoreBtn.onClick.AddListener(() => {
            if (AppSetting.isANDROID)
            {
            }
            else
            {
                NativeiOSPurchase.Restore((success, ids, error) =>
                {
                    if (success)
                    {
                        RestoreCallBack(ids);
                    }
                });
            }
        });
        
    }

    private void RestoreCallBack(string[] ids)
    {
        Hide();
    }
    public void SetLimitedTime(int time) {
        _leftTime = time;
        StartCoroutine("TimeAA");
    }

    private IEnumerator TimeAA() {
        while (_leftTime > 0) {
            _leftTime -= 1;
            string ti = string.Format("00:{0:00}:{1:00}", _leftTime / 60, _leftTime % 60);
            timeLabel.text = ti;
            yield return new WaitForSeconds(1);
        }
        Hide();
    }
    protected override void OnEnable() {
        base.OnEnable();
        AnalyticsUtil.Log("limited_Purchase_Open");
        ADManager.CloseAD(GameAdID.Banner);
        CheckAD();
        Utils.ADRewardSuceessRate();
        ADManager.onRewardADChange += CheckAD;

        TaskHelper.Create<CoroutineTask>()
            .Delay(0.15f)
            .Do(() => {
                scrollRect.content.anchoredPosition = new Vector3(0, 0, 0);
            })
            .Delay(0.15f)
            .Do(() => {
                scrollRect.content.anchoredPosition = new Vector3(0, 0, 0);
            })
            .Execute();
    }
    
    private void OnDisable() {
        ADManager.onRewardADChange -= CheckAD;
    }

    private void CheckAD() {
        if (ADManager.IsCanShowAD(GameAdID.Reward)) {
            receive.gameObject.SetActive(true);
        }
        else {
            receive.gameObject.SetActive(false);
        }
    }
    
    private void ReceiveEvent() {
        var a = new RewardADNotify();
        a.onAdReward = () => {
            Hide();
            EventCenter.Broadcast(UtilsEventType.OnTipNumModify,2);
            AnalyticsUtil.Log($"ad_ad_Limited_finish");
        };
        a.onAdSkip = () => {
            Hide();
            AnalyticsUtil.Log($"ad_ad_Limited_skip");
        };
       
        if (ADManager.ShowAD(GameAdID.Reward, a)) {
            AnalyticsUtil.Log($"ad_ad_Limited_show");
        }
    }
    
}
