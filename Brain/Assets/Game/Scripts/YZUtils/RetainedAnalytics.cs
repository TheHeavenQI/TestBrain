using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RetainedAnalytics
{
    private static int SaveFirstTimeStamp()
	{
		int timestamp = PlayerPrefs.GetInt(Constance.storage_first_open_timestamp, 0);
		/// 保存第一次打开app的时间
		if (timestamp == 0)
		{
			timestamp = Utils.currentTimeStamp;
			PlayerPrefs.SetInt(Constance.storage_first_open_timestamp, timestamp);
		}
        return timestamp;
	}
    public static void Analytics()
	{
		int firstTime = SaveFirstTimeStamp();
        int currentTimeStamp = Utils.currentTimeStamp;
        int day = (currentTimeStamp - firstTime) / (24 * 60 * 60);
        AnalyticsUtil.Log($"Day_Retained_{day+1}");
    }
}
