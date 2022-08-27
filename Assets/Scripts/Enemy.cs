using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class Enemy : MonoBehaviour
{
    public float speed;
    public Node2D target;
    public List<Node2D> path => ptf.path;

    private Pathfinding2D ptf;
    private Transform player;
    private Grid2D gr;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ptf = GetComponent<Pathfinding2D>();
        gr = FindObjectOfType<Grid2D>();
        player = FindObjectOfType<Player>().transform;
        ptf.FindPath(transform.position, player.position);
        target = path[1];
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, player.position) > 1)
        {
            if (Vector3.Distance(transform.position, target.worldPosition) > 1)
            {
                if (path[0].obstacle)
                {
                    gr.ObjectFromWorldPoint(path[0].worldPosition).GetComponent<HitPoints>().Damage(.5f);
                }

                Vector3 dir = (target.worldPosition - transform.position) / 2 * (speed * Time.deltaTime);
                rb.velocity = new Vector2(rb.velocity.x + dir.x, rb.velocity.y + dir.y);
            }
            else
            {
                ptf.FindPath(transform.position, player.position);
                if (ptf.path[1] != null) 
                target = ptf.path[1];
            }
        }
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.name == transform.name)
        {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), other.collider);
        }
    }
}