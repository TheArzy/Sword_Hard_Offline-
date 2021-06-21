using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData_Script : MonoBehaviour
{
    public static GameObject Sword;
    int BestScore = 0;

    void Awake()
    {
        Sword = GameObject.Find("TheGreateSword");
        Debug.Log("Сворд инициализация");
    }

    void Update()
    {

    }
}
