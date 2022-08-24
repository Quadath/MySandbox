using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public float hp = 100;

    public void Damage(float value)
    {
        hp -= value;
        if (hp <= 0)
        {
            FindObjectOfType<Grid2D>().DestroyBlock(transform.position);
            Destroy(gameObject);
        }
    }
}
