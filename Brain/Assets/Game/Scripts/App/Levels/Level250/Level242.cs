using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Level242 : LevelBasePage
{
    public DragMoveEventTrigger drat;
    public Image[] balloons;
    public Sprite balloonNormal;
    public Sprite balloonBreak;

    private readonly HashSet<int> _breakIndex = new HashSet<int>();
    private readonly float breakDis = 85;

    protected override void Start()
    {
        base.Start();
        drat.onDrag = (d) => {
            for (int i = 0; i < balloons.Length; i++)
            {
                if (_breakIndex.Contains(i))
                {
                    continue;
                }

                Image img = balloons[i];
                if (Vector3.Distance(img.transform.localPosition, drat.transform.localPosition) <= breakDis)
                {
                    img.sprite = balloonBreak;
                    img.SetNativeSize();
                    img.transform.DOScale(0, 0.3f).SetDelay(0.1f);
                    _breakIndex.Add(i);
                    if (_breakIndex.Count >= balloons.Length)
                    {
                        Completion();
                    }
                }
            }
        };
    }

    public override void Refresh()
    {
        base.Refresh();

        drat.Return2OriginPos();

        for (int i = 0; i < balloons.Length; i++)
        {
            Image img = balloons[i];
            img.sprite = balloonNormal;
            img.SetNativeSize();
            img.transform.DOKill();
        }

        _breakIndex.Clear();
    }
}
