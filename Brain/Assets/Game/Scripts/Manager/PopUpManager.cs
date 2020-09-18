using System;
using System.Collections.Generic;
using UnityEngine;

public class PopUpManager : MonoBehaviour {
    public static PopUpManager Instance;
    public GameObject HudWaiting;
    private void Awake()
    {
        Instance = this;
    }


    private Dictionary<string,GameObject> _popupdict = new Dictionary<string, GameObject>();
    private GameObject GetPopUp(string name) {
        if (_popupdict.ContainsKey(name)) {
            return _popupdict[name];
        }
        var prefab = Resources.Load<GameObject>($"PopUp/{name}");
        var obj = Instantiate(prefab);
        _popupdict[name] = obj;
        return obj;
    }
    
    private GameObject GetObj(string name){
        var obj = GetPopUp(name);
        obj.transform.SetParent(transform,false);
        obj.SetActive(true);
        return obj;
    }
    
    
    /// <summary>
    /// 显示完成页面
    /// </summary>
    /// <param name="level"></param>
    public void ShowFinish(int level) {
        ShowFinish(level,true);
    }
    
    /// <summary>
    /// 显示完成页面
    /// </summary>
    /// <param name="level"></param>
    public void ShowFinish(int level,bool showAD) {
        var obj = GetObj("FinishPopUp");
        FinishPopUp a = obj.GetComponent<FinishPopUp>();
        if (a) {
            a.SetLevel(level,showAD);
        }
    }
    
    public void ShowAllFinish() {
        GetObj("FinishAllLevelPopUp");
    }
    
    /// <summary>
    /// 显示跳过提示
    /// </summary>
    public void ShowSkip() {
        GetObj("SkipTipsPopUp");
    }
    
    /// <summary>
    /// 显示获取提示
    /// </summary>
    public void ShowGetTip() {
        GetObj("GetTipsPopUp");
    }
    /// <summary>
    /// 显示提示
    /// </summary>
    public void ShowTip(int level) {
        var obj = GetObj("TipsPopUp");
        var a = obj.GetComponent<TipsPopUp>();
        if (a) {
           a.SetLevel(level);
        }
    }
    /// <summary>
    /// 显示评分
    /// </summary>
    public void ShowScore(bool feedback , Action closeAction) {
        var obj = GetObj("ScorePopUp");
        var a = obj.GetComponent<ScorePopUp>();
        if (a) {
            a.CloseAction = closeAction;
            if (feedback) {
                a.ShowFeedback();
            }
            else {
                a.ShowScore();
            }
        }
    }

    public void ShowChrist() {
        GetObj("ChristPopUp");
    }
    
    /// <summary>
    /// 显示获取提示
    /// </summary>
    public void ShowLimitedTime(int limitedTime) {
        var obj = GetObj("LimitedTimePopUp");
        LimitedTimePopUp a = obj.GetComponent<LimitedTimePopUp>();
        if (a) {
            a.SetLimitedTime(limitedTime);
        }
    }
}
