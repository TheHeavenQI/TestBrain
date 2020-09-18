
using System.Collections.Generic;
using UnityEngine;

public class Level257 : LevelBasePage
{
    private float margin = 10;
    public List<DragMove> list;
    private List<Vector2> _margins = new List<Vector2>();
    
    protected override void Start() {
        base.Start();
        _margins.Add(list[0].transform.localPosition - list[1].transform.localPosition);
        _margins.Add(list[1].transform.localPosition - list[2].transform.localPosition);
        _margins.Add(list[2].transform.localPosition - list[3].transform.localPosition);
        _margins.Add(list[3].transform.localPosition - list[0].transform.localPosition);
        Refresh();
        for (int i = 0; i < list.Count; i++)
        {
            list[i].onDragEnd = () =>
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
        if(CheckPos(list[0].transform.localPosition - list[1].transform.localPosition, _margins[0]) == false)
        {
            return false;
        }

        if (CheckPos(list[1].transform.localPosition - list[2].transform.localPosition, _margins[1]) == false)
        {
            return false;
        }
        if (CheckPos(list[2].transform.localPosition - list[3].transform.localPosition, _margins[2]) == false)
        {
            return false;
        }
        if (CheckPos(list[3].transform.localPosition - list[0].transform.localPosition, _margins[3]) == false)
        {
            return false;
        }
        return true;
    }

    private bool CheckPos(Vector2 vector1,Vector2 vector2)
    {
        if(Mathf.Abs(vector1.x-vector2.x) > margin)
        {
            return false;
        }
        if (Mathf.Abs(vector1.y - vector2.y) > margin)
        {
            return false;
        }
        return true;
    }
    public override void Refresh()
    {
        base.Refresh();
        for(int i = 0;i < list.Count; i++)
        {
            list[i].transform.localPosition = new Vector2(Random.Range(-300, 300), Random.Range(-300, 300));
        }
    }

}
