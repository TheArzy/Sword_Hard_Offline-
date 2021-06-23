using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerData_Script : MonoBehaviour
{
    DateTime dtime;
    public int lifeSpan;

    public static GameObject Sword;
    public static TheSword_Script SwordStats;
    public static PlayerData_Script PlData;

    public GameObject TheSword;
    public GameObject HealthInd;
    public GameObject GoldInd;
    public GameObject GoldAddInd;
    public GameObject WaveInd;

    static Text pHealthInd;
    static Text pGoldInd;
    static Text pGoldAddInd;
    static Text pWaveInd;

    public static int GoldAmount;
    public static int BestScore;
    public static int waveCount;
    public static int playerHealth;
    public static int enemyCount;

    void Awake()
    {
        dtime = DateTime.Now;
        GoldAddInd.SetActive(false);

        Sword = TheSword;
        playerHealth = 0;
        GoldAmount = 0;

        SwordStats = Sword.GetComponent<TheSword_Script>();
        PlData = gameObject.GetComponent<PlayerData_Script>();

        pHealthInd = HealthInd.GetComponent<Text>();
        pGoldInd = GoldInd.GetComponent<Text>();
        pGoldAddInd = GoldAddInd.GetComponent<Text>();
        pWaveInd = WaveInd.GetComponent<Text>();

        PlayerHealthUpdate();
        PlayerGoldUpdate();


        Debug.Log("Инициализация данных игрока");
    }

    private void FixedUpdate()
    {
        if (GoldAddInd.activeSelf && DateTime.Now.Subtract(dtime).TotalMilliseconds > lifeSpan)
        {
            GoldAddInd.SetActive(false);
        }
    }

    public static void PlayerHealthUpdate()
    {
        pHealthInd.text = playerHealth.ToString();
    }

    public static void PlayerGoldUpdate()
    {
        pGoldInd.text = GoldAmount.ToString();
    }

    public static void PlayerGoldAddUpdate(int gold)
    {
        pGoldAddInd.text = $"+{gold}";
        PlData.GoldAddInd.SetActive(true);
        PlData.dtime = DateTime.Now;
    }

    public static void WaveUpdate()
    {
        pWaveInd.text = waveCount.ToString();
    }
}
