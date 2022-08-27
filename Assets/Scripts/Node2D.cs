using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node2D
{
    public int gCost, hCost;
    public HitPoints hp;
    public bool obstacle;
    public Vector3 worldPosition;

    public int GridX, GridY;
    public Node2D parent;


    public Node2D(bool _obstacle, Vector3 _worldPos, int _gridX, int _gridY, HitPoints hp = null)
    {
        obstacle = _obstacle;
        worldPosition = new Vector3(_worldPos.x - 0.5f, _worldPos.y - 0.5f, 0);
        GridX = _gridX;
        GridY = _gridY;
        this.hp = hp;
    }

    public int FCost
    {
        get
        {
            return gCost + hCost;
        }

    }
    

    public void SetObstacle(bool isOb)
    {
        obstacle = isOb;
    }
}
