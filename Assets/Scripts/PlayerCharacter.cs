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

    bool isTakeDamage = false;

    public GameObject text;

    void Awake()
    {
        current_health = MaxHealth;
        img_Health = transform.Find("Health/Canvas/HealthImage").GetComponent<Image>();
    }

    public void TakeDamage(int Damage)
    {
        if (isTakeDamage)
        {
            return;
        }
        isTakeDamage = true;
        Invoke("ReDamage", 0.2f);
        current_health -= Damage;
        img_Health.fillAmount = (float)current_health / MaxHealth;
        Rigidbody2D rig2d = GetComponent<Rigidbody2D>();
        rig2d.AddForce(new Vector2(3f, 3f), ForceMode2D.Impulse);

        if (current_health <= 0)
        {
            Death();            
        }
    }

    void Death()
    {
        Debug.Log("player Dead!");
        Collider2D[] cs = GetComponents<Collider2D>();
        foreach (var item in cs)
        {
            item.isTrigger = true;
        }
        GetComponent<PlayerControl>().enabled = false;
        Invoke("Stop", 1f);
    }

    void Stop()
    {
        text.SetActive(true);
        Time.timeScale = 0;
    }

    void ReDamage()
    {
        isTakeDamage = false;
    }
}
