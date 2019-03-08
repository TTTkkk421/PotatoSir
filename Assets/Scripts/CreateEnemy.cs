using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateEnemy : MonoBehaviour
{
    public GameObject[] EnemyPrefabs;//怪物预制体

    bool isRight = true;

    void Start()
    {
        InvokeRepeating("createenemy", 1f, 5f);
    }

    void Update()
    {       
        if (isRight)
        {
            float x = transform.position.x;
            x += Time.deltaTime;
            transform.position = new Vector2(x, transform.position.y);
        }
        else
        {
            float x = transform.position.x;
            x -= Time.deltaTime;
            transform.position = new Vector2(x, transform.position.y);
        }
        if (transform.position.x <= -7)
        {
            isRight = true;
        }
        if (transform.position.x >= 7)
        {
            isRight = false;
        }
    }

    void createenemy()
    {
        float Num = Random.Range(0f, 2f);
        Debug.Log(Num);
        int Index = 0;
        if (Num >= 0.5f)
        {
            Index = 1;
        }
        Instantiate(EnemyPrefabs[Index], transform.position, Quaternion.identity);
    }
}
