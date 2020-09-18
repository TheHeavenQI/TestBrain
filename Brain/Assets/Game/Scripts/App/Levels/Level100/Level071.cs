using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Level071 : LevelBasePage {

    public DragMove keyObj;
    public DragMove shuiTong;
    public DragMove shitou;
    public DragMove chuizi;

    public RectTransform correntRectTransform;
    private bool _rotated;
    private Tweener _tweener;
    protected override void Start() {
        base.Start();
        keyObj.onDragEnd = () => {
            var rect = keyObj.GetComponent<RectTransform>();
            if (RectTransformExtensions.IsRectTransformOverlap(correntRectTransform,rect)) {
                Completion();
            }
        };
        shuiTong.onDrag = () => {
            if (!keyObj.gameObject.activeSelf) {
                keyObj.transform.localPosition = shuiTong.transform.localPosition;
            }
        };
        keyObj.transform.localPosition = shuiTong.transform.localPosition;
        keyObj.gameObject.SetActive(false);
        
    }

    private void Update() {
        if (!_rotated && Input.acceleration.y > 0.8f) {
            _rotated = true;
            keyObj.gameObject.SetActive(true);
            _tweener = keyObj.transform.DOLocalMove(keyObj.transform.localPosition + new Vector3(0, 150, 0), 0.5f);
        }
    }

    public override void Refresh() {
        base.Refresh();
        _rotated = false;
        _tweener?.Kill();
        shuiTong.Return2OriginPos();
        shitou.Return2OriginPos();
        chuizi.Return2OriginPos();
        keyObj.gameObject.SetActive(false);
        keyObj.transform.localPosition = shuiTong.transform.localPosition;
    }
}
