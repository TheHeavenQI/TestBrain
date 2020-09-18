using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Level126 : LevelBasePage {
    public Button[] btns;
    public EventCallBack catEvent;
    public GameObject shou;
    private int count = 0;
    private bool _isAnim = false;
    protected override void Start() {
        base.Start();
        foreach (Button btn in btns) {
            btn.onClick.AddListener(() => {
                ShowErrorWithMousePosition();
            });
        }
        
        catEvent.onSwipe = (a) => {
            if (_isAnim) {
                return;
            }
            count+=1;
            _isAnim = true;
            shou.SetActive(true);
            shou.transform.localPosition = new Vector3(-122,86,0);
            shou.transform.DOLocalMove(new Vector3(-177,11,0),1).OnComplete(() => {
                shou.SetActive(false);
                _isAnim = false;
                if (count >= 2) {
                    CompletionWithMousePosition();
                }
            });
        };
        
        shou.SetActive(false);
    }

    public void OnCompelte() {
        shou.gameObject.SetActive(false);
    }
    public override void Refresh() {
        base.Refresh();
        count = 0;
        shou.transform.localPosition = new Vector3(-122,86,0);
        shou.SetActive(false);
        _isAnim = false;
    }
}
