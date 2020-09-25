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

    public const string UrlTerms = "https://testbrain.tumblr.com/post/629588622688075776/terms-and-conditions";
    public const string UrlPrivacy = "https://testbrain.tumblr.com/post/629588691278561280/privacy-policy";

    public const string APPLE_ID = "1532380255";

    /// ↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓ BUGLY id ↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓
    public const string BUGLY_APP_ID = "";
    public const string BUGLY_APP_ID_IOS = "";
    /// ↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑ BUGLY id ↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑

    /// ↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓ Mopub广告相关 id ↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓
    public static string RewardMopubID = "";
    public static string InterstitialMopubID = "5b07207b6b8c4d28bb9e7121ba3e39eb";
    public static string BannerMopubID = "";
    public static string FBNativeADS_iOS = "2431046500539162_2459667261010419";
    public static string FBNativeADS_android = "2431046500539162_2468593816784430";
    public static string OneSignalKey = "22688905-dcb3-4551-8e52-411bb67c2163";
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
