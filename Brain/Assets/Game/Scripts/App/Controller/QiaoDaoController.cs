using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Debug = System.Diagnostics.Debug;

public class QiaoDaoManager {
    /// <summary>
    /// 今天是否签到
    /// </summary>
    /// <returns></returns>
    public static bool TodayQiaoDao() {
        var LastQiaoDaoTime = PlayerPrefs.GetString("LastQiaoDaoTime", "");
        var today = DateTime.Today.ToShortDateString();
        return today == LastQiaoDaoTime;
    }
    /// <summary>
    /// 签到
    /// </summary>
    public static void QiaoDao() {
        var today = DateTime.Today.ToShortDateString();
        PlayerPrefs.SetString("LastQiaoDaoTime", today);
        var QiaoDaoCount = PlayerPrefs.GetInt("QiaoDaoCount", 0);
        PlayerPrefs.SetInt("QiaoDaoCount",QiaoDaoCount+1);
    }
    /// <summary>
    /// 签到天数
    /// </summary>
    /// <returns></returns>
    public static int QiaoDaoCount() {
        var QiaoDaoCount = PlayerPrefs.GetInt("QiaoDaoCount", 0);
        return QiaoDaoCount;
    }
}
public class QiaoDaoController : BaseController {
    
    public List<QiaoDaoItem2> items;
    public Button receiveButton;
    public Button receiveDoubleButton;
    public GameObject receiveDoubleGameObject;
    public Text receiveDoubleText;
    private int tipNum = 2;
    public override void Start() {
        base.Start();
        receiveButton.onClick.AddListener(() => {
            EventCenter.Broadcast(UtilsEventType.OnTipNumModify,tipNum);
            QiaoDao();
            Hide();
        });
        receiveDoubleButton.onClick.AddListener(() => {
            var a = new RewardADNotify();
            a.onAdReward = () => {
                Hide();
                QiaoDao();
                EventCenter.Broadcast(UtilsEventType.OnTipNumModify,tipNum*2);
            };
            a.onAdSkip = () => {
                Hide();
            };
            if (ADManager.ShowAD(GameAdID.Reward, a)) {
                AnalyticsUtil.Log("DoubleQiaoDao");
            }
        });
    }

    private void QiaoDao() {
        QiaoDaoManager.QiaoDao();
        receiveButton.gameObject.SetActive(false);
        receiveDoubleGameObject.gameObject.SetActive(false);
    }

    private void OnEnable() {
        base.OnEnable();
        CheckAD();
        Utils.ADRewardSuceessRate();
        ADManager.onRewardADChange += CheckAD;
    }

    private void OnDisable() {
        ADManager.onRewardADChange -= CheckAD;
    }

    private void CheckAD() {
        if (ADManager.IsCanShowAD(GameAdID.Reward)) {
            receiveDoubleButton.gameObject.SetActive(true);
        }
        else {
            receiveDoubleButton.gameObject.SetActive(false);
        }
    }

    
    public void Init() {
        receiveButton.gameObject.SetActive(true);
        receiveDoubleGameObject.gameObject.SetActive(true);
        if (Utils.FirstOpenApp()) {
            AnalyticsUtil.Log("qiaodao_first_show");
        }
        // 1 - 5
        var count = QiaoDaoManager.QiaoDaoCount() % 5 + 1;
        // 今天是否签到
        var todayQianDao = QiaoDaoManager.TodayQiaoDao();
        int todayCount = count;
        if (todayQianDao) {
            todayCount -= 1;
        }
        if (todayCount == 0) {
            todayCount = 5;
        }
        UtilsLog.Log($"todayQianDao:{todayQianDao} {todayCount}");

        if (todayQianDao) {
            receiveButton.gameObject.SetActive(false);
            receiveDoubleGameObject.gameObject.SetActive(false);
        }
        if (todayCount <= 3) {
            tipNum = 1;
        }
        else {
            tipNum = 2;
        }
        for (int i = 0; i < items.Count; i++) {
            var item = items[i];
            item.SetDay(i+1,i < 3 ? 1 : 2);
            if (i < todayCount - 1) {
                item.SetType(0);
            }else if (i > todayCount - 1) {
                item.SetType(2);
            }else if (i == todayCount - 1) {
                if (!todayQianDao) {
                    item.SetType(1);
                }
                else {
                    item.SetType(0);
                }
            }
        }
    }
    public override void ClickBack()
    {
        base.ClickBack();
        var count = QiaoDaoManager.QiaoDaoCount() % 5 + 1;
        AnalyticsUtil.Log($"qiaodao_{count}_close");
        if (Utils.FirstOpenApp()) {
            AnalyticsUtil.Log("qiaodao_first_close");
        }
    }
}
