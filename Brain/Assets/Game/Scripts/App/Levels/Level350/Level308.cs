using UnityEngine;
using UnityEngine.UI;

public class Level308 : LevelBasePage
{
    public Button appleBtn;
    public DragMoveEventTrigger hammer;
    public RectTransform deskBottom;
    public DragMoveEventTrigger deskLeg1;
    public DragMoveEventTrigger deskLeg2;
    public GameObject human;

    private int _step = 0;

    protected override void Start()
    {
        base.Start();

        human.SetActive(false);

        appleBtn.onClick.AddListener(() => {
            if (_step == 0)
            {
                _step = -1;
                ShowError();
                After(Refresh, 1);
                human.SetActive(true);
            }
            else if (_step == 1)
            {
                Completion();
            }
        });

        deskLeg1.onEndDrag = (d) => OnDeskLegEndDrag();
        deskLeg2.onEndDrag = (d) => OnDeskLegEndDrag();
    }

    private void OnDeskLegEndDrag()
    {
        if (_step == -1)
        {
            return;
        }
        if (!deskLeg1.rectTransform.IsRectTransformOverlap(deskBottom)
            && !deskLeg2.rectTransform.IsRectTransformOverlap(deskBottom))
        {
            _step = 1;
        }
        else
        {
            _step = 0;
        }
    }

    public override void Refresh()
    {
        base.Refresh();
        _step = 0;
        human.SetActive(false);
        deskLeg1.Return2OriginPos();
        deskLeg2.Return2OriginPos();
        hammer.Return2OriginPos();
    }
}
