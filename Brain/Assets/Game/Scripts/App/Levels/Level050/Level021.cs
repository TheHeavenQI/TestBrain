using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Level021 : LevelBasePage {
    public List<DragMove> dragMoves;
    public DragMove yan1;
    public GameObject yan2;
    private float _pointerDownTime;
    private bool _isPress;
    private bool _finish;
    protected override void Awake() {
        base.Awake();
        for (int i = 0; i < dragMoves.Count; i++) {
            var obj = dragMoves[i];
            obj.onDragEnd = () => { obj.Return2OriginPos(0.5f);};
        }

        yan1.enabelDrag = false;
        yan1.onClick = () => {

            if (yan2.GetComponent<Image>().color.a > 0.01)
            {
                yan1.gameObject.GetComponent<Image>().DOFade(yan1.gameObject.GetComponent<Image>().color.a - 0.33f, 0.5f);
                yan2.gameObject.GetComponent<Image>().DOFade(yan1.gameObject.GetComponent<Image>().color.a - 0.33f, 0.5f).onComplete = () =>
                {
                    if (yan2.GetComponent<Image>().color.a <= 0.01)
                    {
                        yan1.gameObject.SetActive(false);
                        yan2.gameObject.SetActive(false);

                        After(() => {
                            Completion();
                        }, 0.25f);
                    }
                };
            }
            
        };
    }

    public override void Refresh()
    {
        base.Refresh();

        yan1.gameObject.SetActive(true);
        yan2.gameObject.SetActive(true);
        yan1.gameObject.GetComponent<Image>().DOFade(1, 0.5f);
        yan2.gameObject.GetComponent<Image>().DOFade(1, 0.5f);

    }
}
