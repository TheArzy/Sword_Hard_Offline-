using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheSword_Script : MonoBehaviour
{
    public int swDamage = 10;

    float critChance = 0.2f;

    float armorPenChance = 0.15f;

    Vector3 CamStartPos;
    private float MoveSpeed = 0.012f;

    void Start()
    {
        CamStartPos = GameObject.Find("Main Camera").transform.position + new Vector3(-6.5f, 0, -11.5f);
    }

    void Update()
    {
        gameObject.transform.position = new Vector3(
            (Input.mousePosition.x * MoveSpeed) + CamStartPos.x, 
            CamStartPos.y - 20, 
            (Input.mousePosition.y * MoveSpeed) + CamStartPos.z
            );
    }
}
