﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    public GameObject enemyCanvasGo;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
       
        if (collision.transform.CompareTag("Potato"))
        {
            enemyCanvasGo.GetComponent<EnemyHpBar>().Dmg();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Potato"))
        {
            enemyCanvasGo.GetComponent<EnemyHpBar>().Dmg();
        }
    }
}
