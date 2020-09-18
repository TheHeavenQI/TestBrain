using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level176 : LevelBasePage {
    public Image role;
    public Image yanjing;
    public LongPressEventTrigger eyeET;
    public DragMoveEventTrigger dollarDM;
    public Sprite normal;
    public Sprite suc;
    protected override void Start() {
        base.Start();
        eyeET.onLongPress += () =>
        {
            role.sprite = suc;
            yanjing.enabled = false;
            After(()=> 
            {
                CompletionWithMousePosition();
            },1);
        };
    }

    public override void Refresh() {
        base.Refresh();
        role.sprite = normal;
        yanjing.enabled = true;
        dollarDM.Return2OriginPos();
    }
}
