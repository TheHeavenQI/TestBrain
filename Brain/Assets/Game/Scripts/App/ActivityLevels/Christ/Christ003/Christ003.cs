using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Christ003 : ChristBaseLevel
{
    public Christ003Bread genger;

    private bool _isDead = false;

    public List<GameObject> trap;

    protected override void Awake()
    {
        base.Awake();

        genger.collisionCallBack = (bool isSuccess) => {
            if (isSuccess)
            {
                Completion();
            }
            else
            {
                if (_isDead)
                {
                    return;
                }
                _isDead = true;
                ShowError(genger.transform.localPosition);
                Refresh();
            }
        };
    }

    protected override void OnTipsDialogCloseWithTipsUsed()
    {
        base.OnTipsDialogCloseWithTipsUsed();

        foreach (GameObject obj in trap)
        {
            obj.SetActive(false);
        }
    }

    public override void Refresh()
    {
        base.Refresh();
        genger.collider.enabled = false;
        genger.enableDragMove = false;
        genger.Return2OriginPos(0.5f).OnComplete(() => {
            genger.collider.enabled = true;
            genger.enableDragMove = true;
            _isDead = false;
        });

        //foreach (GameObject obj in trap)
        //{
        //    obj.SetActive(true);
        //}
    }
}
