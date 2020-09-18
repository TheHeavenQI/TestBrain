using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public class UtilMoPubModel {
    public string zoneId;
    public string instanceType;
    public string sourceId;
    public string _m_className;
    public string adUnitId;
    public string appId;
    public string placementId;
    public string PlacementId;
    public string demandSourceName;
    public string AD_source() {
        var array = _m_className.Split('.');
        if (array.Length > 4) {
            return array[3];
        }
        return "default";
    }

    public string Network_id() {
        var source = AD_source();
        if (source == "unityads") {
            return zoneId;
        }
        if (source == "admob") {
            return adUnitId;
        }
        if (source == "facebook") {
            return placementId;
        }
        if (source == "vungle") {
            return PlacementId;
        }
        if (source == "applovin") {
            return zoneId;
        }
        if (source == "ironsource") {
            return demandSourceName;
        }
        return "default";
    }
    
    public override string ToString() {
        return $"Network_id:{Network_id()} AD_source:{AD_source()} _m_className:{_m_className}";
    }
}

public class UtilMoPub
{
   
    private static AndroidJavaObject _javaObject;
    private static AndroidJavaObject Instance() {
        if (_javaObject == null) {
            _javaObject = new AndroidJavaObject("com.android.unity_android.MopubUtil");
        }
        return _javaObject;
    }
    
    public static UtilMoPubModel GetRewardedVideoConfigs() {
#if !UNITY_ANDROID || UNITY_EDITOR
        return null;
#endif
//        var a = Instance().CallStatic<string>("getSourceRewardedVideoAdUnitId",AppSetting.RewardMopubID);
//        if (a != null) {
//            var model = JsonConvert.DeserializeObject<UtilMoPubModel>(a);
//            Debug.LogWarning($"[Configs]:GetRewardedVideoConfigs == {model.ToString()} {a}");
//            return model;
//        }
//        Debug.LogWarning($"[Configs]:GetRewardedVideoConfigs == null");
        return null;
    }
    public static UtilMoPubModel GetInterstitialConfigs() {
#if !UNITY_ANDROID || UNITY_EDITOR
        return null;
#endif
//        var a = Instance().CallStatic<string>("getSourceInterstitialAdUnitId",AppSetting.InterstitialMopubID);
//        if (a != null) {
//            var model = JsonConvert.DeserializeObject<UtilMoPubModel>(a);
//            Debug.LogWarning($"[Configs]:GetInterstitialConfigs == {model.ToString()} {a}");
//            return model;
//        }
//        Debug.LogWarning($"[Configs]:GetInterstitialConfigs == null");
        return null; ;
    }
    public static UtilMoPubModel GetBannerConfigs() {
#if !UNITY_ANDROID || UNITY_EDITOR
        return null;
#endif
//        var a = Instance().CallStatic<string>("getSourceBannerAdUnitId",AppSetting.BannerMopubID);
//        if (a != null) {
//            var model = JsonConvert.DeserializeObject<UtilMoPubModel>(a);
//            Debug.LogWarning($"[Configs]:GetBannerConfigs == {model.ToString()} {a}");
//            return model;
//        }
//        Debug.LogWarning($"[Configs]:GetBannerConfigs == null");
        return null;
  
    }
    
    
}
