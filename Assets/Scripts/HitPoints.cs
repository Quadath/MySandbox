using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPoints : MonoBehaviour
{
    public float maxHp;
    private float hp;

    private void Start()
    {
        hp = maxHp;
    }

    public void Damage(float value)
    {
        hp -= value;
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }
}
