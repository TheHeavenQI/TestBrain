using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelRowItem : MonoBehaviour {
    public List<LevelItem> mItemList;
    private RectTransform _rectTransform;
    /// <summary>
    /// 1:普通 2:圣诞
    /// </summary>
    private int _type;

    private void Awake() {
        _rectTransform = GetComponent<RectTransform>();
    }
    public void SetType(int type) {
        _type = type;
        if (type == 2) {
            var a = _rectTransform.sizeDelta;
            _rectTransform.sizeDelta = new Vector2(a.x,468);
        }
        else {
            var a = _rectTransform.sizeDelta;
            _rectTransform.sizeDelta = new Vector2(a.x,368);
        }
    }

    
    
}
