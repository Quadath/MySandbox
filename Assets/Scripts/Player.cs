using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private float speed = 4;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        RaycastHit2D rightHit = Physics2D.Raycast(transform.position, transform.right, .35f);
        RaycastHit2D leftHit = Physics2D.Raycast(transform.position, -transform.right, .35f);
        RaycastHit2D bottomHit = Physics2D.Raycast(transform.position, -transform.up, .6f);
        
        if (Speed(rb.velocity.x) < speed)
        {
            rb.velocity = new Vector2(rb.velocity.x + Input.GetAxis("Horizontal"), rb.velocity.y);
        }

        if (bottomHit.collider != null && Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y + 13);
        }
    }

    private float Speed(float value)
    {
        if (value < 0)
        {
            return value * -1;
        }
        return value;
    }
}
