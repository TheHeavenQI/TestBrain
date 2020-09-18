
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class DragMovePosition : LevelBasePage {
    public DragMove[] mStones;
    public Transform bottleMouse;
    public DragMove dragObject;
    
    public RectTransform collisionRect;
    public Transform answerLoc;
    private Vector3 _orgVector3;
    protected override void Awake() {
        base.Awake();
        _orgVector3 = dragObject.transform.localPosition;
        var rect = dragObject.transform.GetComponent<RectTransform>();
        
        dragObject.onDragEnd = () => {
            if (RectTransformExtensions.IsRectTransformOverlap(rect, collisionRect)) {
                dragObject.transform.DOLocalMove(answerLoc.localPosition, 0.5f).OnComplete(() => {
                    Completion();
                });
            }
            else {
                dragObject.transform.DOLocalMove(_orgVector3, 0.5f).OnComplete(() => {
                    ShowError();
                });
            }
        };

        foreach (var item in mStones)
        {
            item.onDragEnd = () =>
            {
                if (Vector3.Distance(bottleMouse.localPosition + bottleMouse.parent.localPosition, item.transform.localPosition) < 50)
                {
                    int x = Random.Range(-30,30);
                    int y = -160;
                    Vector3 target =item.transform.localPosition + new Vector3(x,y,0);
                    item.transform.DOLocalMove(target,0.2f);
                }
                else
                    item.Return2OriginPos();
            };
        }
    }
    public override void Refresh()
    {
        base.Refresh();
        foreach (var item in mStones)
            item.Return2OriginPos();
        dragObject.Return2OriginPos();
    }
}
