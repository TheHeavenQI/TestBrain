using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BaseFramework;

public class HudWait : MonoBehaviour
{
    private static int _waitedNum = 0;
    public static HudWait Instance;
    private void Awake()
    {
        Instance = this;
        Instance.gameObject.SetActive(false);
    }
    public static void ShowWaiting()
    {
        Instance.gameObject.SetActive(true);
        _waitedNum++;
        AfterHideWaiting(5);
    }

    private static void AfterHideWaiting(float delay)
    {
        int num = _waitedNum;
        TaskHelper.Create<CoroutineTask>()
            .Delay(delay)
            .Do(() => {
                if (num == _waitedNum)
                {
                    HideWaiting();
                }
            })
            .Execute();
    }
    public static void HideWaiting()
    {
        Instance.gameObject.SetActive(false);
    }
}
