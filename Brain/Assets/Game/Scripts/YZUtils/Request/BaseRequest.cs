using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using BaseFramework;
using UnityEngine;
using UnityEngine.Networking;
using Random = UnityEngine.Random;

public class BaseRequest : MonoBehaviour {
    public static BaseRequest Instance;
    public static readonly string urlLogin = "/api/login";
    public static readonly string urlAdWorth = "/api/ad_worth";
    public static readonly string urlPurchaseWorth = "/api/purchase_worth";
#if UNITY_IOS
    public static string Domain = "http://brainios.shapekeeper.net";
    public static readonly string urlUnitid = "https://iwmonetize.s3.amazonaws.com/brainsharpios.json";
#else
    public static string Domain = "http://brainandroid.shapekeeper.net";
    public static readonly string urlUnitid = "https://iwmonetize.s3.amazonaws.com/brainsharpandroid.json";
#endif
    public static readonly string urlAb_test_tag = "/api/ab_test_tag";
    public static readonly string urlApple_subscribe = "/api/apple_subscribe";
    public static readonly string urlApple_subscribe_renew = "/api/apple_subscribe_renew";

    private void Awake() {
        Instance = this;
        DontDestroyOnLoad(this);
    }
    public static string GetAbsolutelyUrl(string url)
    {
        if (url.StartsWith("/", StringComparison.Ordinal))
        {
            url = Domain + url;
        }
        return url;
    }
    public void Get(string url,Action<bool,string> callback) {
        if (string.IsNullOrEmpty(url)) {
            callback(false, "url.IsNullOrEmpty");
            return;
        }
        StartCoroutine(IEGet(url, callback));
    }
    public void Post(string url,Dictionary<string,object> forms, Action<bool,string> callback) {
        if (string.IsNullOrEmpty(url)) {
            callback(false, "url.IsNullOrEmpty");
            return;
        }
        StartCoroutine(IEPost(url, forms, callback));
    }
    
    private IEnumerator IEGet(string url,Action<bool,string> callback) {
       
        url = BaseRequest.GetAbsolutelyUrl(url);
        WWWForm form = new WWWForm();
        int random = Random.Range(1, 100000);
        UtilsLog.Log($"[BaseRequest]:{random}:{url}");
        UnityWebRequest request = UnityWebRequest.Get(url);
        request.timeout = 15;
        yield return request.SendWebRequest();
        UtilsLog.Log($"[BaseRequest]:{random}:finished:{url} responseCode:{request.responseCode} error:{request.error} text:{request.downloadHandler.text}");
        if (string.IsNullOrEmpty(request.error)) {
            callback?.Invoke(true, request.downloadHandler.text);
        } else {
            callback?.Invoke(false, request.error);
        }
    }
    
    private IEnumerator IEPost(string url,Dictionary<string,object> formsParams, Action<bool,string> callback) {
        url = BaseRequest.GetAbsolutelyUrl(url);
        WWWForm form = new WWWForm();
        if (formsParams != null) {
            if (!formsParams.ContainsKey("af_id")) {
                formsParams.Add("af_id",AppsFlyer.getAppsFlyerId() ?? "null");
            }
            if (!formsParams.ContainsKey("bundle_id")) {
                formsParams.Add("bundle_id",Application.identifier);
            }
            if (!formsParams.ContainsKey("idfa") && Global.IDFA != null)
            {
                formsParams.Add("idfa", Global.IDFA);
            }
            if (!formsParams.ContainsKey("timestamp")) {
                int timestamp = (int)(DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds;
                formsParams.Add("timestamp",$"{timestamp}");
            }
            foreach (var param in formsParams) {
                if(param.Value != null)
                {
                    form.AddField(param.Key, param.Value.ToString());
                }
            }
        }
        int random = Random.Range(1, 100000);
        UtilsLog.Log($"[BaseRequest]:{random}:{url} {formsParams.ToCustomString()}");
        UnityWebRequest request = UnityWebRequest.Post(url, form);
        request.timeout = 15;
        yield return request.SendWebRequest();
        UtilsLog.Log($"[BaseRequest]:{random}:finished:{url} responseCode:{request.responseCode} error:{request.error} text:{request.downloadHandler.text}");
        
        if (string.IsNullOrEmpty(request.error)) {
            callback?.Invoke(true, request.downloadHandler.text);
        } else {
            callback?.Invoke(false, request.error);
        }
    }


}
