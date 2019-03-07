using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missle : MonoBehaviour
{
    GameObject boomanim;//爆炸动画物体

    Animator animator;//爆炸动画控制器

    GameObject MissleModel;//导弹模型

    Rigidbody2D missile_rig;//导弹刚体

    private void Awake()
    {
        boomanim = transform.Find("Boom").gameObject;
        animator = GetComponent<Animator>();
        MissleModel = transform.Find("part_rocket").gameObject;
        missile_rig = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            missile_rig.velocity = new Vector2(0, 0);
            MissleModel.SetActive(false);
            boomanim.SetActive(true);
            animator.Play("Boom");
            Invoke("Destroy", 0.5f);
        }
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }

}
