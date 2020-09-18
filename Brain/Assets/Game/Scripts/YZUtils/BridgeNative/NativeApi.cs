using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NativeApi
{

    //GetLocaleCountryCode
    public static float GetBrightness()
    {
#if UNITY_EDITOR
        return 1;
#endif
        if (AppSetting.isIOS)
        {
            return NativeiOS.GetBrightness();
        }
        else
        {
            return NativeAndroid.GetBrightness();
        }
    }
    public static void UploadReceiptData(string urlString, Dictionary<string,object> dict)
    {
        if (AppSetting.isIOS)
        {
           NativeiOS.UploadReceiptData(urlString,dict);
        }
    }
}
