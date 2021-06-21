using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerData_Script;

public class Enemy_Script : MonoBehaviour
{
    public int level = 1;

    public int health = 100;

    public int armor = 10;

    public bool hitCheck = false;

    int speed = 5;

    void Start()
    {
        
    }

    void FixedUpdate()
    {
        gameObject.transform.Translate(new Vector3(0,0,-1) * speed * Time.fixedDeltaTime);
    }

    public void TakeDamage()
    {
        health -= Sword.GetComponent<TheSword_Script>().swDamage;

        if (health <= 0)
        {
            Death();
        }
    }

    private void Death()
    {
        Destroy(gameObject);
    }
}
