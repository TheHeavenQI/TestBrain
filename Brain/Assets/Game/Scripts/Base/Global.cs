
using System.Globalization;
using UnityEngine;

public static class Global {
    
//    public static int TOTAL_LEVEL_COUNT = 193;
    public static bool christmas;
    public static int layerOrder;
    public static Sprite tipSprite;
    /// <summary>
    /// 今天玩了几关
    /// </summary>
    public static int playCount;
    public static bool isHideAD;
    public static bool isLimitPurchaseed;
    public static string currentRegion;
    public static string IDFA;
    public static void Init() {
        layerOrder = 1;
        currentRegion = RegionInfo.CurrentRegion.Name;
#if UNITY_IOS
        currentRegion = NativeiOS.GetLocaleCountryCode();
#endif
        isHideAD = PlayerPrefs.GetInt(Constance.storage_isHideAD, 0) == 1;
        isLimitPurchaseed = PlayerPrefs.GetInt(Constance.storage_isLimitPurchaseed, 0) == 1;
        
    }

    public static string GetAnalyticsPrefix() {
        if (christmas) {
            return "christmas_";
        }
        return "";
    }
    public static void SetLimitetPurchaseed(bool purchased)
    {
        isLimitPurchaseed = purchased;
        PlayerPrefs.SetInt(Constance.storage_isLimitPurchaseed, purchased?1:0);
        if (purchased)
        {
            HideAd();
        }
    }
    public static void HideAd() {
        isHideAD = true;
        ADManager.CloseAD(GameAdID.Banner);
        ADManager.CloseAD(GameAdID.Interstitial);
        PlayerPrefs.SetInt(Constance.storage_isHideAD, 1);
        PlayerPrefs.Save();
    }
    
}
