using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonCar : EventCallBack
{
    public bool isAutoMove = true;
    public float speed = 0.2f;

    // Update is called once per frame
    void Update()
    {
        base.Update();
        if (isAutoMove)
            transform.localPosition += new Vector3(speed,0,0);
    }
}
