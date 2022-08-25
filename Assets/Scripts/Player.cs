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
        
        RaycastHit2D bottomHit = Physics2D.Raycast(transform.position, -transform.up, .6f);
        float input = ClimbBugFix();
        if (Speed(rb.velocity.x) < speed)
        {
            rb.velocity = new Vector2(rb.velocity.x + input, rb.velocity.y);
        }


        if (bottomHit.collider != null && Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y + 13);
        }
    }

    float ClimbBugFix()
    {
        var pos = transform.position;
        var bottom = new Vector3(pos.x, pos.y - .44f, 0);
        var top = new Vector3(pos.x, pos.y + .44f, 0);
        RaycastHit2D rightHitBottom = Physics2D.Raycast(bottom, transform.right, .35f);
        RaycastHit2D rightHitTop = Physics2D.Raycast(top, transform.right, .35f);
        RaycastHit2D leftHitBottom = Physics2D.Raycast(bottom, -transform.right, .35f);
        RaycastHit2D leftHitTop = Physics2D.Raycast(top, -transform.right, .35f);
        
        float input = Input.GetAxis("Horizontal");

        if (leftHitTop.collider != null || leftHitBottom.collider != null)
            if (input < 0)
                input = 0;
        if(rightHitTop.collider != null || rightHitBottom.collider != null)
            if (input > 0)
                input = 0;

        
        // Debug.Log(" " + (rightHitBottom.collider != null) + " " + (rightHitTop.collider != null) + (leftHitBottom.collider != null) + (leftHitTop.collider != null));
        return input;
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