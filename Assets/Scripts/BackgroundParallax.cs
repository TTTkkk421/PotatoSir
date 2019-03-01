using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundParallax : MonoBehaviour
{
    public Transform[] backgrounds; //所有要背景补偿移动的背景的数组

    public float parallaxScale; //运动补偿系数

    public float parallaxReductionFactor; //与摄像机运动的关联程度

    public float smoothing; //背景运动平滑程度

    private Transform cam; //摄像机运动位置

    private Vector3 previousCamPos; //摄像机上一帧的位置

    private void Awake()
    {
        cam = Camera.main.transform;
    }

    void Start()
    {
        previousCamPos = cam.position;
    }

    
    void Update()
    {
        float parallax = (previousCamPos.x - cam.position.x) * parallaxScale; //定义数组中各元素在x轴上的运动量

        //数组中每一个背景都要移动
        for (int i = 0; i < backgrounds.Length; i++)
        {
            float backgroundTargetPosX = backgrounds[i].position.x + parallax * (i * parallaxReductionFactor + 1);//x轴上的移动量目的地

            Vector3 backgroundTargetPos = new Vector3(backgroundTargetPosX, backgrounds[i].position.y, backgrounds[i].position.z); //整体目的地坐标

            backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, backgroundTargetPos, smoothing * Time.deltaTime);//插值平滑过渡
        }

        previousCamPos = cam.position; // 记录摄像机的位置为下一帧使用

    }
}
