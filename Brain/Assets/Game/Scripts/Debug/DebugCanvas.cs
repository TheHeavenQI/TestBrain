using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugCanvas : MonoBehaviour {
    public GameObject content;
    public Button addKeyButton;
    public Button hideAdButton;
    public Button UnlockButton;
    private static int pausedCount = 0;
    private void Start() {
        content.SetActive(false);
        hideAdButton.onClick.AddListener(() => { Global.isHideAD = true; });
        addKeyButton.onClick.AddListener(() => {
            EventCenter.Broadcast(UtilsEventType.OnTipNumModify,10);
        });
        UnlockButton.onClick.AddListener(() => {
            UserModel.Get().levelMaxId = ConfigManager.Current().Questions.Count;
            UserModel.Get().christmasMaxId = ConfigManager.Current().Activities.christ.Count;
        });
    }
    
    public static void ShowDebug() {
       GameObject prefab = Resources.Load<GameObject>("Debug/DebugCanvas");
       GameObject obj = Instantiate(prefab); 
       obj.SetActive(true);
    } 
    
    private void OnApplicationPause(bool isPaused) {
        pausedCount += 1;
        if (pausedCount >= 1 && pausedCount % 5 == 0) {
            content.SetActive(true);
        }
        else {
            content.SetActive(false);
        }
    }
}
