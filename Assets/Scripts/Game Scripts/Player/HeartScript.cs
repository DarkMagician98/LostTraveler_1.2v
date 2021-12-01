using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HeartScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Action affectHeartEvent;
    private int heartCount;

    void Start()
    {
        affectHeartEvent += damageHeart;
        heartCount = transform.childCount;
    }

    void damageHeart()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            GameObject child = transform.GetChild(i).gameObject;
            if (child.activeSelf)
            {
                heartCount--;
                child.SetActive(false);
                break;
            }
        }
     /*   foreach(GameObject heart in transform)
        {
            if (heart.activeSelf)
            {
                heart.SetActive(false);
                break;
            }
        }*/
    }

    public bool IsDead() => heartCount <= 0;

    // Update is called once per frame
    void Update()
    {
        
    }
}
