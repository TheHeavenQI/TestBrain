using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Level139 : LevelBasePage
{

    public DragMove mHuocai;
    public FirePoint mPoint_1;
    public FirePoint mPoint_2;
    public FirePoint mPoint_3;
    bool isFired = false;
    Vector3 huocaiDefPos;
    protected override void Start() {
        base.Start();
        huocaiDefPos = mHuocai.transform.localPosition;
        isFired = false;
        mHuocai.onDragEnd = () =>
        {
            mHuocai.transform.localPosition = huocaiDefPos;
        };
    }
    FirePoint mFired_first;
    void OnPointBeFire(object argu)
    {
        if (!isFired)
        {
            mFired_first = argu as FirePoint;
            mFired_first.isFire = true;
            isFired = true;
            mFired_first.GetComponent<Image>().enabled = true;
        }
    }
    public override void Refresh()
    {
        base.Refresh();
        mHuocai.transform.localPosition = huocaiDefPos;
        isFired = false;
        mPoint_1.isFire = mPoint_2.isFire = mPoint_3.isFire = false;
        mFired_first.GetComponent<Image>().enabled = false;
        mFired_first = null;
    }
    bool complte = false;
    private void Update()
    {
        if (isLevelComplete ||mFired_first == null)
            return;
        float rz = mFired_first.transform.localEulerAngles.z;
        if (mFired_first == mPoint_1)
        {
            if (rz < -70 && rz > -110 || rz<290 &&rz >250)
            {
                mPoint_2.GetComponent<Image>().enabled = true;
                Completion();
            }
        }
        if (mFired_first == mPoint_2)
        {
            if (rz < -70 && rz > -110 || rz < 290 && rz > 250)
            {
                Completion();
                mPoint_3.GetComponent<Image>().enabled = true;
            }
            if (rz > 70 && rz < 110 || rz > -290 && rz < -250)
            {
                Completion();
                mPoint_1.GetComponent<Image>().enabled = true;
            }
        }
        if (mFired_first == mPoint_3)
        {
            if (rz > 70 && rz < 110 || rz > -290 && rz < -250)
            {
                Completion();
                mPoint_2.GetComponent<Image>().enabled = true;
            }
        }
    }
}
