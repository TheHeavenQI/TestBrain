using UnityEngine;
using UnityEngine.UI;

public class Level230 : LevelBasePage
{
    public GameObject manCloseGo;
    public DragMoveEventTrigger fly;
    public RectTransform mouth;

    protected override void Start()
    {
        base.Start();

        fly.onEndDrag = (d) => {
            if (RectTransformExtensions.IsRectTransformOverlap(fly.rectTransform, mouth))
            {
                fly.gameObject.SetActive(false);
                manCloseGo.SetActive(true);
                Completion();
            }
        };
        fly.transform.SetAsLastSibling();
        manCloseGo.SetActive(false);
    }

    public override void Refresh()
    {
        fly.Return2OriginPos();
        base.Refresh();
    }
}
