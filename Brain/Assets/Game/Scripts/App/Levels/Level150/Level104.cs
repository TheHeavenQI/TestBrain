using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BaseFramework;

public class Level104 : SumClickLevel {

    public DragMove breadDM;
    public Attr dishAttr;
    public RectTransform dishTrans;
    public Text dishPriceText;

    private bool _isBreadOnDish = true;

    protected override void Start() {
        base.Start();
        breadDM.onDragEnd = () => {
            if (RectTransformExtensions.IsRectTransformOverlap(breadDM.rectTransform, dishTrans)) {
                if (!_isBreadOnDish) {
                    dishAttr.flag = 12;
                    dishPriceText.text = "$12";
                    _isBreadOnDish = true;
                }
            } else {
                if (_isBreadOnDish) {
                    dishAttr.flag = 2;
                    dishPriceText.text = "$2";
                    _isBreadOnDish = false;
                }
            }
        };
        breadDM.onClick = () => {
            if (_isBreadOnDish) {
                base.Click(0);
            }
        };
    }

    public override void Refresh() {
        base.Refresh();
        dishAttr.flag = 12;
        breadDM.Return2OriginPos();
        dishPriceText.text = "$12";
        _isBreadOnDish = true;
    }
}
