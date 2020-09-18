using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Facebook.Unity;
using UnityEngine;


//    ↑ ↓
public class AppSetting {
    private static bool _init = false;
    private static bool _initABTest = false;
#if DEBUG
    public static bool debug = true;
#else
    public static bool debug = false;
#endif

#if UNITY_EDITOR
    public const bool isEditor = true;
#else
    public const bool isEditor = false;
#endif

#if UNITY_IOS && !UNITY_EDITOR
    public const bool isIOS = true; 
#else
    public const bool isIOS = false;
#endif

#if UNITY_ANDROID && !UNITY_EDITOR
    public const bool isANDROID = true; 
#else
    public const bool isANDROID = false;
#endif

    public const string UrlTerms = "https://brain1231.tumblr.com/post/190187975563/agreement-to-terms";
    public const string UrlPrivacy = "https://brain1231.tumblr.com/post/190187992113/privacy-policy";

    public const string APPLE_ID = "";

    /// ↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓ BUGLY id ↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓
    public const string BUGLY_APP_ID = "";
    public const string BUGLY_APP_ID_IOS = "";
    /// ↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑ BUGLY id ↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑

    /// ↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓ Mopub广告相关 id ↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓
    public static string RewardMopubID = "";
    public static string InterstitialMopubID = "";
    public static string BannerMopubID = "";
    public static string FBNativeADS_iOS = "2431046500539162_2459667261010419";
    public static string FBNativeADS_android = "2431046500539162_2468593816784430";
    public static string OneSignalKey = "";
    public static string OneSignalKey_TEST = "";
    /// 安卓需要在 Plugins/Android/IronSource/AndroidManifest  中修改 GADApplicationIdentifier
    /// ca-app-pub-2380106415199403~9830432370
    public const string GADApplicationIdentifier_IOS = "";
    /// ↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑ IRONSOURCE 广告 id ↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑
   

    private static int GetABTestID(string ABName) {
        string key = $"ABTEST:{ABName}";
        var num = PlayerPrefs.GetInt(key, 0);
        if (num == 0) {
            var a = UnityEngine.Random.Range(0, 1000000);
            PlayerPrefs.SetInt(key, a);
            num = a;
        }
        return num;
    }

}
