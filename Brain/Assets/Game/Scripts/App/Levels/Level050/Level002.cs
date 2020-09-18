using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level002 : SelectNumLevel {

    public DragMoveEventTrigger dragMove;

    public Image mon;

    protected override void Awake() {
        dragMove.RefreshOriginPos();
        base.Awake();
    }

    public override void Refresh() {
        base.Refresh();
        mon.gameObject.SetActive(false);
        dragMove.Return2OriginPos();
    }

    protected override void OnCompletion()
    {
        mon.gameObject.SetActive(true);
        base.OnCompletion();

    } 

}
