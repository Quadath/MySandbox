using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
  private float speed = 5;
  private Rigidbody2D rb;

  private void Start()
  {
    rb = GetComponent<Rigidbody2D>();
    rb.AddForce(transform.right * speed * 100);
  }

  public void OnCollisionEnter2D(Collision2D other)
  {
    Destroy(gameObject);
  }
}
