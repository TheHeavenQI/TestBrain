using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections.Generic;

public class Level300 : LevelBasePage
{
    public Button shotBtn;
    public LimitDragMoveEventTrigger drat;
    public LimitDragMoveEventTrigger apple;
    public RectTransform dratUp;
    public RectTransform dratDownPath;
    private List<Vector3> _downPath = new List<Vector3>();

    private bool _isShoting;

    protected override void Start()
    {
        base.Start();

        dratUp.gameObject.SetActive(false);
        foreach (RectTransform rt in dratDownPath)
        {
            _downPath.Add(rt.localPosition);
            rt.gameObject.SetActive(false);
        }

        drat.onPointerClick = (d) => Shot();
        shotBtn.onClick.AddListener(Shot);
    }

    public override void Refresh()
    {
        base.Refresh();
        drat.Return2OriginPos();
        apple.Return2OriginPos();
        _isShoting = false;
    }

    private void Shot()
    {
        if (_isShoting)
        {
            return;
        }
        _isShoting = true;

        if (drat.transform.localPosition.y >= dratUp.localPosition.y)
        {
            drat.transform.DOLocalMoveX(450, 1).OnComplete(() => {
                ShowError();
                After(Refresh, 0.5f);
            });
        }
        else
        {
            drat.rectTransform.DOLocalPath(_downPath.ToArray(), 1).OnComplete(() => {
                if (drat.rectTransform.IsRectTransformOverlap(apple.rectTransform))
                {
                    Completion();
                }
                else
                {
                    ShowError();
                    After(Refresh, 0.5f);
                }
            });
        }
    }
}
