using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Enemy;
    void Start()
    {
        StartCoroutine("Spawn");
    }

    IEnumerator Spawn()
    {
        float spawnDelay = 8;
        int wave = 250;
        while (wave > 0)
        {
            Instantiate(Enemy, transform.position, Quaternion.identity);
            wave--;
            if (spawnDelay > 2.5f)
            {
                spawnDelay -= .03f;
            }
            yield return new WaitForSeconds(spawnDelay + Random.Range(-2, 3));
        }
    }
}
