
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Level249 : LevelBasePage
{
    public List<Button> buttons;
    public Sprite close;
    public Sprite open;
    private bool _isStart;
    private Dictionary<int,int> _indexs = new Dictionary<int, int> { {0,0}, { 2, 0 }, { 5, 0 }, { 7, 0 }, { 9, 0 },
                                                                      { 13, 0 },
                                                                      { 22, 0 },{ 27, 0 },
                                                                      { 34, 0 },{ 38, 0 },
    };
    protected override void Start() {
        base.Start();
        Refresh();
        for (int i = 0; i < buttons.Count; i++)
        {
            int k = i;
            buttons[i].onClick.AddListener(() =>
            {
                if(!_isStart)
                {
                    return;
                }
                if (_indexs.ContainsKey(k))
                {
                    buttons[k].GetComponent<Image>().sprite = open;
                    _indexs[k] = 1;
                    CheckFinish();
                }
                else
                {
                    ShowError();
                    Refresh();
                }
            });
        }
    }

    public void CheckFinish()
    {
        bool isFinish = true;
        foreach(var pair in _indexs)
        {
            if(pair.Value != 1)
            {
                isFinish = false;
                break;
            }
        }
        if (isFinish)
        {
            Completion();
        }
    }
    public override void Refresh()
    {
        base.Refresh();
        _isStart = false;
        After(() =>
        {
            _isStart = true;
            for (int i = 0; i < buttons.Count; i++)
            {
                buttons[i].GetComponent<Image>().sprite = close;
            }
        }, 3);
        var dict = new Dictionary<int, int>();
        foreach(var pair in _indexs)
        {
            dict[pair.Key] = 0;
            if(pair.Key < buttons.Count)
            {
                buttons[pair.Key].GetComponent<Image>().sprite = open;
            } 
        }
        _indexs = dict;
    }
}
