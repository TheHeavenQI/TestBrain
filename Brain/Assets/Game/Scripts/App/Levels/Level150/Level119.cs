using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class Level119 : LevelBasePage {
    public Shake shake;
    private bool _shaked;
    public DragMove meinianda;
    public DragMove kele;
    public Transform ren;

    public Sprite meiniandaBeforShake;
    public Sprite meiniandaAfterShake;

    public Sprite keleBeforShake;
    public Sprite keleAfterShake;

    protected override void Start() {
        base.Start();
        shake.shakeAction = () => {
            meinianda.gameObject.GetComponent<Image>().sprite = meiniandaAfterShake;
            kele.gameObject.GetComponent<Image>().sprite = keleAfterShake;
            _shaked = true;
        };
        meinianda.onDragEnd = () => {
            if (_shaked && Vector3.Distance(meinianda.transform.localPosition,ren.transform.localPosition) < 200) {
                Completion();
                return;
            }
            meinianda.Return2OriginPos(0.5f);
        };
        kele.onDragEnd = () => { kele.Return2OriginPos(0.5f); };
    }

    public override void Refresh() {
        base.Refresh();
        _shaked = false;
        meinianda.gameObject.GetComponent<Image>().sprite = meiniandaBeforShake;
        kele.gameObject.GetComponent<Image>().sprite = keleBeforShake;
    }
}
