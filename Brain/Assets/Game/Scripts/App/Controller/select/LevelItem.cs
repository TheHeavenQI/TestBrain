using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BaseFramework;

public class LevelItem : MonoBehaviour {
    private int _index;
    public Sprite soildSprite;
    public Sprite whiteSprite;
    public Sprite suoSprite;
    public Sprite starSprite;

    public Sprite header1Sprite;
    public Sprite header2Sprite;
    
    private Text _name;
    private Image _star;
    private Image _header;
    [HideInInspector]
    public Action<int> clickAction;

    private int _type;
    private Image _contentImage;
    private void Awake() {
        _name = transform.Find("Name").GetComponent<Text>();
        _star = transform.Find("Star").GetComponent<Image>();
        _header = transform.Find("Header").GetComponent<Image>();
        _contentImage = transform.GetComponent<Image>();
    }

    private void Start() {
        GetComponent<Button>().onClick.AddListener(() => {
            var model = UserModel.Get();
            if (_type == 2) {
                if (_index <= model.christmasMaxId) {
                    clickAction?.Invoke(_index);
                }
            }
            else {
                if (_index <= model.levelMaxId) {
                    clickAction?.Invoke(_index);
                }
            }
            
        });
    }

    public void SetType(int type) {
        _type = type;
        _header.gameObject.SetActive(type == 2);
    }

    private void SetChristmasIndex(int index) {
        var model = UserModel.Get();
        _index = index;
        var id = ConfigManager.Current().Activities.christ.GetModel(index).id;
        _name.text = $"{Localization.GetText("main_lv")}{id}";
        SetSelect(index <= model.christmasMaxId);
    }
    public void SetIndex(int index) {
        if (_type == 2) {
            SetChristmasIndex(index);
            return;   
        }
        var model = UserModel.Get();
        _index = index;
        if (index >= ConfigManager.Current().Questions.Count) {
            _star.gameObject.SetActive(true);
            SetSelect(index <= model.levelMaxId);
            enabled = true;
            var maxID = ConfigManager.Current().Questions
                        .GetModel(ConfigManager.Current().Questions.Count - 1).id;
            if (model.levelMaxId < maxID - 2) {
                enabled = false;
            }
            _name.text = $"{Localization.GetText("main_lv")}{maxID+1}";
            return;
        }
        
        _star.gameObject.SetActive(true);
        var id = ConfigManager.Current().Questions.GetModel(index).id;
        _name.text = $"{Localization.GetText("main_lv")}{id}";
        SetSelect(index < model.levelMaxId);
        
    }

    private void SetSelect(bool able) {
        if (able) {
             enabled = true; 
             _star.sprite = starSprite;
            _contentImage.sprite = soildSprite;
            _header.sprite = header1Sprite;
        }
        else {
            enabled = false;
            _star.sprite = suoSprite;
            _contentImage.sprite = whiteSprite;
            _header.sprite = header2Sprite;
        }
        _star.SetNativeSize();
    }
}
