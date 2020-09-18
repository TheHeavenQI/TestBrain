
using UnityEngine;
using UnityEngine.UI;

public class Level235 : LevelBasePage {
    public GameObject obj;
    public DragMove dragMove;
    protected override void Start() {
        base.Start();
        var dragMoveRect = dragMove.transform.GetComponent<RectTransform>();
        var parentRect = transform.GetComponent<RectTransform>();
        dragMove.onDragEnd = () => {
            float h = parentRect.rect.height;
            float w = parentRect.rect.width;
            float absx = Mathf.Abs(dragMoveRect.localPosition.x);
            float absy = Mathf.Abs(dragMoveRect.localPosition.y);
            if (absx > w/2.0f || absy > h/2.0f ) {
                obj.SetActive(true);
                After(() => {
                    Completion();
                }, 1);
            }
        };
    }

    public override void Refresh() {
        base.Refresh();
        obj.SetActive(false);
        dragMove.Return2OriginPos();
    }
}
