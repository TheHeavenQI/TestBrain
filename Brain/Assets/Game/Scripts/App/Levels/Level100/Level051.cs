using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level051 : LevelBasePage {
    public AnimalMove animalMove;
    public Button leftButton;
    public Button rightButton;
    public Button jumpButton;
    public DragMove huluobo;
    protected override void Start() {
        base.Start();
        leftButton.onClick.AddListener(() => {
            animalMove.Moveto(new Vector2(-10,0));
        });
        rightButton.onClick.AddListener(() => {
            animalMove.Moveto(new Vector2(10,0));
        });
        jumpButton.onClick.AddListener(() => {
            animalMove.JumpUp();
        });
        animalMove.errorAction = () => {
            ShowError();
            Refresh();
        };
        animalMove.correctAction = () => {
            Completion();
        };

        huluobo.onDragEnd = () => {
            if (Vector2.Distance(huluobo.transform.localPosition,animalMove.transform.localPosition) < 150) {
                Completion();   
            }
            else {
                ShowError();
                Refresh();
                
            }
        };
    }
    public override void Refresh() {
        base.Refresh();
        huluobo.Return2OriginPos();
        animalMove.transform.position = animalMove.orgPosition;
    }
}
