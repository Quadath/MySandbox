using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceBlock : MonoBehaviour
{
    public Grid2D grid;
    public GameObject tile;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            var vect = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            grid.SetBlock(new Vector3Int((int) vect.x, (int) vect.y, 0), tile);
        }
    }
}