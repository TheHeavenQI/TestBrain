
using System.Collections.Generic;
using UnityEngine;

public class Level175 : LevelBasePage {
    public GameObject time1;
    public GameObject time2;
    public EventCallBack naozhong;
    public List<DragMove> list;
    public GameObject sleep;
    public GameObject awake;
    private bool _swipe;
    protected override void Start() {
        base.Start();
        Refresh();
        naozhong.onSwipe = (a) => {
            _swipe = true;
            time1.gameObject.SetActive(false);
            time2.gameObject.SetActive(true);
        };
        for (int i = 0; i < list.Count; i++) {
            var btn = list[i];
            btn.onDragEnd = () => {
                if (btn.name == "nai") {
                    if (_swipe && Vector2.Distance(btn.transform.localPosition, sleep.transform.localPosition) < 200) {
                        awake.gameObject.SetActive(true);
                        sleep.gameObject.SetActive(false);
                        After(() => {
                            Completion();
                        },0.5f);
                        return;
                    }
                }
                btn.Return2OriginPos(0.5f);
            };
            
        }
    }
    public override void Refresh() {
        base.Refresh();
        _swipe = false;
        awake.gameObject.SetActive(false);
        sleep.gameObject.SetActive(true);
        
        time2.gameObject.SetActive(false);
        time1.gameObject.SetActive(true);
        
    }
}
