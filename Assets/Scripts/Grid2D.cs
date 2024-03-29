﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Grid2D : MonoBehaviour
{
    public Vector3 gridWorldSize;
    public float nodeRadius;
    public Node2D[,] Grid;
    public List<Node2D> path;
    private GameObject[,] tiles;
    Vector3 worldBottomLeft;

    float nodeDiameter;
    public int gridSizeX, gridSizeY;

    void Awake()
    {
        nodeDiameter = nodeRadius * 2;
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);
        
        tiles = new GameObject[gridSizeX, gridSizeY];
        Transform[] children = GetComponentsInChildren<Transform>();
        foreach (var child in children)
        {
            if (child.name != "Grid")
            {
                tiles[(int) child.position.x, (int) child.position.y] = child.gameObject;
            }
        }
        CreateGrid();

    }


    void CreateGrid()
    {
        Grid = new Node2D[gridSizeX, gridSizeY];
        worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.up * gridWorldSize.y / 2;

        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) +
                                     Vector3.up * (y * nodeDiameter + nodeRadius);
                Grid[x, y] = new Node2D(false, worldPoint, x, y, tiles[x, y] ? tiles[x, y].GetComponent<HitPoints>() : null);

                if (HasTile(WorldToCell(Grid[x, y].worldPosition)))
                    Grid[x, y].SetObstacle(true);
                else
                    Grid[x, y].SetObstacle(false);
            }
        }
    }


    //gets the neighboring nodes in the 4 cardinal directions. If you would like to enable diagonal pathfinding, uncomment out that portion of code
    public List<Node2D> GetNeighbors(Node2D node)
    {
        List<Node2D> neighbors = new List<Node2D>();

        //checks and adds top neighbor
        if (node.GridX >= 0 && node.GridX < gridSizeX && node.GridY + 1 >= 0 && node.GridY + 1 < gridSizeY)
            neighbors.Add(Grid[node.GridX, node.GridY + 1]);

        //checks and adds bottom neighbor
        if (node.GridX >= 0 && node.GridX < gridSizeX && node.GridY - 1 >= 0 && node.GridY - 1 < gridSizeY)
            neighbors.Add(Grid[node.GridX, node.GridY - 1]);

        //checks and adds right neighbor
        if (node.GridX + 1 >= 0 && node.GridX + 1 < gridSizeX && node.GridY >= 0 && node.GridY < gridSizeY)
            neighbors.Add(Grid[node.GridX + 1, node.GridY]);

        //checks and adds left neighbor
        if (node.GridX - 1 >= 0 && node.GridX - 1 < gridSizeX && node.GridY >= 0 && node.GridY < gridSizeY)
            neighbors.Add(Grid[node.GridX - 1, node.GridY]);


          // Uncomment this code to enable diagonal movement
         
         //checks and adds top right neighbor
         if (node.GridX + 1 >= 0 && node.GridX + 1< gridSizeX && node.GridY + 1 >= 0 && node.GridY + 1 < gridSizeY)
             neighbors.Add(Grid[node.GridX + 1, node.GridY + 1]);

         //checks and adds bottom right neighbor
         if (node.GridX + 1>= 0 && node.GridX + 1 < gridSizeX && node.GridY - 1 >= 0 && node.GridY - 1 < gridSizeY)
             neighbors.Add(Grid[node.GridX + 1, node.GridY - 1]);

         //checks and adds top left neighbor
         if (node.GridX - 1 >= 0 && node.GridX - 1 < gridSizeX && node.GridY + 1>= 0 && node.GridY + 1 < gridSizeY)
             neighbors.Add(Grid[node.GridX - 1, node.GridY + 1]);

         //checks and adds bottom left neighbor
         if (node.GridX - 1 >= 0 && node.GridX - 1 < gridSizeX && node.GridY  - 1>= 0 && node.GridY  - 1 < gridSizeY)
             neighbors.Add(Grid[node.GridX - 1, node.GridY - 1]);


        return neighbors;
    }
        
    public void SetBlock(Vector3Int v, GameObject tile)
    {
        if (!HasTile(v))
        {
            tiles[v.x, v.y] = Instantiate(tile, v, Quaternion.identity);
            Grid[v.x, v.y].SetObstacle(true);
        }
    }

    public void DestroyBlock(Vector3 worldPosition)
    {
        var v = V3toV3I(worldPosition);
        tiles[v.x, v.y] = null;
        Grid[v.x, v.y].SetObstacle(false);
    }

    public GameObject ObjectFromWorldPoint(Vector3 worldPosition)
    {
        int x = Mathf.RoundToInt(worldPosition.x);
        int y = Mathf.RoundToInt(worldPosition.y);
        return tiles[x, y];
    }
    public Node2D NodeFromWorldPoint(Vector3 worldPosition)
    {
        int x = Mathf.RoundToInt(worldPosition.x);
        int y = Mathf.RoundToInt(worldPosition.y);
        return Grid[x, y];
    }

    public bool HasTile(Vector3Int v)
    {
        if (tiles[v.x, v.y] != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public Vector3Int WorldToCell(Vector3 v)
    {
        return new Vector3Int((int) v.x, (int) v.y, 0);
    }

    //Draws visual representation of grid
    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, gridWorldSize.y, 1));
    
        if (Grid != null)
        {
            foreach (Node2D n in Grid)
            {
                if (n.obstacle)
                    Gizmos.color = Color.red;
                else
                    Gizmos.color = Color.white;
    
                if (path != null && path.Contains(n))
                    Gizmos.color = Color.black;
                Gizmos.DrawCube(n.worldPosition, Vector3.one * (nodeRadius));
            }
        }
    }

    private static Vector3Int V3toV3I(Vector3 v)
    {
        return new Vector3Int((int) v.x, (int) v.y, (int) v.z);
    }
}