using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static PlayerData_Script;

public class Enemy_Script : MonoBehaviour
{
    public int level = 1;
    public int health = 50;
    int armor = 5;
    int speed = 10;

    public byte hitCheck = 0;

    public int IndLifespan;
    public GameObject CritInd, ArmorPenInd;
    Text health_bar;
    Transform CanvasTrans;
    Transform IndicatorSpawnPos;

    void Start()
    {
        CanvasTrans = gameObject.transform.Find("Canvas");
        health_bar = gameObject.transform.Find("Canvas").Find("Health").GetComponent<Text>();
        IndicatorSpawnPos = gameObject.transform.Find("Canvas").Find("IndicatorSpawn");
    }

    void FixedUpdate()
    {
        gameObject.transform.Translate(new Vector3(0,0,1) * speed * Time.fixedDeltaTime);
        health_bar.text = health.ToString();
    }

    public void TakeDamage()
    {
        int damage = SwordStats.swDamage;
        if (UnityEngine.Random.value < SwordStats.critChance)
        {
            damage *= 2;
            IndSpawn(CritInd);
            Debug.Log("CRIT");
        }
        if (UnityEngine.Random.value > SwordStats.armorPenChance)
        {
            damage -= armor;
        }
        else
        {
            IndSpawn(ArmorPenInd);
            Debug.Log("ARMOR PENETRATION");
        }

        health -= damage;

        if (health <= 0)
        {
            Death();
        }
    }

    void IndSpawn(GameObject Indicator)
    {
        IndicatorBehaviour_Script stats = Instantiate(Indicator, CanvasTrans).GetComponent<IndicatorBehaviour_Script>();
        stats.lifespan = IndLifespan;
    }

    void GiveGold()
    {
        
    }

    private void Death()
    {
        Destroy(gameObject);
    }
}
