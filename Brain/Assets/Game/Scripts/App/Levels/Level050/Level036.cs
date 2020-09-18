using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level036 : DragOutSceneLevel
{
    public Image mewImg;

    protected override void Start()
    {
        base.Start();
        mewImg.enabled = false;
    }

    protected override void OnCompletion()
    {
        base.OnCompletion();

        //X -250 250
        //Y -578 602
        Vector3 pos = mewImg.transform.position;
        mewImg.transform.SetParent(mewImg.transform.parent.parent, false);
        mewImg.transform.position = pos;

        Vector3 localPos = mewImg.transform.localPosition;
        localPos.x = Mathf.Clamp(localPos.x, -250, 250);
        localPos.y = Mathf.Clamp(localPos.y, -578, 602);
        mewImg.transform.localPosition = localPos;
        mewImg.enabled = true;
    }
}
