using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn_Script : MonoBehaviour
{

    DateTime dtime;
    float tspan;
    public int spRateMin, spRateMax;
    public bool spawnFlag;
    public int enemyCount;

    public GameObject Enemy;
    Transform Portal;

    private void Start()
    {
        Portal = gameObject.transform;
        dtime = DateTime.Now;   
    }

    void FixedUpdate()
    {
        if (spawnFlag)
        {
            tspan = (float)DateTime.Now.Subtract(dtime).TotalMilliseconds;

            if (enemyCount > 0 && tspan >= UnityEngine.Random.Range(spRateMin, spRateMax))
            {
                EnemySpawn(Enemy);
            }
            if (enemyCount == 0) spawnFlag = false;
        }

    }

    void EnemySpawn(GameObject Enemy)
    {
        float PortalX = Portal.position.x;
        Instantiate(
            Enemy,
            new Vector3(UnityEngine.Random.Range(PortalX - 40, PortalX + 40), Portal.position.y, Portal.position.z),
            Portal.rotation
            );

        dtime = DateTime.Now;
        enemyCount--;
    }
}
