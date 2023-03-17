using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    int playerHealth = 10;

    [SerializeField]
    Image healthBar;

    public void TakeDamage()
    {
        playerHealth--;
        healthBar.fillAmount -= 0.1f;
    }
}
