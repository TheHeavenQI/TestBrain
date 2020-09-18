using System.Collections;
using System.Collections.Generic;
using BaseFramework;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.UI;


public class ADUnitIDModel {
    public string rewardvideo;
    public string interstitial;
    public string banner;
    public string native;
    private static ADUnitIDModel _current;
    public static ADUnitIDModel GetCurrent() {
        if (_current == null) {
            string storage_adUnitid = PlayerPrefs.GetString(Constance.storage_adUnitid, "");
            string from = "localNetwork";
            if (storage_adUnitid == "") {
                from = "local";
                if (AppSetting.isIOS) {
                    storage_adUnitid = Resources.Load<TextAsset>("brainsharpADUnitIDiOS").text;
                }
                else {
                    storage_adUnitid = Resources.Load<TextAsset>("brainsharpADUnitIDANDROID").text;
                }
            }
            string region = Global.currentRegion.ToLower();
            var dictionary = JsonConvert.DeserializeObject<Dictionary<string, ADUnitIDModel>>(storage_adUnitid);
            if (!dictionary.ContainsKey(region)) {
                region = "us";
            }
            _current = dictionary[region];
            UtilsLog.Log($"currentRegion:from:{from} currentRegion:{Global.currentRegion.ToLower()} region:{region} {_current.ToString()}");
        }
        return _current;
    }

    public static void ClearCurrentModel() {
        _current = null;
    }
    public override string ToString() {
        return $"\n rewardvideo:{rewardvideo}\n interstitial:{interstitial}\n banner:{banner}\n native:{native}";
    }
}
