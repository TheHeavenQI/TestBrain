using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Level229 : LevelBasePage
{
    public Image water;
    public Image rain;
    public LimitDragMoveEventTrigger cloud;
    public DragMoveEventTrigger[] dragMoves;

    private Coroutine _cor;

    protected override void Start()
    {
        base.Start();

        for (int i = 0; i < dragMoves.Length; ++i)
        {
            int j = i;
            dragMoves[j].onEndDrag = (d) => dragMoves[j].Return2OriginPos();
        }

        water.gameObject.SetActive(false);
        rain.gameObject.SetActive(false);

        cloud.onEndDrag = (d) => {
            if (cloud.transform.localPosition.x >= -126
                && cloud.transform.localPosition.x <= 30)
            {
                rain.gameObject.SetActive(true);
                water.gameObject.SetActive(true);
                water.transform.DOScale(0, 0.3f).From();

                _cor = After(() => Completion(), 0.3f);
            }
        };
    }

    public override void Refresh()
    {
        base.Refresh();

        if (_cor != null)
        {
            StopCoroutine(_cor);
        }

        water.gameObject.SetActive(false);
        rain.gameObject.SetActive(false);
        cloud.Return2OriginPos();
        foreach (var dm in dragMoves)
        {
            dm.Return2OriginPos();
        }
    }
}
