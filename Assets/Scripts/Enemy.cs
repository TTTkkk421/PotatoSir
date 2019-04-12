using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    //最大血量
    public int MaxHealth = 100;

    //怪物移动速度
    public float MoveSpeed;

    //怪物攻击力
    public int Damage = 10;

    //怪物是否活着
    bool isAlive = true;

    //碰撞体
    CapsuleCollider2D capsuleCollider2D;

    //怪物刚体
    Rigidbody2D rig_Enemy;

    //向左向右移动
    int isRight = 1;

    //当前血量
    int current_health;

    //血量UI
    Image img_Health;

    //怪物状态动画
    Animator animator;

    //地面检测
    bool Grounded = false;

    private void Awake()
    {
        current_health = MaxHealth;
        img_Health = transform.Find("Health/Canvas/HealthImage").GetComponent<Image>();
        animator = GetComponent<Animator>();
        rig_Enemy = GetComponent<Rigidbody2D>();
        capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        Invoke("Destroy", 10f);
    }

    private void FixedUpdate()
    {
        if (!isAlive)
        {
            return;
        }

        if (isRight == 1 && Grounded)
        {
            rig_Enemy.velocity = new Vector2(MoveSpeed, 0);
        }
        else if (isRight == -1 && Grounded)
        {
            rig_Enemy.velocity = new Vector2(-MoveSpeed, 0);
        }
        if (!Grounded)
        {
            rig_Enemy.velocity = new Vector2(0, 0);
        }

    }

    public void TakeDamage(int Damage)
    {
        current_health -= Damage;
        img_Health.fillAmount = (float)current_health / MaxHealth;

        if (current_health < MaxHealth)
        {
            animator.SetTrigger("Damage");
        }

        if (current_health <= 0)
        {
            isAlive = false;
            animator.SetTrigger("Death");
            capsuleCollider2D.isTrigger = true;
            rig_Enemy.velocity = new Vector2(0, 0);
            Invoke("Destroy", 1f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            isRight *= -1;
            ChangeScale();
        }
        if (collision.gameObject.tag == "Player")
        {
            PlayerCharacter playerCharacter = collision.gameObject.GetComponent<PlayerCharacter>();
            playerCharacter.TakeDamage(Damage);
        }
        if (collision.gameObject.layer == 8)
        {
            Grounded = true;
        }
        if (collision.gameObject.tag == "Enemy")
        {
            capsuleCollider2D.isTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        capsuleCollider2D.isTrigger = false;
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }

    void ChangeScale()
    {
        var thescale = transform.localScale;
        thescale.x *= -1;
        transform.localScale = new Vector3(thescale.x, transform.localScale.y, transform.localScale.z);
    }
}
