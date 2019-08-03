using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject EnemyPrefabs;
    public int SpawnInterval;
    public GameObject EnemyParent;

    private float spawnTimerCount = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        spawnTimerCount += Time.deltaTime;
        if (spawnTimerCount >= SpawnInterval)
        {
            spawnTimerCount -= ((int)(spawnTimerCount / SpawnInterval)) * SpawnInterval;

            // Spawn Enemy
            GameObject instancedEnemy = Instantiate(EnemyPrefabs, EnemyParent.transform, false);
            instancedEnemy.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, instancedEnemy.transform.position.z);
        }
    }
}
