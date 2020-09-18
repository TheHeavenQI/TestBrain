using UnityEngine;
using UnityEngine.UI;

public class Level314 : LevelBasePage
{
    public DragMoveEventTrigger car;
    public RectTransform road;
    public RectTransform pointLT;
    public RectTransform pointLB;
    public RectTransform pointRT;
    public RectTransform pointRB;

    protected override void Start()
    {
        base.Start();

        car.onEndDrag = (d) => {
            if (!IsVector2InRect(car.rectTransform.localPosition))
            {
                Completion();
            }
        };
    }

    public override void Refresh()
    {
        base.Refresh();
        car.Return2OriginPos();
    }

    private bool IsVector2InRect(Vector2 point)
    {
        return IsVector2InRect(point.x, point.y);
    }


    private bool IsVector2InRect(float x, float y)
    {
        //ref: https://blog.csdn.net/laukaka/article/details/45168439

        Vector2 A = pointLB.localPosition;
        Vector2 B = pointLT.localPosition;
        Vector2 C = pointRT.localPosition;
        Vector2 D = pointRB.localPosition;

        float a = (B.x - A.x) * (y - A.y) - (B.y - A.y) * (x - A.x);
        float b = (C.x - B.x) * (y - B.y) - (C.y - B.y) * (x - B.x);
        float c = (D.x - C.x) * (y - C.y) - (D.y - C.y) * (x - C.x);
        float d = (A.x - D.x) * (y - D.y) - (A.y - D.y) * (x - D.x);

        if ((a > 0 && b > 0 && c > 0 && d > 0) || (a < 0 && b < 0 && c < 0 && d < 0))
        {
            return true;
        }

        return false;
    }
}
