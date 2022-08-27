using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPoints : MonoBehaviour
{
    public float maxHp;
    private float hp;
    
    public delegate void FiringDelegate();
    public FiringDelegate onDeath = (() =>
    {
        
    });

    private void Start()
    {
        hp = maxHp;
    }

    public int HP()
    {
        return (int)hp;
    }
    public void Damage(float value)
    {
        hp -= value;
        if (hp <= 0)
        {
            onDeath();
            Destroy(gameObject);
        }
    }
}
