using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretTrigger : MonoBehaviour
{
    private Turret turr;

    private void Start()
    {
        turr = transform.parent.GetComponent<Turret>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("Enemy"))
            turr.SetTarget(other.transform);
    }
}
