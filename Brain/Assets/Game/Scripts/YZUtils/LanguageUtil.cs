using UnityEngine;

/// <summary>
/// 语言类型
/// </summary>
public enum LanguageType : ushort
{
    /// <summary>
    /// 英语
    /// </summary>
    en = 0,
    /// <summary>
    /// 中文
    /// </summary>
    cn = 1,
    /// <summary>
    /// 葡萄牙语
    /// </summary>
    pt = 2,
    /// <summary>
    /// 西班牙语
    /// </summary>
    es = 3,
    /// <summary>
    /// 德语
    /// </summary>
    de = 4,
    /// <summary>
    /// 日语
    /// </summary>
    ja = 5
}

public static class LanguageUtil
{
    public static LanguageType LocalLanguage()
    {
        SystemLanguage sl = Application.systemLanguage;
        LanguageType? language = null;
        switch (sl)
        {
            //case SystemLanguage.Chinese:
            //case SystemLanguage.ChineseSimplified:
            //case SystemLanguage.ChineseTraditional:
            //    _language = LanguageType.cn;
            //    break;
            case SystemLanguage.English:
                language = LanguageType.en;
                break;
            case SystemLanguage.Portuguese:
                language = LanguageType.pt;
                break;
            case SystemLanguage.Spanish:
                language = LanguageType.es;
                break;
            case SystemLanguage.German:
                language = LanguageType.de;
                break;
            case SystemLanguage.Japanese:
                language = LanguageType.ja;
                break;
            default:
                language = LanguageType.en;
                break;
        }
        return language.Value;
    }
}
