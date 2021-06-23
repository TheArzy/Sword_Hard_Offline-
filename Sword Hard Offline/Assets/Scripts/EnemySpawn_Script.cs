using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerData_Script;

public class EnemySpawn_Script : MonoBehaviour
{

    DateTime dtime;
    DateTime waveStartTime;
    int tspan;
    int wspan;

    public int spRateMin, spRateMax;
    public int waveDuration;
    public int waveChangePause;
    public bool spawnFlag;
    public bool newWaveFlag;
    public bool GameOver;

    public GameObject Enemy;
    public GameObject StartButton;
    Transform Portal;

    private void Start()
    {
        Portal = gameObject.transform;
        dtime = DateTime.Now;
        waveStartTime = DateTime.Now;
        newWaveFlag = false;
        GameOver = false;
        waveCount = 1;
        WaveUpdate();
        Debug.Log($"Начальная волна {waveCount}");
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
        playerHealth = 3;
        PlayerHealthUpdate();
        spawnFlag = true;
        GameOver = false;
        StartButton.SetActive(false);
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

    void PlayerDeath()
    {
        Debug.Log("UrDead");
        spawnFlag = false;
        newWaveFlag = false;
        GameOver = true;
        StartButton.SetActive(true);

        waveCount = 1;

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }
    }

}
