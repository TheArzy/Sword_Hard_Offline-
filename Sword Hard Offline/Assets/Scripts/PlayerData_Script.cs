using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData_Script : MonoBehaviour
{
    public static GameObject Sword;
    public static TheSword_Script SwordStats;
    public GameObject TheSword;

    public static int GoldAmount;
    public static int BestScore;

    void Awake()
    {
        Sword = TheSword;
        SwordStats = Sword.GetComponent<TheSword_Script>();
        Debug.Log("Инициализация основных данных");
        
    }

    void Update()
    {

    }
}
