using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
public class RotateNum : DragMove
{

    public bool enableRotate = true;
    new void Start()
    {
        enabelDrag = false;
        onDragBegin = () =>
        {
            if (enableRotate)
            {
                float z = transform.localEulerAngles.z;
                float z_1 = z - 180; float z_2 = z + 180;
                if (Mathf.Abs(z_1) < 30 || Mathf.Abs(z_2) < 30)
                {
                    transform.DOLocalRotate(Vector3.zero, 0.2f);
                }
                else
                    transform.DOLocalRotate(new Vector3(0, 0, 180), 0.2f);
            }
        };
    }
}
