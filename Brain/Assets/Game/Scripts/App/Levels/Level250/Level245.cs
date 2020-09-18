
using System.Collections.Generic;
using UnityEngine;
public class Level245 : LevelBasePage
{
    public DragMove kongbai;
    public DragMove shuilongtou;
    public DragMove wuya;
    public List<Transform> posLists;

    protected override void Start() {
        base.Start();
        wuya.onDragEnd = () =>
        {
            if (CheckFinish())
            {
                Completion();
            }
            else
            {
                ShowError();
                wuya.Return2OriginPos(0.5f);
            }
        };
        shuilongtou.onDragEnd = () =>
        {
            ShowError();
            shuilongtou.Return2OriginPos(0.5f);
        };
        kongbai.onDragEnd = () =>
        {
            if (CheckFinish())
            {
                Completion();
            }
        };
    }

    private bool CheckFinish()
    {
        for(int i = 0;i< posLists.Count; i++)
        {
            var pos = posLists[i].localPosition + posLists[i].parent.localPosition;
            if (Vector3.Distance(wuya.transform.localPosition, pos) < 200)
            {
                return true;
            }
        }
        return false;
    }
    public override void Refresh()
    {
        base.Refresh();
        kongbai.Return2OriginPos();
        shuilongtou.Return2OriginPos();
        wuya.Return2OriginPos();
    }
}
