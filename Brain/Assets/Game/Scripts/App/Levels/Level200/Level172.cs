using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level172 : LevelBasePage {

    public DragMoveEventTrigger[] dragMoves;

    public DragMoveEventTrigger man;
    public DragMoveEventTrigger juice;
    public GameObject mSuc;
    protected override void Start() {
        base.Start();

        man.onEndDrag += (d) => OnEndDrag();
        juice.onEndDrag += (d) => OnEndDrag();
    }

    private void OnEndDrag() {
        float dis = Vector3.Distance(man.rectTransform.localPosition, juice.rectTransform.localPosition);
        if (dis <= 100) {
            mSuc.SetActive(true);
            juice.gameObject.SetActive(false);
            After(()=> { CompletionWithMousePosition(); },0.5f);           
        }
    }

    public override void Refresh() {
        base.Refresh();
        mSuc.SetActive(false);
        juice.gameObject.SetActive(true);
        foreach (DragMoveEventTrigger dragMove in dragMoves) {
            dragMove.Return2OriginPos();
        }
    }
}
