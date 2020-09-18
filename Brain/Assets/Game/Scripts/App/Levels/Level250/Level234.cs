
using DG.Tweening;
using UnityEngine;

public class Level234 : LevelBasePage
{
    public DragMove gutou;
    public DragMove dog;
    public DragMove boom;
    public DragMove qiang;
    protected override void Start() {
        base.Start();
        var parentRect = transform.GetComponent<RectTransform>();
        gutou.onDragEnd = () => {
            float h = parentRect.rect.height;
            float w = parentRect.rect.width;
            float absx = Mathf.Abs(gutou.transform.localPosition.x);
            float absy = Mathf.Abs(gutou.transform.localPosition.y);
            if (absx > w/2.0f || absy > h/2.0f ) {
                dog.transform.DOLocalMove(gutou.transform.localPosition, 0.5f).OnComplete(() => {
                    Completion();
                });
            }
            else {
                gutou.Return2OriginPos(0.5f);
            }
        };
        boom.onDragEnd = () => { boom.Return2OriginPos(0.5f); };
        qiang.onDragEnd = () => { qiang.Return2OriginPos(0.5f); };
        dog.onDragEnd = () => { dog.Return2OriginPos(0.5f); };
    }
    
}
