using System.Collections.Generic;
using UnityEngine;
public class Level179 : LevelBasePage
{
    public List<DragMove> dgs;
     DragMove d_2;
     DragMove d_3;
    float wDistance = 50;
    float hDistance = 20;
    protected override void Start() {
        base.Start();
        d_2 = dgs[2];
        d_2.onDragEnd = onDragEnd;
        d_3 = dgs[3];
        d_3.onDragEnd = onDragEnd;
    }
    public override void Refresh()
    {
        base.Refresh();
        foreach (var item in dgs)
            item.Return2OriginPos();
    }
    void onDragEnd()
    {
        Vector2 pos1 = d_2.transform.localPosition;
        Vector2 pos2 = d_3.transform.localPosition;
        if(Mathf.Abs(pos1.x - pos2.x) < wDistance && Mathf.Abs(pos1.y-pos2.y) < hDistance)
            Completion();
    }
}
