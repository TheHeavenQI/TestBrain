
using System.Collections.Generic;
using UnityEngine;

public class Level276 : LevelBasePage
{
    public List<DragMove> shupians;
    public Transform left;
    public Transform right;
    protected override void Start() {
        base.Start();
        for(int i = 0; i < shupians.Count; i++)
        {
            shupians[i].onDragEnd = () =>
            {
                if (CheckFinished())
                {
                    Completion();
                }
            };
        }
    }

    private bool CheckFinished()
    {
        int leftCount = 0;
        int rightCount = 0;
        for (int i = 0; i < shupians.Count; i++)
        {
            var pos = shupians[i].transform.localPosition;
            if(Vector3.Distance(pos, left.localPosition) < 150)
            {
                leftCount++;
            }
            if (Vector3.Distance(pos, right.localPosition) < 150)
            {
                rightCount++;
            }
        }
        if(leftCount == 3 && rightCount == 3)
        {
            return true;
        }
        return false;
    }
    public override void Refresh()
    {
        base.Refresh();
        for (int i = 0; i < shupians.Count; i++)
        {
            shupians[i].Return2OriginPos();
        }
    }
}
