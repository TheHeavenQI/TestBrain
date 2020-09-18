using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtilsLog 
{

    public static void Log(string value) {
        if (AppSetting.debug)
        {
            Debug.Log(value);
        }
    }
    public static void LogWarning(string value) {
        if (AppSetting.debug)
        {
            Debug.LogWarning(value);
        }
    }
    public static void LogError(string value) {
        if (AppSetting.debug)
        {
            Debug.LogError(value);
        }
    }

}
