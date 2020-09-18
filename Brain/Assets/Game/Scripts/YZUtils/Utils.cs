using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class Utils
{
    public static int currentTimeStamp => (int)(DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds;
    private static readonly StringBuilder builder = new StringBuilder();
    public static string PriceToString(double price) {
        builder.Clear();
        Int64 beilv = (Int64)10E+6;
        Int64 num = Convert.ToInt64(price * beilv);
        long zhengshu = num / 10000000;
        builder.Append(zhengshu);
        builder.Append(".");
        builder.Append((num/1000000) % 10); 
        builder.Append((num/100000) % 10); 
        builder.Append((num/10000) % 10);
        builder.Append((num/1000) % 10); 
        builder.Append((num/100) % 10);
        builder.Append((num/10) % 10);
        string numstring = builder.ToString();
        if (numstring.Contains(",")) {
            Dictionary<string,object> dict = new Dictionary<string, object>();
            dict["price"] = price;
            dict["errorMsg"] = "price Contains ,";
            AnalyticsUtil.Log("price_error",dict);
            return "0";
        }
        UtilsLog.Log($"PriceToString:{price} {numstring}");
        return numstring;
    }
    
    
    private static Dictionary<string,GameObject> _objectdict = new Dictionary<string, GameObject>();
    /// <summary>
    /// 震动
    /// </summary>
    public static void Vibrate() {
        if (UserModel.Get().vibration) {
            Handheld.Vibrate();
        }
    }

    public static GameObject GetObjectSingle(string name) {
        if (_objectdict.ContainsKey(name)) {
            var gameObject = _objectdict[name];
            if (gameObject) {
                return gameObject;
            }
        }
        var prefab = Resources.Load<GameObject>(name);
        var obj = GameObject.Instantiate(prefab);
        _objectdict[name] = obj;
        return obj;
    }

    public static void calTime(Action action,string name) {
         float st = Time.realtimeSinceStartup;
         action?.Invoke();
         float end = Time.realtimeSinceStartup;
         Debug.LogWarning($"spendTime:{name}:{end-st}");
    }
    
    private static int _firstOpenApp = 0;
    public static bool FirstOpenApp() {
        if (_firstOpenApp == 1) {
            return true;
        }
        _firstOpenApp = PlayerPrefs.GetInt("FirstOpenApp", 0);
        if (_firstOpenApp == 0) {
            _firstOpenApp = 1;
            PlayerPrefs.SetInt("FirstOpenApp",2);
            PlayerPrefs.Save();
            return true;
        }
        return false;
    }

    public static void ADInterstitialSuceessRate() {
        if (ADManager.IsCanShowAD(GameAdID.Reward)) {
            AnalyticsUtil.Log("ad_interstitial_loaded_success");
        }
        else {
            AnalyticsUtil.Log("ad_interstitial_loaded_fail");
        }
    }
    public static void ADRewardSuceessRate() {
        if (ADManager.IsCanShowAD(GameAdID.Reward)) {
            AnalyticsUtil.Log("ad_reward_loaded_success");
        }
        else {
            AnalyticsUtil.Log("ad_reward_loaded_fail");
        }
    }
    public static void GiveTips()
    {
        UtilsLog.Log($"[GiveTips]:{Global.isLimitPurchaseed}");
        if (!Global.isLimitPurchaseed)
        {
            return;
        }
        string day = PlayerPrefs.GetString(Constance.storage_send_tip, "");
        string nowDay = DateTime.Now.ToShortDateString();
        UtilsLog.Log($"[GiveTips]:{nowDay} {day}");
        if (day != nowDay)
        {
            PlayerPrefs.SetString(Constance.storage_send_tip, nowDay);
            EventCenter.Broadcast(UtilsEventType.OnTipNumModify, 20);
        }
    }
}
