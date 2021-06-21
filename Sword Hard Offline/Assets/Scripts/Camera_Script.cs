using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerData_Script;

public class Camera_Script : MonoBehaviour
{
    RaycastHit[] hit = new RaycastHit[2];
    Ray[] rays = new Ray[2];
    GameObject[] hit_Buffer = new GameObject[2];

    Transform SwordPointTop;
    Transform SwordPointMiddle;

    void Start()
    {
        SwordPointTop = Sword.transform.Find("PointTop");
        SwordPointMiddle = Sword.transform.Find("PointMiddle");
    }

    void Update()
    {
        rays[0] = new Ray(this.transform.position, SwordPointTop.transform.position - this.transform.position);
        rays[1] = new Ray(this.transform.position, SwordPointMiddle.transform.position - this.transform.position);

        for (byte indx = 0; indx < hit.Length; indx++)
        {
            Debug.Log("Цикл");
            if (Physics.Raycast(rays[indx], out hit[indx], 300))
            {
                Debug.Log("Рейкаст");

                GameObject hit_Object = hit[indx].transform.gameObject;

                if (hit[indx].transform.gameObject.CompareTag("Enemy"))
                {
                    Debug.Log("Проверка тега");

                    if (hit_Object.GetComponent<Enemy_Script>().hitCheck == false && hit_Object != hit_Buffer[indx])
                    {
                        Debug.Log($"Ray №{indx + 1}");
                        Debug.Log(hit_Object);

                        hit_Object.GetComponent<Enemy_Script>().TakeDamage();

                        hit_Object.GetComponent<Enemy_Script>().hitCheck = true;
                        if (hit_Buffer[indx] != null && hit_Buffer[indx].CompareTag("Enemy"))
                        {
                            hit_Buffer[indx].GetComponent<Enemy_Script>().hitCheck = false;
                        }

                        hit_Buffer[indx] = hit_Object;
                    }
                }
                else
                {
                    if (hit_Buffer[indx] != null && hit_Buffer[indx].CompareTag("Enemy"))
                    {
                        hit_Buffer[indx].GetComponent<Enemy_Script>().hitCheck = false;
                    }
                    hit_Buffer[indx] = hit_Object;
                }
            }
        }
    }
}
