using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using System.Text;

public class RateUtil
{
    public static void RateGame()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        Application.OpenURL("market://details?id=" + Application.identifier);
#elif UNITY_IOS && !UNITY_EDITOR
        var commentCount = PlayerPrefs.GetInt("IOS_COMMENT", 0);
        PlayerPrefs.SetInt("IOS_COMMENT",commentCount+1);
//        if (commentCount < 3) {
//            UnityStoreKitMgr.Instance.GoToCommnet(AppSetting.APPLE_ID);
//        }
//        else {
//            Application.OpenURL($"itms-apps://itunes.apple.com/app/id{AppSetting.APPLE_ID}?mt=8&action=write-review");
//        }
        Application.OpenURL($"itms-apps://itunes.apple.com/app/id{AppSetting.APPLE_ID}?mt=8&action=write-review");
#endif
    }
    
    public struct ClientInfo {
        public string bundleID;
        public string version;

        public static ClientInfo Default {
            get {
                return new ClientInfo() {
                    bundleID = Application.identifier,
                    version = Application.version,
                };
            }
        }
    }

    public static void Feedback(int star, string content)
    {
        string url = "http://fantasypark.shapekeeper.net/api/review";
        WWWForm form = new WWWForm();
        form.AddField("rate", star);
        form.AddField("content", content);
        form.AddField("clientInfo", JsonConvert.SerializeObject(ClientInfo.Default));
        UnityWebRequest request = UnityWebRequest.Post(url, form);
        request.SendWebRequest();

    }
    
    
}
