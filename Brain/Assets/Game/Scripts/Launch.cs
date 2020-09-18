using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Launch : MonoBehaviour {
    
    public RectTransform _processRect;
    public Image valueImage;
    public Text _processText;
    private Vector2 _processsizeDelta;
    private int _currentCount;
    private int _totalCount = 60;

    private void Start() {
        StartCoroutine("Init");
#if !Brain_Hero
        ConfigManager.Init();
#endif
        //      Global.TOTAL_LEVEL_COUNT = ConfigManager.Current().Questions.Count + 1;
    }
    
    private IEnumerator Init() {
        yield return 0;
        _processsizeDelta = _processRect.sizeDelta;
        while (_currentCount++ <= _totalCount) {
            var process = _currentCount / (float)_totalCount;
            if (process > 1) {
                process = 1;
            }
            var pro = string.Format("{0:F0}", process*100);
            _processText.text = $"{pro}%";
            if (_currentCount == _totalCount/2) {
                ControllerManager.Instance.Init(); 
            }
            _processRect.sizeDelta = new Vector2(_processsizeDelta.x*process,_processsizeDelta.y);
            valueImage.fillAmount = process;
            yield return 0;
        }
        transform.DOScale(new Vector3(0, 0, 0), 0.2f).OnComplete(() => {
            Destroy(this.gameObject);
        });
        MusicManager.Instance.PlayBgMusic();
    }

}
