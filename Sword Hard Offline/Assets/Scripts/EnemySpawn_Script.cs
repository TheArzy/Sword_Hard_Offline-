using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerData_Script;

public class EnemySpawn_Script : MonoBehaviour
{

    public DateTime dtime;
    public DateTime waveStartTime;
    public int tspan;
    public int wspan;

    public int spRateMin, spRateMax;
    public int waveDuration;
    public int waveChangePause;

    public GameObject Enemy;
    Transform Portal;

    private void Start()
    {
        Portal = gameObject.transform;

        dtime = DateTime.Now;
        waveStartTime = DateTime.Now;

        newWaveFlag = false;
        GameOver = false;

        Debug.Log("Инициализация спаунера");
    }

    void FixedUpdate()
    {
        if (!GameOver)
        {
            if (!spawnFlag && !newWaveFlag) waveStartTime = DateTime.Now;
            if (spawnFlag)
            {
                tspan = (int)DateTime.Now.Subtract(dtime).TotalMilliseconds;
                wspan = (int)DateTime.Now.Subtract(waveStartTime).TotalSeconds;

                if (wspan < waveDuration && !newWaveFlag && tspan >= UnityEngine.Random.Range(spRateMin, spRateMax))
                {
                    EnemySpawn(Enemy, waveCount);
                    dtime = DateTime.Now;
                }
                else if (wspan > waveDuration)
                {
                    spawnFlag = false;
                    newWaveFlag = true;
                    waveStartTime = DateTime.Now;
                }
            }
            else if ((spawnFlag || newWaveFlag) && playerHealth <= 0) PlayerDeath();
            else if (newWaveFlag && enemyCount > 0) waveStartTime = DateTime.Now;

            if (newWaveFlag && enemyCount == 0 && waveChangePause < (int)DateTime.Now.Subtract(waveStartTime).TotalSeconds)
            {
                NextWave();
            };
        }
    }

    public void GameStart()
    {
        ReceivedGold = 0;
        playerHealth = 3;
        PlayerHealthUpdate();

        dtime = DateTime.Now;
        waveStartTime = DateTime.Now;

        spawnFlag = true;
        GameOver = false;

        PlData.MainMenu.SetActive(false);
        PlData.InGameInterface.SetActive(true);
        PlData.GoldAddInd.SetActive(false);
        PlData.TheSword.SetActive(true);
    }

    void EnemySpawn(GameObject Enemy, int wave)
    {
        Vector3 PortalPos = Portal.position;

        Instantiate(
            Enemy,
            new Vector3(UnityEngine.Random.Range(PortalPos.x - 40, PortalPos.x + 40), PortalPos.y, PortalPos.z),
            Portal.rotation
            ).GetComponent<Enemy_Script>().level = wave;


        enemyCount++;
    }

    void NextWave()
    {
        waveCount++;
        WaveUpdate();
        spawnFlag = true;
        newWaveFlag = false;
        waveStartTime = DateTime.Now;
    }

    public static void PlayerDeath()
    {
        spawnFlag = false;
        newWaveFlag = false;
        GameOver = true;
        Time.timeScale = 1;

        if (waveCount - 1 > BestScore)
        {
            BestScoreUpdate();
            PlData.NewRecordInd.SetActive(true);
        }

        int goldBonus = 0;
        if (waveCount > 1)
        {
            goldBonus = (100 * waveCount * waveCount) / 2;
            GoldAmount += goldBonus;
        }
        PlayerGoldRecieveUpdate(goldBonus);

        ScoreUpdate();
        enemyCount = 0;
        waveCount = 1;
        WaveUpdate();

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }

        PlData.ResultMenu.SetActive(true);
        PlData.InGameInterface.SetActive(false);
        PlData.TheSword.SetActive(false);
    }

}
