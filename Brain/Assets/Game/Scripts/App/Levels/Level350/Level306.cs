using UnityEngine;
using UnityEngine.UI;

public class Level306 : LevelBasePage
{
    public GameObject door2;
    public Button appleBtn;
    public DragMoveEventTrigger twig1;
    public RectTransform twig2;
    public DragMoveEventTrigger hammer;

    private int _step = 0;

    protected override void Start()
    {
        base.Start();

        door2.SetActive(false);
        twig2.gameObject.SetActive(false);

        appleBtn.onClick.AddListener(() => {
            if (_step == 0)
            {
                door2.SetActive(true);
                ShowError();
                _step = -1;
                After(Refresh, 1);
            }
            else if (_step == 1)
            {
                CompletionWithMousePosition();
            }
        });

        twig1.onEndDrag = (d) => {
            if (twig1.rectTransform.IsRectTransformOverlap(twig2))
            {
                _step = 1;
                twig1.enableDragMove = false;
                twig1.transform.localPosition = twig2.transform.localPosition;
            }
        };
    }


    public override void Refresh()
    {
        base.Refresh();

        _step = 0;
        door2.SetActive(false);
        twig2.gameObject.SetActive(false);
        twig1.Return2OriginPos();
        twig1.enableDragMove = true;
        hammer.Return2OriginPos();
    }
}
