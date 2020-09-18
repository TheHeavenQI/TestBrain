using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level073 : LevelBasePage {
    public Transform babyAwake;
    public Transform babySleep;
    public EventCallBack doorClose;
    public Transform doorOpen;
    public Transform mama;
    public List<DragMove> dragMoves;
    protected override void Start() {
        base.Start();
        for (int i = 0; i < dragMoves.Count; i++) {
            var btn = dragMoves[i];
            btn.onDragEnd = () => {
                ShowError();
                btn.Return2OriginPos(0.5f);
            };
        }
        MamaComing(false);
        doorClose.onSwipe = (dir) => {
            if (dir == SwipeDirection.Left) {
                MamaComing(true);
                After(() => {
                    Completion();
                },0.5f);
            }
        };
    }

    private void MamaComing(bool mamaComing) {
        babyAwake.gameObject.SetActive(mamaComing);
        doorOpen.gameObject.SetActive(mamaComing);
        mama.gameObject.SetActive(mamaComing);
        babySleep.gameObject.SetActive(!mamaComing);
        doorClose.gameObject.SetActive(!mamaComing);
    }

    public override void Refresh() {
        base.Refresh();
        MamaComing(false);
    }
    
    
    
}
