using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Level228 : LevelBasePage
{
    public Image fire;
    public Image bucket;
    public Image bucketFall;

    private Shake _shake;

    protected override void Start()
    {
        base.Start();

        _shake = this.GetComponent<Shake>();
        _shake.shakeAction = () => {
            Completion();
            bucket.gameObject.SetActive(false);
            bucketFall.gameObject.SetActive(true);
            fire.rectTransform.DOScale(0, 0.3f);
        };

        bucketFall.gameObject.SetActive(false);
    }
}
