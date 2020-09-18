using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using BaseFramework;
using System.Collections.Generic;

public class Level316 : LevelBasePage
{
    public DragMoveEventTrigger ground;
    public DragMoveEventTrigger[] stones;
    public Level296Hole[] holes;
    private HashSet<Level296Hole> _coveredHoles = new HashSet<Level296Hole>();

    private float _lastShowTime;
    private float _showGap = 1;

    protected override void Start()
    {
        base.Start();

        for (int i = 0; i < stones.Length; ++i)
        {
            stones[i].onEndDrag = (d) => CheckHoleCovered();
        }
    }


    public override void Refresh()
    {
        base.Refresh();

        ground.Return2OriginPos();
        foreach (var stone in stones)
        {
            stone.Return2OriginPos();
        }
        foreach (var hole in holes)
        {
            hole.Clear();
        }
        _coveredHoles.Clear();

        _lastShowTime = Time.time;
    }

    private void Update()
    {
        if (isLevelComplete)
        {
            return;
        }
        if (Time.time - _lastShowTime >= _showGap)
        {
            _lastShowTime = Time.time;
            ShowRabbit();
        }
    }

    private void ShowRabbit()
    {
        if (_coveredHoles.Count == stones.Length)
        {
            var hole = holes.GetRandomItem(it => !_coveredHoles.Contains(it));
            if (hole == null)
            {
                return;
            }
            hole.ShowGameObject(Level296Hole.GOType.rabbit, 0.1f, (rabbit) => {
                rabbit.GetComponentInChildren<Button>().onClick.AddListener(() => {
                    Completion();
                });
                After(() => {
                    if (!isLevelComplete)
                    {
                        hole.HideGameObject(rabbit, 0.1f);
                    }
                }, _showGap - 0.1f);
            });
        }
        else
        {
            var hole = holes.GetRandomItem(it => !_coveredHoles.Contains(it));
            if (hole == null)
            {
                return;
            }
            hole.ShowGameObject(Level296Hole.GOType.rabbit, 0.1f, (rabbit) => {
                After(() => {
                    hole.HideGameObject(rabbit, 0.1f);
                }, 0.2f);
            });
        }
    }

    private void CheckHoleCovered()
    {
        _coveredHoles.Clear();
        foreach (var hole in holes)
        {
            foreach (var stone in stones)
            {
                if (IsHoleCovered(hole, stone))
                {
                    _coveredHoles.Add(hole);
                    break;
                }
            }
        }
    }

    private bool IsHoleCovered(Level296Hole hole, DragMoveEventTrigger stone)
    {
        Vector3 offset = stone.transform.localPosition - hole.transform.localPosition;
        if (offset.x >= -35 && offset.x <= 35 && offset.y >= -23 && offset.y <= 70)
        {
            return true;
        }
        return false;
    }
}
