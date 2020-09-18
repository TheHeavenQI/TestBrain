using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BaseFramework;

public class HudMsg : MonoBehaviour
{
    private static int _hintNum = 0;
    public Text msgText;
    private Text _text;
    public static HudMsg Instance;
    private void Awake()
    {
        Instance = this;
        _text = GetComponent<Text>();
        Instance.gameObject.SetActive(false);
    }
    public static void ShowMsg(string msg)
    {
        Instance._text.text = msg;
        Instance.msgText.text = msg;
        Instance.gameObject.SetActive(true);
        _hintNum++;
        AfterHideMsg(3);
    }
    private static void AfterHideMsg(float delay)
    {
        int num = _hintNum;
        TaskHelper.Create<CoroutineTask>()
            .Delay(delay)
            .Do(() => {
                if (num == _hintNum)
                {
                    HideMsg();
                }
            })
            .Execute();
    }
    private static void HideMsg()
    {
        Instance.gameObject.SetActive(false);
    }
}
