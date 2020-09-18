using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Level022 : LevelBasePage {
    public List<DragMove> dragMoves;
    private List<Vector3> _poslist = new List<Vector3>();
    public DragMove dragMove1;
    public DragMove dragMove2;

    public GameObject smoke;

    protected override void Awake() {
        base.Awake();
        for (int i = 0; i < dragMoves.Count; i++) {
            var obj = dragMoves[i];
            _poslist.Add(obj.transform.localPosition);
        }

        dragMove1.onDragEnd = () => {
            if (Vector3.Distance(dragMove1.transform.localPosition,dragMove2.transform.localPosition) < 100) {
                dragMove2.transform.SetAsLastSibling();
                dragMove1.transform.SetAsLastSibling();
                dragMove1.transform.DOLocalMove(dragMove2.transform.localPosition, 0.5f).OnComplete(() => {
                    smoke.SetActive(true);
                    Completion();
                });
            }
        };
        
        dragMove2.onDragEnd = () => {
            if (Vector3.Distance(dragMove1.transform.localPosition,dragMove2.transform.localPosition) < 100) {
                dragMove2.transform.SetAsLastSibling();
                dragMove1.transform.SetAsLastSibling();
                dragMove2.transform.DOLocalMove(dragMove1.transform.localPosition, 0.5f).OnComplete(() => {
                    smoke.SetActive(true);
                    Completion();
                });
            }
        };

    }
    
    
    public override void Refresh() {
        base.Refresh();
        for (int i = 0; i < dragMoves.Count; i++) {
            var obj = dragMoves[i];
            obj.transform.localPosition = _poslist[i];
        }

        smoke.SetActive(false);

    }
}
