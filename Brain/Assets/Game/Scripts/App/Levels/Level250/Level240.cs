using UnityEngine;
using DG.Tweening;

public class Level240 : LevelBasePage
{
    public CustomEventTrigger bagBody;
    public CustomEventTrigger bagUp;
    public DragMoveEventTrigger gun;

    protected override void Start()
    {
        base.Start();
        bagUp.onDrag = (d) => {
            if (bagBody.isPress)
            {
                bagUp.transform.DOLocalMoveY(300, 0.2f);
                Completion();
            }
        };
    }

    public override void Refresh()
    {
        base.Refresh();
        gun.Return2OriginPos();
    }
}
