using System.Collections.Generic;
using System.Runtime.InteropServices;
using Newtonsoft.Json;
public class NativeiOS 
{
#if UNITY_IOS && !UNITY_EDITOR
    [DllImport("__Internal")]
    private static extern string getLocaleCountryCode();

    [DllImport("__Internal")]
    private static extern bool getiOSDebug();

    [DllImport("__Internal")]
    private static extern float getBrightness();
    [DllImport("__Internal")]
    private static extern void setBrightness(float value);
    [DllImport("__Internal")]
    private static extern void uploadReceiptData(string urlString,string value);
#endif


    public static string GetLocaleCountryCode() {
#if UNITY_IOS && !UNITY_EDITOR
        return getLocaleCountryCode();
#endif
        return "US";
    }
    /// <summary>
    /// 获取iOS配置Debug
    /// </summary>
    /// <returns></returns>
    public static bool GetiOSDebug()
    {
#if UNITY_IOS && !UNITY_EDITOR
        return getiOSDebug();
#endif
        return false;
    }

    public static float GetBrightness()
    {
#if UNITY_IOS && !UNITY_EDITOR
        return getBrightness();
#endif
        return -1;
    }

    public static void SetBrightness(float value)
    {
#if UNITY_IOS && !UNITY_EDITOR
         setBrightness(value);
#endif
    }
    public static void UploadReceiptData(string urlString, Dictionary<string, object> dict)
    {
        string value = "";
        if (dict != null)
        {
            value = JsonConvert.SerializeObject(dict);
        }
#if UNITY_IOS && !UNITY_EDITOR
         uploadReceiptData(urlString,value);
#endif
    }
}
