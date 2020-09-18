using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Level081 : LevelBasePage {

    public LongPressEventTrigger longPressEventTrigger;
    public GameObject common;
    public GameObject wakeUped;
    public Image red;
    public DragMove dragMove;
    Tweener fadeTween;
    protected override void Start() {
        base.Start();

        longPressEventTrigger.onLongPress += () => {
            fadeTween = red.DOFade(1,1);
            fadeTween.onComplete = () =>
            {
                common.SetActive(false);
                wakeUped.SetActive(true);
                red.color = new Color(1,1,1,0);
                After(()=> { Completion(); },0.5f);
            };
            //Completion();
        };
        dragMove.onDragEnd = () => {
            dragMove.Return2OriginPos(0.5f);
            ShowError();
        };
    }
    public override void Refresh()
    {
        base.Refresh();
        if (fadeTween != null)
            fadeTween.Kill();
        red.color = new Color(1,1,1,0);
        wakeUped.SetActive(false);
        common.SetActive(true);
    }
}
