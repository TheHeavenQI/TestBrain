using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotate : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 3;
    void Update()
    {
        transform.localEulerAngles += new Vector3(0,0, speed); 
    }
}
