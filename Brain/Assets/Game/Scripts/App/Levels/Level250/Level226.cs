using UnityEngine;

public class Level226 : LevelBasePage
{
    private LimitDragMoveEventTrigger[] dragMoves;

    private bool _isFaceDown;

    protected override void Start()
    {
        base.Start();
        dragMoves = this.GetComponentsInChildren<LimitDragMoveEventTrigger>();
        dragMoves[dragMoves.Length - 1].rectTransform.SetAsLastSibling();
    }

    public override void Refresh()
    {
        base.Refresh();
        foreach (var dm in dragMoves)
        {
            dm.Return2OriginPos();
        }
        _isFaceDown = false;
    }

    private void Update()
    {
        if (isLevelComplete)
        {
            return;
        }
        if (Input.deviceOrientation == DeviceOrientation.FaceDown)
        {
            _isFaceDown = true;
        }
        else if (_isFaceDown)
        {
            Completion();
        }
    }
}
