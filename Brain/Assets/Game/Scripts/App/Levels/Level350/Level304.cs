using UnityEngine;
using UnityEngine.UI;

public class Level304 : LevelBasePage
{
    public DragMoveEventTrigger[] apples;
    public RectTransform[] dishs;

    private float _outX = 750 / 2;
    private float _outY = 1334 / 2;

    protected override void Start()
    {
        base.Start();

        for (int i = 0; i < apples.Length; ++i)
        {
            int j = i;
            apples[j].onEndDrag = (d) => OnAppleEndDrag();
        }
    }

    private void OnAppleEndDrag()
    {
        int outCount = 0;
        int inDishCount = 0;

        foreach (var apple in apples)
        {
            bool isInDisk = false;
            foreach (var dish in dishs)
            {
                if (apple.rectTransform.IsRectTransformOverlap(dish))
                {
                    isInDisk = true;
                    ++inDishCount;
                    break;
                }
            }
            if (isInDisk)
            {
                continue;
            }
            Vector2 p = apple.transform.localPosition;
            if (p.x < -_outX || p.x > _outX
                 || p.y > _outY || p.y < -_outY)
            {
                ++outCount;
            }
        }

        if (outCount == 1 && inDishCount == dishs.Length)
        {
            Completion();
        }
    }

    public override void Refresh()
    {
        base.Refresh();

        foreach (var dm in apples)
        {
            dm.Return2OriginPos();
        }
    }
}
