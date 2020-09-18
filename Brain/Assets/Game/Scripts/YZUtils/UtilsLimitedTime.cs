using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtilsLimitedTime {
    private static DateTime _baseTime = new DateTime(1970, 1, 1, 0, 0, 0);
    private static int _sec = 60 * 60 - 1;
    public static int LimitedTime() {
        int limitedTime = -1;
        /// 已经去掉广告
       if (Global.isHideAD) {
           return limitedTime;
       }
      DateTime nowtime = DateTime.UtcNow;
      string today = nowtime.ToShortDateString();
      string key = $"UtilsLimitedTime_KEY";
      var oldDay = PlayerPrefs.GetString(key, "");
      var timestamp = (int)(DateTime.UtcNow - _baseTime).TotalSeconds;
      if (oldDay != today) {
          // 今天第一次显示
          PlayerPrefs.SetString(key,today);
          /// 存入今天的时间戳
          PlayerPrefs.SetInt("LimitedTime_timestamp",timestamp);
          limitedTime = _sec;
      }
      else {
          int old = PlayerPrefs.GetInt("LimitedTime_timestamp", 0);
          limitedTime = _sec - (timestamp - old);
      }
      return limitedTime;
    }
        
    
}
