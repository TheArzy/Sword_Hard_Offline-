using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerData_Script;

public class Camera_Script : MonoBehaviour
{
    static int rayN = 4;
    Enemy_Script Enemy;
    GameObject hit_Object;

    Ray[] rays = new Ray[rayN];
    RaycastHit[] hit = new RaycastHit[rayN];
    GameObject[] hit_Buffer = new GameObject[rayN];
    Transform[] SwordPoint = new Transform[rayN];

    void Start()
    {
        for (byte indx = 0; indx < rayN; indx++)
        {
            SwordPoint[indx] = Sword.transform.Find($"Point_{indx}");
        }
        Enemy = GetComponent<Enemy_Script>();
        Debug.Log("Инициализация хитсканера");
    }

    void Update()
    {
        for (byte indx = 0; indx < rayN; indx++)
        {
            rays[indx] = new Ray(gameObject.transform.position, SwordPoint[indx].position - gameObject.transform.position);
        }

        for (byte indx = 0; indx < rays.Length; indx++)
        {
            if (Physics.Raycast(rays[indx], out hit[indx], 300))
            {
                hit_Object = hit[indx].transform.gameObject;
                Enemy = hit_Object.GetComponent<Enemy_Script>();

                if (
                    hit[indx].transform.gameObject.CompareTag("Enemy") && 
                    hit_Object != hit_Buffer[indx]
                    )
                {
                    if (Enemy.hitCheck == 0)
                    {
                        Enemy.TakeDamage();
                    }
                    Enemy.hitCheck++;
                }

                if (
                    hit_Object != hit_Buffer[indx] && 
                    hit_Buffer[indx] != null && 
                    hit_Buffer[indx].CompareTag("Enemy")
                   )
                {
                    hit_Buffer[indx].GetComponent<Enemy_Script>().hitCheck--;
                }

                hit_Buffer[indx] = hit_Object;
            }
        }
    }
}
