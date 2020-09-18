using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class Level221 : LevelBasePage
{
    public Button[] btns;
    public RectTransform bugRect;
    public LimitDragMoveEventTrigger dragMove;
    public Image bugFoundImg;
    public RectTransform glassesRect;
    public RectTransform clockRect;

    private bool _isBigger = false;

    private Vector3 _glassOffset;

    protected override void Start()
    {
        base.Start();
        bugFoundImg.enabled = false;

        foreach (Button button in btns)
        {
            button.onClick.AddListener(() => ShowErrorWithMousePosition());
        }

        dragMove.onPointerClick = (d) => ShowErrorWithMousePosition();

        dragMove.onDrag = (d) => {
            if (isLevelComplete)
            {
                return;
            }

            if (!_isBigger)
            {
                if (!RectTransformExtensions.IsRectTransformOverlap(clockRect, dragMove.rectTransform))
                {
                    _isBigger = true;
                    dragMove.rectTransform.DOScale(1, 0.3f);
                }
            }

            if (RectTransformExtensions.IsRectTransformOverlap(bugRect, glassesRect))
            {
                bugFoundImg.enabled = true;
                Completion();
                dragMove.enableDragMove = false;
            }
        };

        _glassOffset = glassesRect.position - dragMove.transform.position;
    }

    public void Update()
    {
        glassesRect.position = dragMove.transform.position + _glassOffset;
    }

    public override void Refresh()
    {
        base.Refresh();
        dragMove.transform.localScale = new Vector3(0.63f, 0.63f, 0.63f);
        bugFoundImg.enabled = false;
        dragMove.Return2OriginPos();
        dragMove.enableDragMove = true;
        _isBigger = false;
    }
}
