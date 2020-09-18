using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateLight : MonoBehaviour
{
    private void FixedUpdate()
    {
        transform.localEulerAngles += new Vector3(0,0,2);
    }
}
