using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerData_Script;

public class TheSword_Script : MonoBehaviour
{
    public int swordLevel = 1;
    public int upgradeCost;
    public int swDamage;
    public float critChance;
    public float armorPenChance;

    public GameObject Camera;
    Vector3 CamStartPos;
    private float MoveSpeed = 0.012f;

    void Start()
    {
        swDamage = Base_swDamage;
        critChance = Base_swCritChance;
        armorPenChance = Base_swArmorPenChance;

        CamStartPos = Camera.transform.position + new Vector3(-6.5f, 0, -11.5f);
        Debug.Log("Инициализация клинка");
    }

    void Update()
    {
        gameObject.transform.position = new Vector3(
            (Input.mousePosition.x * MoveSpeed) + CamStartPos.x, 
            CamStartPos.y - 20, 
            (Input.mousePosition.y * MoveSpeed) + CamStartPos.z
            );
    }

    public void SwordUpgrade()
    {
        if (upgradeCost <= GoldAmount)
        {
            GoldAmount -= upgradeCost;
            PlayerGoldUpdate();
            swordLevel++;
            swDamage += (int)(Base_swDamage * (swordLevel / 10f));
            SwordLevelUpdate();
            upgradeCost += (int)(upgradeCost / 10f);
            UpgradeCostUpdate();
        }
    }
}
