using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanguageItemSwitch : MonoBehaviour
{
    //public GameObject cnGo;
    public GameObject enGo;
    public GameObject ptGo;
    public GameObject esGo;
    public GameObject deGo;
    public GameObject jaGo;

    private void Awake()
    {
        LanguageType type = ConfigManager.GetLanguage();

        //cnGo?.SetActive(type == LanguageType.cn);
        enGo?.SetActive(type == LanguageType.en);
        ptGo?.SetActive(type == LanguageType.pt);
        esGo?.SetActive(type == LanguageType.es);
        deGo?.SetActive(type == LanguageType.de);
        jaGo?.SetActive(type == LanguageType.ja);
    }
}
