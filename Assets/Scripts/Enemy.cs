using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform target;
    public Pathfinding2D ptf;
    private Grid2D gr;

    private void Start()
    {
        ptf = GetComponent<Pathfinding2D>();
        gr = ptf.GridOwner.GetComponent<Grid2D>();
        ptf.FindPath(transform.position, target.transform.position);
        StartCoroutine("Walk");
    }

    private void Update()
    {
        // Debug.Log(ptf.GridOwner.GetComponent<Grid2D>().path);
        // transform.position = Vector2.MoveTowards(transform.position, target.position, 0.002f);
    }

    private int index = 0;
    IEnumerator Walk()
    {
        while (index < gr.path.Count)
        {
            transform.position = ptf.GridOwner.GetComponent<Grid2D>().path[index].worldPosition;
            index++;
            yield return new WaitForSeconds(.5f);
        }        
    }
}
