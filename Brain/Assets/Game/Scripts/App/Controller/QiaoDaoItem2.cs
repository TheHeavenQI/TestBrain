using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BaseFramework;

public class QiaoDaoItem2 : MonoBehaviour {
    public GameObject okGameObject;
    public Text tipNumText;
    public Text dayText;
    public GameObject okImage;
    
    public void SetDay(int day, int tipNum) {
        dayText.text = $"{day} {Localization.GetText("dlg_back_day")}";
        tipNumText.text = $"X{tipNum}";
    }

    /// <summary>
    /// 0: 已签到 1: 可以签到 2:不可以签到
    /// </summary>
    /// <param name="type"></param>
    public void SetType(int type) {
        okGameObject.SetActive(false);
        okImage.SetActive(false);
        if (type == 0) {
            okGameObject.SetActive(true);
            okImage.SetActive(true);
        }
        else if(type == 1) {
            okGameObject.SetActive(true);
        }
    }
}
