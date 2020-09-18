using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public class UserModel {
    /// <summary>
    /// 当前玩到第几关,从1开始
    /// </summary>
    public int levelId;
    /// <summary>
    /// 钥匙数量
    /// </summary>
    public int keyCount;
    /// <summary>
    /// 最多玩到第几关,从1开始
    /// </summary>
    public int levelMaxId;
    public int christmasMaxId;
    public bool bgSound;
    public bool sound;
    public bool vibration;
    public static void Save(UserModel userModel) {
        if (userModel != null) {
            var str = JsonConvert.SerializeObject(userModel);
            UtilsLog.Log($"UserModel:{str}");
            PlayerPrefs.SetString("UserModel_KEY",str);
        }
    }
    
    private static UserModel _Instance;
    public static UserModel Get() {
        if (_Instance == null) {
            var a = PlayerPrefs.GetString("UserModel_KEY", "");
            if (a.Length > 0) {
                _Instance = JsonConvert.DeserializeObject<UserModel>(a);
            }
        }
        if (_Instance == null) {
            _Instance = new UserModel();
            _Instance.bgSound = true;
            _Instance.sound = true;
            _Instance.vibration = true;
        }
#if UNITY_EDITOR
        _Instance.levelMaxId = ConfigManager.Current().Questions.Count;
        _Instance.christmasMaxId = ConfigManager.Current().Activities.christ.Count;
#endif
        return _Instance;
    }

    public static void SaveLevel(int level) {
        var model = Get();
        model.levelId = level;
        if (Global.christmas) {
            if (model.levelId > model.christmasMaxId) {
                model.christmasMaxId = model.levelId;
            }
        }
        else {
            if (model.levelId > model.levelMaxId) {
                model.levelMaxId = model.levelId;
            }
        }
        Save(model);
    }
}
