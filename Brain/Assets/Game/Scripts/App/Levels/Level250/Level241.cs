
using UnityEngine;
public class Level241 : LevelBasePage
{
    public DragMove men;
    public DragMove ren;
    public RectTransform qiu;
    protected override void Start() {
        base.Start();

        men.onDragEnd = () =>
        {
            if (Vector3.Distance(men.transform.localPosition, qiu.transform.localPosition) < 100)
            {
                Completion();
            }
        };
    }

    public override void Refresh()
    {
        base.Refresh();
        men.Return2OriginPos();
        ren.Return2OriginPos();
    }
}
