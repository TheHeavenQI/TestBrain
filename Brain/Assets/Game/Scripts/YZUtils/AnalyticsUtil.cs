using Facebook.Unity;
using System.Collections.Generic;
using System.Diagnostics;
using BaseFramework;
using Debug = UnityEngine.Debug;
using System;

public static class AnalyticsUtil
{

    private static readonly string KEY = "AnalyticsUtil";

    public static void Log(string name)
    {
#if UNITY_EDITOR
        UtilsLog.Log($"[AnalyticsUtil]:name:{name}");
        return;
#endif
        try
        {
            FB.LogAppEvent(name, null, null);
            UtilsLog.Log($"[AnalyticsUtil]:name:{name}");
        }
        catch (Exception e)
        {
            UtilsLog.LogError($"[AnalyticsUtil]:error:{e}");
        }
    }

    public static void Log(string name, string key)
    {
#if UNITY_EDITOR
        UtilsLog.Log($"[AnalyticsUtil]:name:{name}");
        return;
#endif
        try
        {

            Dictionary<string, object> dicFB = new Dictionary<string, object>();
            dicFB.Add(key, -1);
            FB.LogAppEvent(name, null, dicFB);

            Dictionary<string, string> dicAF = new Dictionary<string, string>();
            dicAF.Add(key, "-1");


            UtilsLog.Log($"[AnalyticsUtil]:name:{name},key:{key}");
        }
        catch (Exception e)
        {
            UtilsLog.LogError($"[AnalyticsUtil]:error:{e}");
        }
    }


    public static void Log(string name, string key, int value)
    {
#if UNITY_EDITOR
        UtilsLog.Log($"[AnalyticsUtil]:name:{name},key:{key},value:{value}");
        return;
#endif
        try
        {

            Dictionary<string, object> dicFB = new Dictionary<string, object>();
            dicFB.Add(key, value);
            FB.LogAppEvent(name, null, dicFB);

            Dictionary<string, string> dicAF = new Dictionary<string, string>();
            dicAF.Add(key, value.ToString());


            UtilsLog.Log($"[AnalyticsUtil]:name:{name},key:{key},value:{value}");
        }
        catch (Exception e)
        {
            UtilsLog.LogError($"[AnalyticsUtil]:error:{e}");
        }
    }

    public static void Log(string name, string key, long value)
    {
#if UNITY_EDITOR
        UtilsLog.Log($"[AnalyticsUtil]:name:{name},key:{key},value:{value}");
        return;
#endif
        try
        {
            Dictionary<string, object> dicFB = new Dictionary<string, object>();
            dicFB.Add(key, value);
            FB.LogAppEvent(name, null, dicFB);

            Dictionary<string, string> dicAF = new Dictionary<string, string>();
            dicAF.Add(key, value.ToString());
            UtilsLog.Log($"[AnalyticsUtil]:name:{name},key:{key},value:{value}");
        }
        catch (Exception e)
        {
            UtilsLog.LogError($"[AnalyticsUtil]:error:{e}");
        }
    }

    public static void Log(string name, string key, double value)
    {
#if UNITY_EDITOR
        UtilsLog.Log($"[AnalyticsUtil]:name:{name},key:{key},value:{value}");
        return;
#endif
        try
        {
            Dictionary<string, object> dicFB = new Dictionary<string, object>();
            dicFB.Add(key, value);
            FB.LogAppEvent(name, null, dicFB);

            Dictionary<string, string> dicAF = new Dictionary<string, string>();
            dicAF.Add(key, value.ToString());
            UtilsLog.Log($"[AnalyticsUtil]:name:{name},key:{key},value:{value}");
        }
        catch (Exception e)
        {
            UtilsLog.LogError($"[AnalyticsUtil]:error:{e}");
        }
    }

    public static void Log(string name, string key, string value)
    {
#if UNITY_EDITOR
        UtilsLog.Log($"[AnalyticsUtil]:name:{name},key:{key},value:{value}");
        return;
#endif
        try
        {
            Dictionary<string, object> dicFB = new Dictionary<string, object>();
            dicFB.Add(key, value);
            FB.LogAppEvent(name, null, dicFB);

            Dictionary<string, string> dicAF = new Dictionary<string, string>();
            dicAF.Add(key, value.ToString());
            UtilsLog.Log($"[AnalyticsUtil]:name:{name},key:{key},value:{value}");
        }
        catch (Exception e)
        {
            UtilsLog.LogError($"[AnalyticsUtil]:error:{e}");
        }

    }

    public static void Log(string name, params KeyValuePair<string, object>[] pairs)
    {
#if UNITY_EDITOR
        UtilsLog.Log($"[AnalyticsUtil]:name:{name}");
        return;
#endif
        try
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();

            foreach (var kv in pairs)
            {
                dic.Add(kv.Key, kv.Value);
            }

            FB.LogAppEvent(name, null, dic);

            UtilsLog.Log($"[AnalyticsUtil]:name:{name},dic:{dic.ToCustomString()}");
        }
        catch (Exception e)
        {
            UtilsLog.LogError($"[AnalyticsUtil]:error:{e}");
        }
    }

    public static void Log(string name, Dictionary<string, object> dic)
    {
#if UNITY_EDITOR
        UtilsLog.Log($"[AnalyticsUtil]:name:{name},");
        return;
#endif
        try
        {
            Dictionary<string, string> dicAF = new Dictionary<string, string>();

            foreach (var kv in dic)
            {
                dicAF.Add(kv.Key, kv.Value.ToString());
            }

            FB.LogAppEvent(name, null, dic);
            UtilsLog.Log($"[AnalyticsUtil]:name:{name},dic:{dic.ToCustomString()}");
        }
        catch (Exception e)
        {
            UtilsLog.LogError($"[AnalyticsUtil]:error:{e}");
        }
    }

}
