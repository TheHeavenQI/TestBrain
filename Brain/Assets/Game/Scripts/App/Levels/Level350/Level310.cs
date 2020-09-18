using UnityEngine;
using UnityEngine.UI;

public class Level310 : LevelBasePage
{
    public Button appleBtn;
    public DragMoveEventTrigger hammer;
    public DragMoveEventTrigger deskLeg1;
    public DragMoveEventTrigger deskLeg2;
    public RectTransform human;

    private int _step = 0;

    protected override void Start()
    {
        base.Start();

        appleBtn.onClick.AddListener(() => {
            if (_step == 0)
            {
                ShowError();
            }
            else if (_step == 1)
            {
                CompletionWithMousePosition();
            }
        });

        hammer.onEndDrag = (d) => {
            if (hammer.rectTransform.IsRectTransformOverlap(human))
            {
                _step = 1;
                human.gameObject.SetActive(false);
            }
        };
    }

    public override void Refresh()
    {
        base.Refresh();
        hammer.Return2OriginPos();
        deskLeg1.Return2OriginPos();
        deskLeg2.Return2OriginPos();
        human.gameObject.SetActive(true);
    }
}
