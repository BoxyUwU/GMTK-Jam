using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public int maxTime;
    public int minTime;

    public GameObject EnemyPrefabs;
    public int InitialSpawnInterval;
    public GameObject EnemyParent;
    public int MaxEnemiesInCluster;
    public int MinEnemiesInCluster;
    private float maxEnemyIncreaseTimer;
    public float EnemyIncreaseInterval;

    public int UpperMaxEnemiesInScene;
    public int CurrentMaxEnemiesInScene = 10;

    public List<Vector2> RelativeSpawnPositions;

    private float spawnTimerCount = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        RelativeSpawnPositions = new List<Vector2>
        {
            new Vector2(1, 0),
            new Vector2(-1, 0),
            new Vector2(0.5f, 1),
            new Vector2(-0.5f, 1),
            new Vector2(0.5f, -1),
            new Vector2(-0.5f, -1),
        };

        if (InitialSpawnInterval > 0.5f)
        {
            spawnTimerCount = InitialSpawnInterval - 0.5f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        maxEnemyIncreaseTimer += Time.deltaTime;
        if (maxEnemyIncreaseTimer > EnemyIncreaseInterval)
        {
            CurrentMaxEnemiesInScene += 1;
            maxEnemyIncreaseTimer = 0f;
        }




        spawnTimerCount += Time.deltaTime;
        if (spawnTimerCount >= InitialSpawnInterval && GetEnemiesInScene() < CurrentMaxEnemiesInScene)
        {
            spawnTimerCount -= ((int)(spawnTimerCount / InitialSpawnInterval)) * InitialSpawnInterval;

            InitialSpawnInterval = Random.Range(minTime, maxTime);

            int amountToSpawn = Random.Range(MinEnemiesInCluster, MaxEnemiesInCluster + 1);

            for (int i = 0; i < amountToSpawn; i++)
            {
                // Spawn Enemy
                Vector2 offset = RelativeSpawnPositions[Random.Range(0, RelativeSpawnPositions.Count - 1)];
                GameObject instancedEnemy = Instantiate(EnemyPrefabs, EnemyParent.transform, false);
                instancedEnemy.transform.position = new Vector3(this.transform.position.x + offset.x,
                                                                this.transform.position.y + offset.y,
                                                                instancedEnemy.transform.position.z);
            }
        }
    }

    private int GetEnemiesInScene()
    {
        int enemies = 0;
        foreach (Transform child in GameObject.Find("EnemyContainer").transform)
        {
            if (child.gameObject.layer == 8)
            {
                enemies++;
            }
        }
        return enemies;
    }
}
