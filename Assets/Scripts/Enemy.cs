using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform target;

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target.position, 0.002f);
    }
}
