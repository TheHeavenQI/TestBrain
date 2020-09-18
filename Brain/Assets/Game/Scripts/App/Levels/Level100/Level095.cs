
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Level095 : LevelBasePage {
    public List<DragMove> dragMoves;
    public Transform topCorrect;
    public Transform bottomCorrect;
    private int correctCount;
    protected override void Start() {
        base.Start();
        for (int i = 0; i < dragMoves.Count; i++) {
            var btn = dragMoves[i];
            btn.onDragEnd = ()=>{
                if (btn.name == "top") {
                    if (Vector2.Distance(btn.transform.localPosition, topCorrect.localPosition) < 50) {
                        correctCount += 1;
                        btn.transform.DOLocalMove(topCorrect.localPosition, 0.5f).OnComplete(() => {
                            if (correctCount == 2) {
                                Completion();
                            }
                        });
                        return;
                    }
                }
                if (btn.name == "bottom") {
                    if (Vector2.Distance(btn.transform.localPosition, bottomCorrect.localPosition) < 50) {
                        correctCount += 1;
                        btn.transform.DOLocalMove(bottomCorrect.localPosition, 0.5f).OnComplete(() => {
                            if (correctCount == 2) {
                                Completion();
                            }
                        });
                        return;
                    }
                }
                btn.Return2OriginPos(0.5f);
            };
        }   
    }
}
