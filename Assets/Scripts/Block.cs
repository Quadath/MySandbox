using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    private HitPoints hp;
    void Start()
    {
        hp = GetComponent<HitPoints>();
        
        hp.onDeath = (() =>
        {
            FindObjectOfType<Grid2D>().DestroyBlock(transform.position);
        });
    }

    public void OnMouseDown()
    {
        hp.Damage(10);
    }
}
