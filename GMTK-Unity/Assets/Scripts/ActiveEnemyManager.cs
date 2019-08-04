using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveEnemyManager : MonoBehaviour
{
    public GameObject CommanderPrefab;
    public GameObject TurretPrefab;
    public GameObject StatuePrefab;
    public GameObject TurretShootSound;

    public GameObject EnemyContainer;
    GameObject activeEnemy;
    List<GameObject> enemies;
    LineRenderer lineRenderer;
    GameObject nextActiveEnemy;

    public float MinStartWidth = 0.1f;
    public float MaxStartWidth = 0.5f;
    public float MinEndWidth = 0.1f;
    public float MaxEndWith = 0.5f;
    public Color LaserStartColor;
    public Color LaserEndColor;

    public float ActiveEnemySwitchTime = 1;
    public float TimerCount = 0.0f;

    public int CurrentTurretLimit;
    public int MinTurrets;
    public int MaxTurrets;
    public float TurretLimitIncreaseInterval;
    float turretIncreasCounter;

    // Start is called before the first frame update
    void Start()
    {
        enemies = new List<GameObject>();
        lineRenderer = this.GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        TimerCount += Time.deltaTime;
        turretIncreasCounter += Time.deltaTime;

        if (turretIncreasCounter >= TurretLimitIncreaseInterval)
        {
            turretIncreasCounter = 0;
            if (CurrentTurretLimit < MaxTurrets)
            {
                CurrentTurretLimit++;
            }
        }

        if (GameObject.Find("CommanderEnemy(Clone)") != null)
        {
            GameObject.Find("CommanderEnemy(Clone)").GetComponent<CommanderEnemy>().CurrentTurretLimit = CurrentTurretLimit;
        }

        // Get an up to date list of enemies
        enemies.Clear();
        foreach (Transform child in EnemyContainer.transform)
        {
            // Make sure we have an enemy
            if (child.gameObject.layer == 8)
            {
                if (child.GetComponent<CommanderEnemy>())
                    activeEnemy = child.gameObject;
                enemies.Add(child.gameObject);
            }
        }

        // Store the next active enemy
        if (nextActiveEnemy == null && enemies.Count > 0)
        {
            enemies.Remove(activeEnemy);
            nextActiveEnemy = enemies[Random.Range(0, enemies.Count)];
        }

        // Timer logic
        if ((TimerCount >= ActiveEnemySwitchTime || activeEnemy == null) && enemies.Count > 0)
        {
            TimerCount = 0.0f;

            // Set old active enemy to innactive and set velocity back to normal
            if (activeEnemy != null)
            {
                //activeEnemy.GetComponent<Enemy>().Active = false;
                SwapEnemyType(activeEnemy, StatuePrefab);
            }

            // Set new active enemy
            activeEnemy = nextActiveEnemy;
            //activeEnemy.GetComponent<Enemy>().Active = true;
            SwapEnemyType(activeEnemy, CommanderPrefab);

            // Null the next active enemy variable
            nextActiveEnemy = null;
        }

        // Draw laser from activeEnemy -> nextActiveEnemy
        if (activeEnemy != null && nextActiveEnemy != null)
        {
            lineRenderer.enabled = true;
            lineRenderer.SetPosition(0, activeEnemy.transform.position);
            lineRenderer.SetPosition(1, nextActiveEnemy.transform.position);
            float startWidth = Mathf.Lerp(MinStartWidth, MaxStartWidth, TimerCount / ActiveEnemySwitchTime);
            float endWidth = Mathf.Lerp(MinEndWidth, MaxEndWith, TimerCount / ActiveEnemySwitchTime);
            Color laserColor = Color.Lerp(LaserStartColor, LaserEndColor, TimerCount / ActiveEnemySwitchTime);
            lineRenderer.startWidth = startWidth;
            lineRenderer.endWidth = endWidth;
            lineRenderer.startColor = laserColor * 0.9f;
            lineRenderer.endColor = laserColor * 0.5f;
        }
        else { lineRenderer.enabled = false; }
    }

    private void SwapEnemyType(GameObject enemy, GameObject prefab)
    {
        Vector2 position = enemy.transform.position;
        Vector2 velocity = enemy.GetComponent<Rigidbody2D>().velocity;
        GameObject instancedEnemy = Instantiate(prefab, EnemyContainer.transform);
        instancedEnemy.transform.position = position;
        instancedEnemy.GetComponent<Rigidbody2D>().velocity = velocity;

        if (instancedEnemy.GetComponent<CommanderEnemy>() != null)
        {
            instancedEnemy.GetComponent<CommanderEnemy>().TurretShootSound = TurretShootSound;
        }

        Destroy(enemy);
    }

}
