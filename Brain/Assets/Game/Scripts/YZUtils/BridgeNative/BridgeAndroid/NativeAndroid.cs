using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NativeAndroid
{
    private static AndroidJavaObject _instance;
    private static AndroidJavaObject GetInstance()
    {
        if (_instance == null)
        {
            try
            {
                UtilsLog.Log($"[NativeAndroid]:GetInstance");
                var obj = new AndroidJavaClass("com.android.bridgeandroid.NativeAndroid");
                _instance = obj.CallStatic<AndroidJavaObject>("getInstance");
            }
            catch (Exception e)
            {
                UtilsLog.LogError($"[NativeAndroid]:{e}");
            }
        }
        return _instance;
    }

    public static void SetDebug(bool debug)
    {
        GetInstance()?.CallStatic("setDebug", debug);
    }
    public static float GetBrightness()
    {
        return GetInstance()?.CallStatic<float>("getBrightness") ?? 0;
    }
}
