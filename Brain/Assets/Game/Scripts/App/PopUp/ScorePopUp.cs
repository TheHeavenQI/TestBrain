using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScorePopUp : BasePopUp {

    public Action CloseAction;
    private GameObject _score;
    private GameObject _feedback;
    private InputField _inputField;
    private Button _okBtn;
    public List<Button> stars;
    public Sprite normalStarSprite;
    public Sprite selStarSprite;
    
    private int _star;

    public override void Awake() {
        base.Awake();
        _feedback = transform.Find("Content/Feedback").gameObject;
        _score = transform.Find("Content/Score").gameObject;
        _inputField = transform.Find("Content/Feedback/InputField").GetComponent<InputField>();
        _okBtn = transform.Find("Content/Feedback/OK").GetComponent<Button>();
        _okBtn.onClick.AddListener(() => {
            var txt = _inputField.text;
#if DEBUG
            Debug.Log($"_star:{_star},txt:{txt}");
#else
            RateUtil.Feedback(_star,txt);
#endif
            Hide();      
        });
        
        _inputField.onValueChanged.AddListener((value) => { _okBtn.interactable = !string.IsNullOrEmpty(value); });
        transform.Find("Content/Feedback/Close").GetComponent<Button>().onClick.AddListener(() => {
            Hide();
        });
        transform.Find("Content/Score/Close").GetComponent<Button>().onClick.AddListener(() => {
            Hide();
        });

        for (int i = 0; i < stars.Count; i++) {
            var index = i;
            stars[i].onClick.AddListener(() => {
                _star = index+1;
                Refresh();
                if (AppSetting.isIOS)
                {
                    After(() => {
                        ShowFeedback();
                    }, 0.5f);
                }
                else
                {
                    if (_star == 5)
                    {
                        RateUtil.RateGame();
                        Hide();
                    }
                    else
                    {
                        After(() =>
                        {
                            ShowFeedback();
                        }, 0.5f);
                    }
                }
            });
        }
    }

    private void Refresh() {
        for (int j = 0; j < stars.Count; j++) {
            var btn = stars[j];
            if (j < _star) {
                btn.image.sprite = selStarSprite;
            }
            else {
                btn.image.sprite = normalStarSprite;
            }
        }
    }
    
    protected override void OnEnable() {
        base.OnEnable();
        _star = -1;
        _inputField.text = null;
        Refresh();
    }

    public void ShowScore() {
        _feedback.SetActive(false);
        _score.SetActive(true);
    }
    public void ShowFeedback() {
        _feedback.SetActive(true);
        _score.SetActive(false);
    }
    public override void Hide() {
        base.Hide();
        CloseAction?.Invoke();
    }
}
