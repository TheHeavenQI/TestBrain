using UnityEngine;
using UnityEngine.UI;

public class Level222 : LevelBasePage
{
    public Button[] btns;
    public RectTransform boy;
    public DragMoveEventTrigger liftRing;
    public Image savedBoy;

    protected override void Start()
    {
        base.Start();

        foreach (Button btn in btns)
        {
            btn.onClick.AddListener(() => ShowErrorWithMousePosition());
        }
        liftRing.onPointerClick = (d) => ShowErrorWithMousePosition();
        liftRing.onDrag = (d) => {
            if (RectTransformExtensions.IsRectTransformOverlap(boy, liftRing.rectTransform))
            {
                liftRing.gameObject.SetActive(false);
                savedBoy.enabled = true;
                Completion();
            }
        };

        savedBoy.enabled = false;
    }


    public override void Refresh()
    {
        base.Refresh();
        liftRing.Return2OriginPos();
        liftRing.gameObject.SetActive(true);

        savedBoy.enabled = false;
    }
}
