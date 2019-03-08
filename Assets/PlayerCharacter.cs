using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCharacter : MonoBehaviour
{
    //最大血量
    public int MaxHealth = 100;

    //当前血量
    int current_health;

    //血量UI
    Image img_Health;

    void Awake()
    {
        current_health = MaxHealth;
        img_Health = transform.Find("Health/Canvas/HealthImage").GetComponent<Image>();
    }

    public void TakeDamage(int Damage)
    {
        current_health -= Damage;
        img_Health.fillAmount = (float)current_health / MaxHealth;

        if (current_health <= 0)
        {
            Debug.Log("player Dead!");
        }
    }
}
