using System;
using System.Collections.Generic;
using Assets.SimpleAndroidNotifications;
using Newtonsoft.Json;
using UnityEngine;


public class Notifications : MonoBehaviour {
    private List<string> _msgList = new List<string>();
    private List<float> _timeList = new List<float>();
    private bool _finish = false;

    public static Notifications Instance;
    private void Awake() {
        Instance = this;
    }
    private static void HandleNotificationOpened(OSNotificationOpenedResult result) {
        UtilsLog.Log($"[Notification]:{JsonConvert.SerializeObject(result.notification.payload)}");
    }
    // Start is called before the first frame update
    void Start() {
        _msgList.Add("How many words in Oxford Dictionary?");
        _msgList.Add("What letters in alphabet people like to hear the most?");
        _msgList.Add("Fold a 0.01 cm thick paper 4 times, how thick it will be?");
        _msgList.Add("Boil 1 egg need 4 minute, how long does it take to boil 3?");
        _msgList.Add("We have some new ridiculous levels today!");
        _msgList.Add("Free up your imagination at BRAIN SHARP today!");
        _msgList.Add("Already mastered BRAIN SHARP, here’s new levels for you!");
        _msgList.Add("What’s your best score? Ready to update it now?");
        _msgList.Add("Challenge your brain at BRAIN SHARP!");
        _msgList.Add("Sharp your brain here as well as your imagination!");
        
        _msgList.Add("How many letters left in alphabet if ET left?");
        _msgList.Add("What did the first person do to the cow found milk was drinkable?");
        _msgList.Add("Who would you save? Mom or girlfriend?");
        _msgList.Add("Tom's hen lays egg on Jack's yard, who owns the egg?");
    }

    private DateTime FirstNotificationsLocalTime() {
        string timeStr = PlayerPrefs.GetString("Notifications_FIRST_TIME", "");
        if (timeStr.Length <= 0) {
            DateTime nowTime = DateTime.Now;//本地时间
            timeStr = $"{nowTime.Year}-{nowTime.Month}-{nowTime.Day} 13:00:00";
            PlayerPrefs.SetString("Notifications_FIRST_TIME",timeStr);
        }
        DateTime nowTime2 = Convert.ToDateTime(timeStr);//本地时间
        return nowTime2;
    }
    private void CancelAlliOS() {
#if UNITY_IOS && !UNITY_EDITOR
        UnityEngine.iOS.NotificationServices.RegisterForNotifications(
            UnityEngine.iOS.NotificationType.Alert 
            | UnityEngine.iOS.NotificationType.Sound
            | UnityEngine.iOS.NotificationType.Badge);
        UnityEngine.iOS.LocalNotification l = new UnityEngine.iOS.LocalNotification (); 
        l.applicationIconBadgeNumber = 0; 
        UnityEngine.iOS.NotificationServices.PresentLocalNotificationNow (l); 
        UnityEngine.iOS.NotificationServices.CancelAllLocalNotifications (); 
        UnityEngine.iOS.NotificationServices.ClearLocalNotifications (); 
#endif
    }
    /// <summary>
    /// 第6关之后执行
    /// </summary>
    public void RegisterNoti() {
        if (_finish) {
            return;
        }
        _finish = true;
        NotificationManager.CancelAll();
        Noti_ANDROID_Time();
        CancelAlliOS();
        Invoke("Noti_IOS_TIME",3f);
        OneSignal.StartInit(AppSetting.OneSignalKey)
            .HandleNotificationOpened(HandleNotificationOpened)
            .EndInit();
        OneSignal.inFocusDisplayType = OneSignal.OSInFocusDisplayOption.Notification;
        
    }
    
    private void Noti_IOS_TIME() {
#if UNITY_IOS && !UNITY_EDITOR
        string zone = Zone();
        int cnt = 0;
        var times = GetAllLocalTime();
        var nowTime = DateTime.Now;
        for (int i = 0; i < times.Count; i++) {
            if (times[i] > nowTime) {
                var msg = _msgList[i%_msgList.Count];
                var notif = new UnityEngine.iOS.LocalNotification();
                notif.alertBody = msg;
                notif.fireDate = times[i];
                notif.timeZone = zone;
                UnityEngine.iOS.NotificationServices.ScheduleLocalNotification(notif);
                if(cnt++ > 20){
                    break;
                }
            }
        }
#endif
    }
    
    private List<DateTime> GetAllLocalTime() {
        List<DateTime> list = new List<DateTime>();
        DateTime firstLocalTime = FirstNotificationsLocalTime();
        for (int i = -1; i < 365;i++) {
            var time1 = firstLocalTime.AddDays(i);
            var time2 = firstLocalTime.AddDays(i).AddHours(4);
            list.Add(time1);
            list.Add(time2);
        }
        return list;
    }
    private void Noti_ANDROID_Time() {
#if UNITY_ANDROID && !UNITY_EDITOR
        var times = GetAllLocalTime();
        var nowTime = DateTime.Now;
        int cnt = 0;
        for (int i = 0; i < times.Count; i++) {
            if (times[i] > nowTime) {
                var msg = _msgList[i%_msgList.Count];
                var sec = (times[i] - nowTime).TotalSeconds;
                NotificationManager.SendWithAppIcon(TimeSpan.FromSeconds(sec), 
                    Application.productName, msg,
                    new Color(0, 0.6f, 1), NotificationIcon.Message);
                    if(cnt++ > 20){
                        break;
                    }
            }
        }
#endif
    }

    private String Zone() {
        int offset = TimeZone.CurrentTimeZone.GetUtcOffset(DateTime.Now).Hours;
        string sign = "";
        if(offset > 0) {
            sign = "+";
        }
        string zone = "GMT" + sign + offset;
        return zone;
    }
    
    private float SecondsFromTime(string time) {
        DateTime nowTime = DateTime.Now;//本地时间
        string timeStr = $"{nowTime.Year}-{nowTime.Month}-{nowTime.Day} {time}";
        DateTime nowTime2 = Convert.ToDateTime(timeStr).AddDays(1);//本地时间
        TimeSpan ts1 = new TimeSpan(nowTime.Ticks);
        TimeSpan ts2 = new TimeSpan(nowTime2.Ticks);
        TimeSpan tsSub = ts2.Subtract(ts1).Duration();
        float sec = tsSub.Hours * 60 * 60 + tsSub.Minutes * 60 + tsSub.Seconds;
        return sec;
    }
    private String RandomMsg() {
        var msg = _msgList[UnityEngine.Random.Range(0, _msgList.Count)];
        return msg;
    }

}
