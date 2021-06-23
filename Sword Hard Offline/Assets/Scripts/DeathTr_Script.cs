using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerData_Script;

public class DeathTr_Script : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
            enemyCount--;
            if (playerHealth > 0)
            {
                playerHealth--;
                PlayerHealthUpdate();
            }
        }
    }
}
