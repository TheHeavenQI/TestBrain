using UnityEngine;

public class ConfigModel
{
    public readonly LanguageType languageType;

    private QuestionModelList _questionModelList;
    private ActivityQuestionModel _activityQuestionModel;

    private const string questionCfgPath = "Config/{0}/QuestionConfig";
    private const string activityCfgPath = "Config/{0}/Activity/ActivityQuestionConfig";

    public ConfigModel(LanguageType languageType)
    {
        this.languageType = languageType;

        string qPath = string.Format(questionCfgPath, languageType.ToString());
        TextAsset questionConfig = Resources.Load<TextAsset>(qPath);
        _questionModelList = new QuestionModelList(questionConfig);

        string aPath = string.Format(activityCfgPath, languageType.ToString());
        ActivityQuestionConfig activityQuestionConfig = Resources.Load<ActivityQuestionConfig>(aPath);
        _activityQuestionModel = new ActivityQuestionModel();
        _activityQuestionModel.christ = new QuestionModelList(activityQuestionConfig.christ);
    }

    public QuestionModelList Questions => _questionModelList;

    public int GetQuestionCount()
    {
        if (Global.christmas)
        {
            return ConfigManager.Current().Activities.christ.Count;
        }
        else
        {
            return ConfigManager.Current().Questions.Count + 1;
        }
    }

    public QuestionModel GetQuestionModel(int index)
    {
        QuestionModel model = null;
        if (Global.christmas)
        {
            model = ConfigManager.Current().Activities.christ.GetModel(index);
        }
        else
        {
            model = ConfigManager.Current().Questions.GetModel(index);
        }
        return model;
    }

    public ActivityQuestionModel Activities => _activityQuestionModel;
}
