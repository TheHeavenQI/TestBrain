using ADBridge;
using UnityEngine;
using Facebook.Unity;
using BaseFramework;
using System.Collections;
using Newtonsoft.Json.Linq;

public class MyAppStart : MonoBehaviour {
    public static bool FBInited;
    public static bool FirebaseInited;
    public static MyAppStart instance;
    private void Awake() {
        if (AppSetting.isIOS)
        {
            AppSetting.debug = NativeiOS.GetiOSDebug();
        }
        instance = this;
        Global.Init();
    }

    private void Start() {
        
        InitLog();
        InitAppsFlyer();
        InitFacebook();
        InitBugly();
        if (AppSetting.debug)
        {
            DebugCanvas.ShowDebug();
        }

        StartCoroutine(StartAfter());

        InitProducts();
    }

    private void InitProducts()
    {
    }
    
    private IEnumerator StartAfter() {
        while (!FBInited) {
        	yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(0.1f);
        RetainedAnalytics.Analytics();

    }
    private void CheckAllFinish() {
        
    }
    private void InitLog() {
        Debug.unityLogger.logEnabled = AppSetting.debug;
        Log.logLevel = AppSetting.debug ? Log.LogLevel.Verbose : Log.LogLevel.Assert;
    }

    private void InitFirebase() {
        
    }

    private void InitAppsFlyer() {
//        /* Mandatory - set your AppsFlyer’s Developer key. */
//        AppsFlyer.setAppsFlyerKey(AppSetting.APPSFLYER_DEV_KEY);
//        /* For detailed logging */
//        AppsFlyer.setIsDebug(AppSetting.debug);
//#if UNITY_IOS
//        /* Mandatory - set your apple app ID
//         NOTE: You should enter the number only and not the "ID" prefix */
//        AppsFlyer.setAppID(AppSetting.APPLE_ID);
//        AppsFlyer.trackAppLaunch();
//#elif UNITY_ANDROID
//        /* Mandatory - set your Android package name */
//        AppsFlyer.setAppID(Application.identifier);
//        /* For getting the conversion data in Android, you need to add the "AppsFlyerTrackerCallbacks" listener.*/
//        AppsFlyer.init(AppSetting.APPSFLYER_DEV_KEY, "AppsFlyerTrackerCallbacks");
//#endif

//        GameObject go = new GameObject("AppsFlyerTrackerCallbacks");
//        go.AddComponent<AppsFlyerTrackerCallbacks>();
//        DontDestroyOnLoad(go);
    }

    private void InitFacebook() {
        //if (FB.IsInitialized) {
        //    FB.ActivateApp();
        //    MyAppStart.FBInited = true;
        //    CheckAllFinish();
        //} else {
        //    //Handle FB.Init
        //    FB.Init(() => {
        //        FB.ActivateApp();
        //        MyAppStart.FBInited = true;
        //        CheckAllFinish();
        //    });
        //}
    }

    private void InitBugly() {
#if DEBUG
        return;
#endif
#if UNITY_IOS
        if (!string.IsNullOrEmpty(AppSetting.BUGLY_APP_ID_IOS)) {
            BuglyAgent.InitWithAppId(AppSetting.BUGLY_APP_ID_IOS);
            BuglyAgent.ConfigDebugMode(AppSetting.debug);
            BuglyAgent.EnableExceptionHandler();
        }
#elif UNITY_ANDROID
        if (!string.IsNullOrEmpty(AppSetting.BUGLY_APP_ID)) {
            BuglyAgent.InitWithAppId(AppSetting.BUGLY_APP_ID);
            BuglyAgent.ConfigDebugMode(AppSetting.debug);
            BuglyAgent.EnableExceptionHandler();
        }
#endif

    }
    
    public void InitAd() {
        ADUnitIDModel.ClearCurrentModel();
        AppSetting.RewardMopubID = ADUnitIDModel.GetCurrent().rewardvideo;
        AppSetting.InterstitialMopubID = ADUnitIDModel.GetCurrent().interstitial;
        AppSetting.BannerMopubID = ADUnitIDModel.GetCurrent().banner;
//        AppSetting.FBNativeADS = ADUnitIDModel.GetCurrent().native;
        MoPubManager.Instance.AdUnitId = AppSetting.RewardMopubID; 
        GameAdID.Reward = new AdUnit(AdType.Reward, AppSetting.RewardMopubID); 
        GameAdID.Interstitial = new AdUnit(AdType.Interstitial, AppSetting.InterstitialMopubID); 
        GameAdID.Banner = new AdUnit(AdType.Banner, AppSetting.BannerMopubID);
        ADManager.Init();
        ADManager.CloseAD(GameAdID.Banner);
    }

    private void OnApplicationPause(bool isPaused) {

        if (isPaused) {
            PlayerPrefs.Save();
        } else {
            InitFacebook();
        }
    }

    private void OnApplicationQuit() {
        PlayerPrefs.Save();
    }
}
