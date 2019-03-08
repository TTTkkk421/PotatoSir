using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    Animator animator;//控制角色动画

    private new Rigidbody2D rigidbody2D;//角色刚体

    public float MoveForce;//移动的力

    public float maxSpeed;//角色最大速度

    public float JumpForce;//跳跃力

    Transform groundcheck;//检测射线到地面的位置

    bool Grounded = false;//是否处于地面上

    bool isJump = false;//是否跳跃

    bool isRight = true;//朝右方向（正方向）

    Transform FirePlace;//开火的位置

    public Rigidbody2D MissilePrefab;//导弹预制体

    public float FireSpeed;//导弹发射速度

    bool isFire = false;//是否在开火

    void Awake()
    {
        animator = GetComponent<Animator>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        groundcheck = transform.Find("GroundCheck");
        FirePlace = transform.Find("bazooka/Fire");
    }

    void Update()
    {
        var h = Input.GetAxis("Horizontal");

        Grounded = Physics2D.Linecast(transform.position, groundcheck.position, 1 << LayerMask.NameToLayer("Ground"));

        if (h > 0)
        {
            isRight = true;
            animator.SetBool("Run", true);
            if (transform.localScale.x < 0)
            {
                ChangeScale();
            }
        }
        if (h < 0)
        {
            isRight = false;
            animator.SetBool("Run", true);
            if (transform.localScale.x > 0)
            {
                ChangeScale();
            }
        }
        if (h == 0)
        {
            animator.SetBool("Run", false);
        }

        if (Input.GetKeyDown(KeyCode.Space) && !isJump && Grounded)
        {
            isJump = true;
            animator.SetBool("Jump", true);
        }
    }

    private void FixedUpdate()
    {
        var h = Input.GetAxis("Horizontal");

        //移动
        if (h > 0 && isRight)
        {
            rigidbody2D.AddForce(new Vector2(MoveForce, 0) * h);
        }
        if (h < 0 && !isRight)
        {
            rigidbody2D.AddForce(new Vector2(MoveForce, 0) * h);
        }
        if (Mathf.Abs(rigidbody2D.velocity.x) > maxSpeed)
        {
            if (rigidbody2D.velocity.x > 0)
            {
                rigidbody2D.velocity = new Vector2(maxSpeed, rigidbody2D.velocity.y);
            }
            else
            {
                rigidbody2D.velocity = new Vector2(-maxSpeed, rigidbody2D.velocity.y);
            }
            
        }

        //跳跃
        if (isJump)
        {
            Jump();
        }

        //开火
        if (Input.GetKeyDown(KeyCode.F) && !isFire)
        {
            Fire();
        }
        if (Input.GetKeyUp(KeyCode.F))
        {
            isFire = false;
        }
    }

    void ChangeScale()
    {
        var thescale = transform.localScale;
        thescale.x *= -1;
        transform.localScale = new Vector3(thescale.x, transform.localScale.y, transform.localScale.z);
    }

    void Jump()
    {
        rigidbody2D.AddForce(new Vector2(0f, JumpForce));
        isJump = false;
        animator.SetBool("Jump", false);
    }

    void Fire()
    {
        isFire = true;
        Rigidbody2D missle =  Instantiate(MissilePrefab, FirePlace.position, Quaternion.identity);
        missle.gravityScale = 0;
        animator.SetTrigger("Shoot");
        if (isRight)
        {
            missle.velocity = new Vector2(FireSpeed, 0);
        }
        else
        {
            missle.transform.localScale *= -1;
            missle.velocity = new Vector2(-FireSpeed, 0);
        }
    }
}
