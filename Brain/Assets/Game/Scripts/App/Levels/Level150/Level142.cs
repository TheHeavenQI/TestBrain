using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using DG.Tweening;

public class Level142 : SelectNumLevel
{
    public List<Image> images;
    private List<Vector3> positons = new List<Vector3>();

    public Shake shake;

    private int shakeCount = 0;
    public DOTweenAnimation shakeAnim;
    protected override void Awake()
    {
        
        for (int i = 0; i < images.Count; i++)
        {
            positons.Add(images[i].transform.localPosition);
        }

        shake.shakeAction = shakeCallBack;

        base.Awake();
    }


    public override void Refresh()
    {
        base.Refresh();
        shakeCount = 0;

        if (positons.Count == 0)
        {
            return;
        }

        for (int i = 0; i < images.Count; i++)
        {
            images[i].transform.localPosition = positons[i];
            if (i > 2)
            {
                images[i].transform.SetAsFirstSibling();
            }else
            {
                images[i].transform.SetAsLastSibling();
            }
            
        }
    }

    private void shakeCallBack() {
        shakeAnim.DORestart();
        shakeCount += 1;
        if (shakeCount <= 3)
        {
            // move
            images[2 + shakeCount].transform.DOLocalMove(new Vector3(images[0].transform.localPosition.x + 80 * shakeCount, images[0].transform.localPosition.y, images[2 + shakeCount].transform.localPosition.z), 0.5f).SetEase(Ease.InQuart);
            images[2 + shakeCount].transform.SetAsLastSibling();
        }
    }

}
