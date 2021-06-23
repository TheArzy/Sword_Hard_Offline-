using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static PlayerData_Script;

public class Enemy_Script : MonoBehaviour
{
    public float level = 1;
    public float health = 50;
    public float armor = 5;
    int gold = 25;
    int speed = 10;

    public byte hitCheck = 0;

    public int IndLifespan;
    public GameObject CritInd, ArmorPenInd;
    Text health_bar;
    Transform CanvasTrans;

    void Start()
    {
        CanvasTrans = gameObject.transform.Find("Canvas");
        health_bar = gameObject.transform.Find("Canvas").Find("Health").GetComponent<Text>();

        if (level > 1)
        {
            health += (int)(health * (2 * level / 10f));
            armor += (int)(armor * (level / 10f));
            gold += (int)(gold * (level / 10f));
        }
        Debug.Log($"Деление уровня {level / 10f}");

        speed = Random.Range(speed, speed+7);

    }

    void FixedUpdate()
    {
        gameObject.transform.Translate(speed * Time.fixedDeltaTime * Vector3.forward);
        health_bar.text = health.ToString();
    }

    public void TakeDamage()
    {
        float damage = SwordStats.swDamage;
        if (UnityEngine.Random.value < SwordStats.critChance)
        {
            damage *= 2;
            IndSpawn(CritInd);
            Debug.Log("CRIT");
        }
        if (UnityEngine.Random.value > SwordStats.armorPenChance)
        {
            damage -= armor;
            if (damage < 0) damage = 0;
        }
        else
        {
            IndSpawn(ArmorPenInd);
            Debug.Log("ARMOR PENETRATION");
        }

        health -= damage;

        if (health <= 0)
        {
            GiveGold();
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
        GoldAmount += gold;
        PlayerGoldAddUpdate(gold);
        PlayerGoldUpdate();
    }

    private void Death()
    {
        Destroy(gameObject);
        enemyCount--;
    }
}
