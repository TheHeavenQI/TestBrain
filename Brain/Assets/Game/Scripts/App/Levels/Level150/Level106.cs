using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level106 : SumClickLevel
{
    public DragMove candyDM;
    public Attr groupAttr;
    public RectTransform groupTrans;
    public Text groupPriceText;

    private bool _isCandyInGroup = true;

    protected override void Start() {
        base.Start();
        candyDM.onDragEnd = () => {
            if (RectTransformExtensions.IsRectTransformOverlap(candyDM.rectTransform, groupTrans)) {
                if (!_isCandyInGroup) {
                    groupAttr.flag = 3;
                    groupPriceText.text = "$3";
                    _isCandyInGroup = true;
                }
            } else {
                if (_isCandyInGroup) {
                    groupAttr.flag = 2;
                    groupPriceText.text = "$2";
                    _isCandyInGroup = false;
                }
            }
        };
        candyDM.onClick = () => {
            if (_isCandyInGroup) {
                base.Click(2);
            }
        };
    }

    public override void Refresh() {
        base.Refresh();
        groupAttr.flag = 3;
        candyDM.Return2OriginPos();
        groupPriceText.text = "$3";
        _isCandyInGroup = true;
    }
}
