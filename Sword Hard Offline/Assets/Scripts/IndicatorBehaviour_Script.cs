using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatorBehaviour_Script : MonoBehaviour
{
    DateTime dtime;
    public int lifespan;
    Vector3 vector;

    void Start()
    {
        vector = new Vector3(UnityEngine.Random.Range(-1f, 1.1f), UnityEngine.Random.value, 0);
        dtime = DateTime.Now;
    }

    void FixedUpdate()
    {
        gameObject.transform.Translate(vector * 0.6f);
        if (DateTime.Now.Subtract(dtime).TotalMilliseconds > lifespan)
        {
            Destroy(gameObject);
        }
    }
}
