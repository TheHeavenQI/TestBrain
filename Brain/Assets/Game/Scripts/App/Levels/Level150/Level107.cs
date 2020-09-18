using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level107 : LevelBasePage
{
    protected override void Start()
    {
        base.Start();
        StartCoroutine("mUpdate");
    }
    IEnumerator mUpdate()
    {
        while (Input.acceleration.z <= 0.8f)
        {
            yield return null;
        }
        Completion();
    }
}

