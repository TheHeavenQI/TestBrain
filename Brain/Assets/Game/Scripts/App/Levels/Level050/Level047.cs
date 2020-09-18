using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level047 : LevelBasePage {
    public List<DragMove> list;
    public GameObject babySleep;
    public GameObject babyWake;
    private Shake _shake;
    protected override void Start() {
        base.Start();
        _shake = GetComponent<Shake>();
        _shake.shakeAction = () => {
            babySleep.SetActive(true);
            babyWake.SetActive(false);
            After(() => {
                Completion();
            },1f);
        };
        for (int i = 0; i < list.Count; i++) {
            var obj = list[i];
            obj.onDragEnd = () => {
                obj.Return2OriginPos(0.5f);
                ShowError();
            };
        }
        babySleep.SetActive(false);
        babyWake.SetActive(true);
    }
    
    public override void Refresh() {
        base.Refresh();
        babySleep.SetActive(false);
        babyWake.SetActive(true);
    }
}
