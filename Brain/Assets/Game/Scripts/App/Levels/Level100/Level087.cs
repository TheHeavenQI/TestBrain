
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level087 : LevelBasePage {
    
    public AnimalMove ren;
    public DragMove dragMove;
    public Button leftbtn;
    public Button rightbtn;
    protected override void Start() {
        base.Start();
        leftbtn.onClick.AddListener(() => {
            ren.Moveto(new Vector2(-20,0));
        });
        rightbtn.onClick.AddListener(() => {
            ren.Moveto(new Vector2(20,0));
        });
        ren.errorAction = () => {
            ShowError();
            Refresh();
        };
        ren.jumpDownOffset = new Vector2(0,-200);
        ren.correctAction = () => {
            Completion();
        };
        dragMove.onDrag = () => { ren.CheckDown(); };
    }

    public override void Refresh() {
        base.Refresh();
        dragMove.Return2OriginPos();
        ren.Refresh();
    }
}
