using UnityEngine;
using DG.Tweening;

public class Level236 : LevelBasePage
{
    public RectTransform fissureBig;
    public RectTransform fissureSmall;
    public RectTransform hand;

    public DragMoveEventTrigger[] beans;
    public DragMoveEventTrigger hammer;

    private Shake _shake;
    private bool _isHammerDown;

    protected override void Start()
    {
        base.Start();

        beans[2].gameObject.SetActive(false);
        fissureBig.gameObject.SetActive(false);
        fissureSmall.gameObject.SetActive(true);

        hammer.gameObject.SetActive(true);
        hammer.enableDragMove = false;
        hammer.onEndDrag = (d) => {
            if (RectTransformExtensions.IsRectTransformOverlap(hammer.rectTransform, fissureSmall))
            {
                beans[2].gameObject.SetActive(true);
                fissureBig.gameObject.SetActive(true);
                fissureSmall.gameObject.SetActive(false);
                hammer.gameObject.SetActive(false);
            }
        };

        _shake = this.GetComponent<Shake>();
        _shake.shakeAction = () => {
            if (_isHammerDown)
            {
                return;
            }
            _isHammerDown = true;
            hammer.rectTransform.DOLocalMove(Vector3.zero, 0.5f).OnComplete(() => {
                hammer.enableDragMove = true;
            });
        };

        foreach (var dm in beans)
        {
            dm.onEndDrag = (d) => CheckFinish();
        }
    }

    public override void Refresh()
    {
        base.Refresh();

        beans[2].gameObject.SetActive(false);
        fissureBig.gameObject.SetActive(false);
        fissureSmall.gameObject.SetActive(true);

        hammer.enableDragMove = false;
        hammer.Return2OriginPos();
        hammer.gameObject.SetActive(true);
        _isHammerDown = false;

        foreach (var dm in beans)
        {
            dm.Return2OriginPos();
        }
    }

    private void CheckFinish()
    {
        bool isAllNear = true;
        foreach (var dm in beans)
        {
            if (!RectTransformExtensions.IsRectTransformOverlap(hand, dm.rectTransform))
            {
                isAllNear = false;
                break;
            }
        }
        if (isAllNear)
        {
            Completion();
        }
    }
}
