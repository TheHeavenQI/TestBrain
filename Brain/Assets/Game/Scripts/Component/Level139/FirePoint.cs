using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FirePoint : MonoBehaviour
{
    public GameObject CallBackObj;



    public bool isFire = false;
    bool gyinfo;
    Gyroscope go;
    void Start()
    {
        Input.gyro.enabled = true;
        go = Input.gyro;
        go.enabled = true;
    }
    void Update()
    {
        Vector3 a = go.attitude.eulerAngles;
        a = new Vector3(-a.x, -a.y, a.z); //直接使用读取的欧拉角发现不对，于是自己调整一下符号
        this.transform.eulerAngles = a;
        this.transform.Rotate(Vector3.right * 90, Space.World);
        Vector3 localEular = this.transform.localEulerAngles;
        transform.localEulerAngles = new Vector3(0, 0, -localEular.z);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (CallBackObj != null)
            CallBackObj.SendMessage("OnPointBeFire", this);
    }
}

