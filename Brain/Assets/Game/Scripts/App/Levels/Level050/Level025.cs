
using UnityEngine;

public class Level025 : LevelBasePage {
    public Transform peopleTransform;
    public DragMove roadDragMove;
    private Vector3 _roadPos;
    protected override void Start() {
        base.Start();
        _roadPos = roadDragMove.transform.localPosition;
        roadDragMove.onDragEnd = () => {
            if (roadDragMove.transform.localPosition.x < peopleTransform.localPosition.x) {
                Completion();
            }
        };
    }
    
    public override void Refresh() {
        base.Refresh();
        roadDragMove.transform.localPosition = _roadPos;
    }
}
