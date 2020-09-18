using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using System;

public class QuestionModel : IComparable<QuestionModel>
{
    public int id;
    public string name;
    public string question;
    public string tip;
    public string completeTip;

    public int CompareTo(QuestionModel other)
    {
        return this == null ? -1 : other == null ? 1 : this.id.CompareTo(other.id);
    }
}

public class QuestionModelList
{
    private List<QuestionModel> _models;
    public int Count => _models.Count;
    public QuestionModel GetModel(int index)
    {
        return _models[index % _models.Count];
    }

    public QuestionModelList(TextAsset textAsset)
    {
        string text = textAsset.text;
        _models = JsonConvert.DeserializeObject<List<QuestionModel>>(text);
        _models.Sort();
    }

}
