using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static PlayerData_Script;

public class IndicatorBehaviour_Script : MonoBehaviour
{
    DateTime dtime;
    Vector3 vector;

    float speed = 0.6f;
    public int lifespan;

    void Start()
    {
        vector = new Vector3(UnityEngine.Random.Range(-1f, 1.1f), UnityEngine.Random.value, 0);
        dtime = DateTime.Now;
    }

    void FixedUpdate()
    {
        gameObject.transform.Translate(vector * speed);
        if (DateTime.Now.Subtract(dtime).TotalMilliseconds > lifespan)
        {
            Destroy(gameObject);
        }
    }
}
