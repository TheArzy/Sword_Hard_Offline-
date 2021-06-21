using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData_Script : MonoBehaviour
{
    public static GameObject Sword;
    public static TheSword_Script SwordScript;
    int BestScore = 0;

    void Start()
    {
        Sword = GameObject.Find("TheGreateSword");
        SwordScript = Sword.GetComponent<TheSword_Script>();
    }

    void Update()
    {

    }
}
