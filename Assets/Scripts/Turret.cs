using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    private Transform target;
    private Transform tower;

    private float rotSpeed = 1;


    private void Start()
    {
        tower = transform.GetChild(1);
    }

    public void SetTarget(Transform tr)
    {
        if (target) return;
        target = tr;
    }

    private void Update()
    {
        if (target)
        {
            Vector3 vectorToTarget = target.transform.position - transform.position;
            float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            tower.rotation = Quaternion.RotateTowards(tower.rotation, q, 70f * rotSpeed *Time.deltaTime);
        }
    }
}
