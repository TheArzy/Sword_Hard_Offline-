using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static EnemySpawn_Script;

public class PlayerData_Script : MonoBehaviour
{
    DateTime dtime;
    public int lifeSpan;

    public static GameObject Sword;
    public static TheSword_Script SwordStats;
    public static PlayerData_Script PlData;

    public GameObject TheSword;
    public GameObject MainMenu;
    public GameObject PauseMenu;
    public GameObject ResultMenu;
    public GameObject InGameInterface;

    public GameObject HealthInd;
    public GameObject GoldInd_Menu;
    public GameObject GoldInd_Pause;
    public GameObject GoldInd_Result;
    public GameObject ReceivedGoldInd;
    public GameObject GoldInd;
    public GameObject GoldAddInd;
    public GameObject WaveInd;
    public GameObject BestScoreInd;
    public GameObject ResultScoreInd;
    public GameObject UpgradeCostInd;
    public GameObject SwordLevelInd;
    public GameObject NewRecordInd;

    static Text pHealthInd;
    static Text pGoldInd_Menu;
    static Text pGoldInd_Pause;
    static Text pGoldInd_Result;
    static Text pReceivedGoldInd;
    static Text pGoldInd;
    static Text pGoldAddInd;
    static Text pWaveInd;
    static Text pBestScoreInd;
    static Text pResultScoreInd;
    static Text pUpgradeCostInd;
    static Text pSwordLevelInd;

    public static int GoldAmount;
    public static int ReceivedGold;
    public static int playerHealth;
    public static int enemyCount;
    public static int waveCount = 1;
    public static int BestScore = 0;

    public static int Base_swDamage = 10;
    public static float Base_swCritChance = 0.2f;
    public static float Base_swArmorPenChance = 0.15f;
    public static int Base_upgradeCost = 5000;

    public static bool spawnFlag;
    public static bool newWaveFlag;
    public static bool GameOver;

    void Awake()
    {
        dtime = DateTime.Now;

        Sword = TheSword;
        playerHealth = 0;
        GoldAmount = 0;

        SwordStats = Sword.GetComponent<TheSword_Script>();
        PlData = gameObject.GetComponent<PlayerData_Script>();

        pHealthInd = HealthInd.GetComponent<Text>();
        pGoldInd_Menu = GoldInd_Menu.GetComponent<Text>();
        pGoldInd_Pause = GoldInd_Pause.GetComponent<Text>();
        pGoldInd_Result = GoldInd_Result.GetComponent<Text>();
        pReceivedGoldInd = ReceivedGoldInd.GetComponent<Text>();
        pGoldInd = GoldInd.GetComponent<Text>();
        pGoldAddInd = GoldAddInd.GetComponent<Text>();
        pWaveInd = WaveInd.GetComponent<Text>();
        pBestScoreInd = BestScoreInd.GetComponent<Text>();
        pResultScoreInd = ResultScoreInd.GetComponent<Text>();
        pUpgradeCostInd = UpgradeCostInd.GetComponent<Text>();
        pSwordLevelInd = SwordLevelInd.GetComponent<Text>();

        Debug.Log("Инициализация данных игрока");

        PlayerHealthUpdate();
        PlayerGoldUpdate();
        WaveUpdate();
        SwordLevelUpdate();

        SwordStats.upgradeCost = Base_upgradeCost;
        UpgradeCostUpdate();

        InGameInterface.SetActive(false);
        TheSword.SetActive(false);

        Debug.Log("Инициализация интерфейса");

    }

    private void FixedUpdate()
    {
        if (GoldAddInd.activeSelf && DateTime.Now.Subtract(dtime).TotalMilliseconds > lifeSpan)
        {
            GoldAddInd.SetActive(false);
        }
    }

    public void PauseButton()
    {
        Time.timeScale = 0;
        PauseMenu.SetActive(true);
        TheSword.SetActive(false);
    }

    public void ToMainMenuButton()
    {
        if (Time.timeScale == 0)
        {
            PauseMenu.SetActive(false);
            PlayerDeath();
        }
        else
        {
            ResultMenu.SetActive(false);
            MainMenu.SetActive(true);
        }
        PlData.NewRecordInd.SetActive(false);
    }

    public void ResumeButton()
    {
        Time.timeScale = 1;
        PauseMenu.SetActive(false);
        TheSword.SetActive(true);
    }

    public static void PlayerHealthUpdate()
    {
        pHealthInd.text = playerHealth.ToString();
    }

    public static void PlayerGoldUpdate()
    {
        pGoldInd.text = $"Gold: {GoldAmount}";
        pGoldInd_Menu.text = $"Gold: {GoldAmount}";
        pGoldInd_Pause.text = $"Gold: {GoldAmount}";
        pGoldInd_Result.text = $"Gold: {GoldAmount}";
    }

    public static void PlayerGoldAddUpdate(int gold)
    {
        pGoldAddInd.text = $"+{gold}";
        PlData.GoldAddInd.SetActive(true);
        PlData.dtime = DateTime.Now;
    }

    public static void PlayerGoldRecieveUpdate(int goldBonus)
    {
        pReceivedGoldInd.text = $"{ReceivedGold} Received\n +{goldBonus} Bonus";
        PlayerGoldUpdate();
    }

    public static void ScoreUpdate()
    {
        pResultScoreInd.text = $"Score: {waveCount - 1}";
    }

    public static void BestScoreUpdate()
    {
        BestScore = waveCount-1;
        pBestScoreInd.text = $"Best Score: {BestScore}";
    }

    public static void WaveUpdate()
    {
        pWaveInd.text = waveCount.ToString();
    }

    public static void UpgradeCostUpdate()
    {
        pUpgradeCostInd.text = $"Cost: {SwordStats.upgradeCost}";
    }

    public static void SwordLevelUpdate()
    {
        pSwordLevelInd.text = $"Lvl {SwordStats.swordLevel}";
    }

}
