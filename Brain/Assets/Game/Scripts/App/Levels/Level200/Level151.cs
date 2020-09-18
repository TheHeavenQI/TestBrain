
using System;
using UnityEngine;

public class Level151 : LevelBasePage {
    public GameObject blubOff;
    public GameObject blubOpen;
    public EventCallBack blub;
    private Vector3 org;
    protected override void Start() {
        base.Start();
        org = blub.transform.localPosition;
        blubOff.transform.localPosition = blubOpen.transform.localPosition;
        blubOpen.SetActive(true);
        blubOff.SetActive(false);
        blub.onSwipeRepeat = (dir) => {
            if (dir == SwipeDirection.Left || dir == SwipeDirection.Right) {
                blub.transform.localPosition += new Vector3(0, -0.5f, 0);
            }
            if (Math.Abs(blub.transform.localPosition.y - org.y) > 17) {
                blubOpen.SetActive(false);
                blubOff.SetActive(true);
                After(() => {
                    Completion();
                },0.5f);
            }
        };
    }

    public override void Refresh() {
        base.Refresh();
        blubOpen.SetActive(true);
        blubOff.SetActive(false);
        blub.transform.localPosition = org;
    }
}
