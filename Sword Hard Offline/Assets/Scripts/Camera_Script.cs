using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerData_Script;

public class Camera_Script : MonoBehaviour
{
    static int rayN = 4;

    Ray[] rays = new Ray[rayN];
    RaycastHit[] hit = new RaycastHit[rayN];
    GameObject[] hit_Buffer = new GameObject[rayN];
    Transform[] SwordPoint = new Transform[rayN];

    Transform SwordPointTop;
    Transform SwordPointMiddle;

    void Start()
    {
        for (byte indx = 0; indx < rayN; indx++)
        {
            SwordPoint[indx] = Sword.transform.Find($"Point_{indx}");
        }
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
                GameObject hit_Object = hit[indx].transform.gameObject;

                if (
                    hit[indx].transform.gameObject.CompareTag("Enemy") && 
                    hit_Object != hit_Buffer[indx]
                    )
                {
                    if (hit_Object.GetComponent<Enemy_Script>().hitCheck == 0)
                    {
                        hit_Object.GetComponent<Enemy_Script>().TakeDamage();
                    }
                    hit_Object.GetComponent<Enemy_Script>().hitCheck++;
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
