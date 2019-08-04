using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommanderEnemy : MonoBehaviour
{
    public float Speed;
    public int Damage;
    public float InvulnerabilityLength;
    public float ConversionDistance;
    public GameObject ConvertedEnemyPrefab;
    public GameObject RevertedEnemyPrefab;
    public GameObject TurretShootSound;

    public int CurrentTurretLimit;
    public int MinTurrets;
    public int MaxTurrets;
    public float TurretLimitIncreaseInterval;
    float turretIncreasCounter;

    List<GameObject> convertedEnemies;
    GameObject target;
    public bool recalculateDirection = true;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        // Increase MaxTurrets;
        turretIncreasCounter += Time.deltaTime;
        GameObject.Find("GameManager").GetComponent<ScoreCounter>().Text.GetComponent<UnityEngine.UI.Text>().text = turretIncreasCounter.ToString();
        Debug.Log(TurretLimitIncreaseInterval);
        if (turretIncreasCounter >= TurretLimitIncreaseInterval)
        {
            turretIncreasCounter = 0.0f;
            if (CurrentTurretLimit < MaxTurrets)
                CurrentTurretLimit++;
        }


        // Get number of turrets
        int turrets = 0;
        foreach (Transform child in GameObject.Find("EnemyContainer").transform)
        {
            if (child.GetComponent<TurretEnemy>() != null)
            {
                turrets++;
            }
        }

        // Get an up to date list of enemies
        foreach (Transform child in GameObject.Find("EnemyContainer").transform)
        {
            // Make sure we have an enemy
            if (child.gameObject.layer == 8)
            {
                // Checks if there are enemies within a square around the CommanderEnemy
                bool withinConversionDistance = false;
                Vector2 childPosition = child.transform.position;
                Vector2 enemyPosition = this.transform.position;
                float dX = Mathf.Abs(enemyPosition.x - childPosition.x);
                float dY = Mathf.Abs(enemyPosition.y - childPosition.y);
                float distance = Mathf.Max(dX, dY);
                if (distance <= ConversionDistance)
                    withinConversionDistance = true;

                // Convert statues into shooty dudes
                if (withinConversionDistance && child.GetComponent<StatueEnemy>() != null && turrets < CurrentTurretLimit)
                {
                    turrets++;
                    SwapEnemyType(child.gameObject, ConvertedEnemyPrefab);
                } // Turn shooty dudes back into statues if out of range
                else if (!withinConversionDistance && child.GetComponent<TurretEnemy>() != null)
                {
                    SwapEnemyType(child.gameObject, RevertedEnemyPrefab);
                }
            }
        }

        if (recalculateDirection)
            UpdateVelocity();
    }

    private void UpdateVelocity()
    {
        // Move towards where the player is but with some innacuracy
        recalculateDirection = false;
        Vector2 velocity = (target.transform.position - this.transform.position).normalized;
        velocity = Quaternion.AngleAxis(Random.Range(-10, 10), Vector3.forward) * velocity;
        this.GetComponent<Rigidbody2D>().velocity = velocity * Speed;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        // If we hit a wall change direction
        if (collision.gameObject.layer == 12)
        {
            recalculateDirection = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Layer 13 is a collider that is attached to the player for the sole purpose of allowing us to have enemies move through players
        // but still be able to check if enemies and players overlap
        if (collision.gameObject.layer == 13)
        {
            // Same code as in Bullet.CS
            if (GameObject.Find("Player").GetComponent<Invincible>() == null)
            {
                GameObject.Find("Player").GetComponent<Health>().Amount -= Damage;
                Invincible invincible = GameObject.Find("Player").AddComponent<Invincible>();
                invincible.Duration = InvulnerabilityLength;
                if (GameObject.Find("Player").GetComponent<FlashSpriteRed>() == null)
                {
                    FlashSpriteRed flashRed = GameObject.Find("Player").AddComponent<FlashSpriteRed>();
                    flashRed.Duration = 0.5f;
                    flashRed.startColor = new Color(1, 0, 0, 0.125f);
                    flashRed.targetColor = new Color(1, 0, 0, 1f);
                }
                GameObject.Find("Player").GetComponent<Health>().PlayPainSound();
            }
        }
    }

    // This is the function used to convert statues into shooty dudes.
    // we remove the statue and then spawn in a new enemy of the shooty type.
    private void SwapEnemyType(GameObject enemy, GameObject prefab)
    {
        Vector2 position = enemy.transform.position;
        Vector2 velocity = enemy.GetComponent<Rigidbody2D>().velocity;
        GameObject instancedEnemy = Instantiate(prefab, GameObject.Find("EnemyContainer").transform);
        instancedEnemy.transform.position = position;
        instancedEnemy.GetComponent<Rigidbody2D>().velocity = velocity;

        if (instancedEnemy.GetComponent<TurretEnemy>() != null)
        {
            instancedEnemy.GetComponent<TurretEnemy>().ShootSound = TurretShootSound;
        }
        Destroy(enemy);
    }
}
