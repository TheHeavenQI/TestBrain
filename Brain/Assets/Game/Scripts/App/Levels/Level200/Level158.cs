using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level158 : LevelBasePage {

    public DragMoveEventTrigger[] dragMoves;
    public List<Image> imageList;
    protected override void Start() {
        base.Start();
        for (int i = 0; i < imageList.Count; i++) {
            imageList[i].gameObject.SetActive(i != 2);
        }
        foreach (DragMoveEventTrigger dragMove in dragMoves) {
            dragMove.onEndDrag += (d) => OnEndDrag();
        }
    }
    
    private void OnEndDrag() {
        float offsetY = dragMoves[1].transform.localPosition.y - dragMoves[0].transform.localPosition.y;
        float offsetX = dragMoves[1].transform.localPosition.x - dragMoves[0].transform.localPosition.x;
        float margin = 15;
        if (offsetY >= -margin && offsetY <= margin && offsetX >= 229 - margin && offsetX <= 229 + margin) {
            for (int i = 0; i < imageList.Count; i++) {
                imageList[i].gameObject.SetActive(i == 2);
            }
            After(() => {
                Completion();
            },0.5f);
        }
    }

    public override void Refresh() {
        base.Refresh();
        for (int i = 0; i < imageList.Count; i++) {
            imageList[i].gameObject.SetActive(i != 2);
        }
        foreach (DragMoveEventTrigger dragMove in dragMoves) {
            dragMove.Return2OriginPos();
        }
    }
}
