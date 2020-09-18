using System;
using UnityEngine;

public class FBNativeADAndroid:FBNativeADInterface{
    private static AndroidJavaObject _instance;
    private static AndroidJavaObject GetInstance() {
        if (_instance == null) {
            try {
                UtilsLog.Log($"[FBNativeADAndroid]:GetInstance");
                //"com.android.bridgeandroid"; "com.android.fbnativead";
                var obj= new AndroidJavaClass("com.android.fbnativead.FBNativeAD");
                _instance = obj.CallStatic<AndroidJavaObject> ("getInstance");
            }
            catch (Exception e) {
                UtilsLog.LogError($"[FBNativeADAndroid]:{e}");
            }
        }
        return _instance;
    }

    public void initFBNativeADsWithPlacementID(string placementID) {
        UtilsLog.Log($"[FBNativeADAndroid]:initFBNativeADsWithPlacementID {placementID}");
        GetInstance()?.Call("initFBNativeADsWithPlacementID",placementID);
    }
    
    public void showFBNativeADs() {
        UtilsLog.Log($"[FBNativeADAndroid]:showFBNativeADs {GetInstance()}");
        GetInstance()?.Call("showFBNativeADs");
    }
    
    public void hideFBNativeADs() {
        UtilsLog.Log($"[FBNativeADAndroid]:hideFBNativeADs {GetInstance()}");
        GetInstance()?.Call("hideFBNativeADs");
    }
    
    private bool FBNativeADsIsAdLoaded() {
        UtilsLog.Log($"[FBNativeADAndroid]:FBNativeADsIsAdLoaded {GetInstance()}");
        var obj = GetInstance();
        if (obj == null) {
            return false;
        }
        return obj.Call<bool>("FBNativeADsIsAdLoaded");
    }
    public bool FBNativeADsIsAdValid() {
        UtilsLog.Log($"[FBNativeADAndroid]:FBNativeADsIsAdValid {GetInstance()}");
        if (AppSetting.debug) {
            return FBNativeADsIsAdLoaded();
        }
        var obj = GetInstance();
        if (obj == null) {
            return false;
        }
        return obj.Call<bool>("FBNativeADsIsAdValid");
    }
    
}
