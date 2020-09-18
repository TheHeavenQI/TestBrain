using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LanguageController : BaseController
{
    public override void Awake() {
        base.Awake();
        transform.Find("Content/en").GetComponent<Button>().onClick.AddListener(() => {
            ConfigManager.SetLanguage(LanguageType.en);
            Hide();
        });
        transform.Find("Content/cn").GetComponent<Button>().onClick.AddListener(() => {
            ConfigManager.SetLanguage(LanguageType.cn);
            Hide();
        });
    }
}
