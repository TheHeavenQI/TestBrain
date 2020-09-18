using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuidenceManager {

    private static GuidenceManager _instance;

    public static GuidenceManager Instance() {
        if (_instance == null) {
            _instance = new GuidenceManager();
        }
        return _instance;
    }
    /// <summary>
    /// 引导次数
    /// </summary>
    public int guideCount {
        get {
            return PlayerPrefs.GetInt("SLGuideCount", 0);
        }

        set {
            setGuidenCount(value);
        }

    }
    /// <summary>
    /// 是否需要展示
    /// </summary>
    public bool canShowGuide {
        get => PlayerPrefs.GetInt("SLGuideCount", 0) < 2 ? true : false;

    }


    private void setGuidenCount(int value) {
        PlayerPrefs.SetInt("SLGuideCount", value);
    }

}
