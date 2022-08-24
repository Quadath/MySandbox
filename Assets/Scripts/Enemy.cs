using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Vector3 target;
    public Pathfinding2D ptf;
    public Transform finish;

    private void Start()
    {
        ptf = GetComponent<Pathfinding2D>();
        ptf.FindPath(transform.position, finish.position);
        target = ptf.path[1].worldPosition;
    }

    private void Update()
    {
        if (transform.position != target)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, .002f);
        }
        else
        {
            ptf.FindPath(transform.position, finish.position);
            target = ptf.path[0].worldPosition;
        }
    }
}
