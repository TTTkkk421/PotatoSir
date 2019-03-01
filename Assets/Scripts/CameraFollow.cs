using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float xMargin = 1f; //摄像机与角色之间的x距离（不会超过此距离）
    public float yMargin = 1f; //摄像机与角色之间的y距离
    public float xSmooth = 8f; 
    public float ySmooth = 8f;
    public Vector2 MaxXAndY; //在此范围之内，摄像机才会跟随主角一起移动,摄像机运动范围被限定在这里面
    public Vector2 MinXAndY;

    private Transform player; //玩家位置

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    bool CheckXMargin()
    {
        return Mathf.Abs(transform.position.x - player.position.x) > xMargin;//检测x轴上的距离是否超出
    }

    bool CheckYMargin()
    {
        return Mathf.Abs(transform.position.y - player.position.y) > yMargin;//检测y轴上的距离是否超出
    }

    private void FixedUpdate()
    {
        TrackPlayer();
    }

    void TrackPlayer()
    {
        float targetX = transform.position.x;
        float targetY = transform.position.y;

        //超出距离，进行移动
        if (CheckXMargin())
        {
            targetX = Mathf.Lerp(transform.position.x, player.position.x, xSmooth * Time.deltaTime); //目标坐标x轴
        }

        if (CheckYMargin())
        {
            targetY = Mathf.Lerp(transform.position.y, player.position.y, ySmooth * Time.deltaTime); //目标坐标Y轴
        }

        //如果目标位置超出了我们限定的摄像机移动范围，目标位置只能移动到限定的边界
        targetX = Mathf.Clamp(targetX, MinXAndY.x, MaxXAndY.x);
        targetY = Mathf.Clamp(targetY, MinXAndY.y, MaxXAndY.y);

        transform.position = new Vector3(targetX, targetY, transform.position.z);
    }
}
