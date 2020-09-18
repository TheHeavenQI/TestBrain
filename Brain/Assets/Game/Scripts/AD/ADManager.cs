
using System;
using System.Collections.Generic;
using ADBridge;
using UnityEngine;

public class ADManager {

    public static event Action onRewardADChange;
    public static bool isMoPub = true;
    public static void Init() {
        #if UNITY_EDITOR
                return;
        #endif
        FBNativeAD.initFBNative();
        AdUnit[] adUnits = new AdUnit[] { GameAdID.Reward, GameAdID.Interstitial, GameAdID.Banner };
        AdBridge.InitMediationType(MediationType.MoPub);
        AdBridge.Init(adUnits, AppSetting.debug, () => {
            AdBridge.Request(GameAdID.Reward);
            if (!Global.isHideAD) {
                AdBridge.Request(GameAdID.Interstitial);
                AdBridge.Request(GameAdID.Banner);
            }
            AdBridge.CloseAd(GameAdID.Banner);
        });
        AdBridge.SetAlwayNotify(AdType.Reward, GetAlwayRewardADNotify());
        AdBridge.SetAlwayNotify(AdType.Interstitial, GetAlwayinterstitialNotify());
        AdBridge.SetAlwayNotify(AdType.Banner, GetAlwayBannerNotify());
    }
    public static bool IsCanShowAD(AdUnit adUnit) {
#if UNITY_EDITOR
        return true;
#endif
        return AdBridge.IsAdReady(adUnit);
    }
    public static void SetNotify(AdUnit adUnit, IAdNotify notify) {
        AdBridge.SetNotify(adUnit.adType, notify);
    }
    public static void RemoveNotify(AdUnit adUnit) {
        AdBridge.SetNotify(adUnit.adType, null);
    }
    public static void CloseAD(AdUnit adUnit) {
        AdBridge.CloseAd(adUnit);
    }


    public static bool ShowAD(AdUnit adUnit, IAdNotify notify = null) {
        if (Global.isHideAD && adUnit.adType != AdType.Reward) {
            return false;
        }
        if (!IsCanShowAD(adUnit)) {
            return false;
        }
#if UNITY_EDITOR 
        if (adUnit.adType == AdType.Reward && notify != null) {
            IRewardADNotify rewardNotify = notify as IRewardADNotify;
            rewardNotify.OnAdShow();
            rewardNotify.OnAdReward();
            rewardNotify.OnAdClose();
            return true;
        }
#endif
        if (adUnit.adType == AdType.Reward) {
            FBNativeAD.hideFBNativeADs();
        }
        AdBridge.ShowAd(adUnit, notify);
        return true;
    }
    /// ↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓ AlwayNotify ↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓
    private static ADNotify GetAlwayBannerNotify() {
        ADNotify bannerNotify = new ADNotify();
        bannerNotify.onAdLoad = () => {
            
        };
        bannerNotify.onAdShow = () => {

        };
        return bannerNotify;
    }
    private static ADNotify GetAlwayinterstitialNotify() {

        ADNotify interstitialNotify = new ADNotify();
        interstitialNotify.onAdLoad = () => {
            
        };
        interstitialNotify.onAdShow = () => {

        };
        return interstitialNotify;
    }

    private static RewardADNotify GetAlwayRewardADNotify() {
        RewardADNotify notify = new RewardADNotify();
        notify.onAdLoad = () => {
           
            onRewardADChange?.Invoke();
        };
        notify.onAdClose = () => {
            onRewardADChange?.Invoke();
        };
        notify.onAdSkip = () => {
            AnalyticsUtil.Log($"{Global.GetAnalyticsPrefix()}ad_ad_skip");
        };

        notify.onAdReward = () => {
//            UtilsRequest.Instance.WatchADReq(3);
            AnalyticsUtil.Log($"{Global.GetAnalyticsPrefix()}ad_ad_finish");
        };
        notify.onAdShow = () => {
            AnalyticsUtil.Log($"{Global.GetAnalyticsPrefix()}ad_ad_show");
        };
        return notify;
    }
    /// ↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑ AlwayNotify ↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑
}


