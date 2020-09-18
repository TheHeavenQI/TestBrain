using UnityEngine;
using BaseFramework;
using SceneManager = UnityEngine.SceneManagement.SceneManager;
public class ConfigManager
{
    private const string LanguageKey = "LanguageType";

    private static LanguageType? _language;
    private static ConfigModel _configModel;
    private static bool Inited = false;
    public static void Init()
    {
        if (Inited)
            return;
        Inited = true;
        CheckLanguage();
    }

    private static void CheckLanguage()
    {
        LanguageType languageType = LanguageType.en;
        if (PlayerPrefs.HasKey(LanguageKey))
        {
            languageType = (LanguageType)(PlayerPrefs.GetInt(LanguageKey));
        }
        else
        {
            languageType = LanguageUtil.LocalLanguage();
        }
        if (AppSetting.isEditor)
        {
            languageType = LanguageType.en;
        }
        string textConfig = Resources.Load<TextAsset>("Config/TextConfig").text;
        Localization.Init(textConfig, languageType.ToString());
        SetLanguage(languageType);
    }

    /// <summary>
    /// 切换语言
    /// </summary>
    /// <param name="language"></param>
    public static void SetLanguage(LanguageType language)
    {
        if (language == _language)
        {
            return;
        }

        _language = language;
        _configModel = new ConfigModel(language);

        //PlayerPrefs.SetInt(LanguageKey, (int)language);
        Localization.SetLanguage(language.ToString());
        EventCenter.Broadcast(UtilsEventType.LanguageSwitch);
    }

    /// <summary>
    /// 获取当前语言
    /// </summary>
    /// <returns></returns>
    public static LanguageType GetLanguage()
    {
        return _language == null ? LanguageType.en : _language.Value;
    }

    /// <summary>
    /// 获取当前配置
    /// </summary>
    /// <returns></returns>
    public static ConfigModel Current()
    {
        return _configModel;
    }
}
