using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class BaseCanvas : MonoBehaviour {
    protected virtual int showType => 0;
    protected virtual bool autoShow => true;
    protected GameObject _content;
    protected Button BackBtn;
    
    /// <summary>
    /// 偏移量
    /// </summary>
    protected virtual float marginY => 0;
    
    public virtual void Awake() {
        _content = transform.Find("Content").gameObject;
        
        var back = transform.Find("Content/Back");
        if (back) {
            var btn = back.GetComponent<Button>();
            if (btn) {
                btn.onClick.AddListener(() => {
                    ClickBack();
                });
            }
        }
        EventCenter.AddListener(UtilsEventType.LanguageSwitch,LanguageSwitch);
    }

    public virtual void Start() {
        LanguageSwitch();
    }

    private void OnDestroy() {
        EventCenter.RemoveListener(UtilsEventType.LanguageSwitch,LanguageSwitch);
    }

    protected virtual void LanguageSwitch() {
        
    }
    
    protected virtual void OnEnable() {
        var a = transform.GetComponent<Canvas>();
        if (a) {
            Global.layerOrder += 100;
            a.sortingOrder = Global.layerOrder;
        }
        if (autoShow) {
            if (showType == 1) {
                _content.transform.localPosition = new Vector3(-750, 0, 0);
            }else if (showType == 2) {
                _content.transform.localScale = new Vector3(0,0,0);
                _content.transform.localPosition = new Vector3(0,marginY,0);
            }
            StartCoroutine(AutoShowView());
        }
    }

    private IEnumerator AutoShowView() {
        yield return 1;
        Show();
    }
    public void After(Action action, float delay) {
        StartCoroutine(AfterDoEvent(action,delay));
    }
    
    private IEnumerator AfterDoEvent(Action action, float delay) {
        yield return new WaitForSeconds(delay);
        action?.Invoke();
    }
    
    
    public virtual void Show() {
        viewWillAppear();
        if (showType == 1) {
            _content.transform.DOLocalMoveX(0, 0.25f).OnComplete(() => {
                viewDidAppear();
            });
        }else if (showType == 2) {
            _content.transform.DOScale(new Vector3(1, 1, 1), 0.25f).OnComplete(() => {
                viewDidAppear();
            });
        }
        else {
            viewDidAppear();
        }
    }

    
    public virtual void ClickBack() {
        Hide();
    }

    public virtual void Hide() {
        viewWillDisappear();
        if (showType == 1) {
            _content.transform.DOLocalMoveX(-750, 0.25f).OnComplete(() => {
                gameObject.SetActive(false);
                viewDidDisappear();
            }); 
        }else if (showType == 2) {
            _content.transform.DOScale(new Vector3(0, 0, 0), 0.25f).OnComplete(() => {
                gameObject.SetActive(false);
                viewDidDisappear();
            });
        }
        else {
            viewDidDisappear();
        }
    }
    
    public virtual void viewWillAppear() {
        
    }
    public virtual void viewDidAppear() {
        
    }
    public virtual void viewWillDisappear() {
        
    }
    public virtual void viewDidDisappear() {
        
    }
}
