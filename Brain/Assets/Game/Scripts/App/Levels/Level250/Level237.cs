using System.Collections.Generic;
using UnityEngine;

public class Level237 : LevelBasePage
{
    public List<RectTransform> rectList;
    public DragMove chidouren;
    public DragMove bi;
    public DragMove ren;

    private int _removeCount = 0;
    protected override void Start()
    {
        base.Start();
        chidouren.onDrag = () => {
            for (int i = 0; i < rectList.Count; i++)
            {
                if (rectList[i].gameObject.activeSelf
                    && RectTransformExtensions.IsRectTransformOverlap(chidouren.rectTransform, rectList[i]))
                {
                    rectList[i].gameObject.SetActive(false);
                    ++_removeCount;
                    if (_removeCount >= rectList.Count)
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
        bi.Return2OriginPos();
        ren.Return2OriginPos();
        chidouren.Return2OriginPos();
        for (int i = 0; i < rectList.Count; i++)
        {
            rectList[i].gameObject.SetActive(true);
        }
        _removeCount = 0;
    }
}
