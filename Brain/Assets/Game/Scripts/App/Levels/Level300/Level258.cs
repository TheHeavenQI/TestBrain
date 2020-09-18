
public class Level258 : LevelBasePage
{
    public DragMoveEventTrigger[] dragMoves;
    public Level258DragMove goldBall;

    protected override void Start()
    {
        base.Start();
        goldBall.onPointerClick += (d) => {
            if (!goldBall.RandomMove())
            {
                Completion();
            }
        };
    }


    public override void Refresh()
    {
        base.Refresh();
        foreach (var dm in dragMoves)
        {
            dm.Return2OriginPos();
        }
    }
}
