using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveEnemyManager : MonoBehaviour
{
    public GameObject EnemyContainer;
    GameObject activeEnemy;
    List<GameObject> enemies;

    public float ActiveEnemySwitchTime = 1;
    float timerCount = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        enemies = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        bool setActiveEnemy = false;

        // Get an up to date list of enemies
        enemies.Clear();
        foreach (Transform child in EnemyContainer.transform)
        {
            // Make sure we have an enemy
            if (child.GetComponent<Enemy>() != null)
            {
                enemies.Add(child.gameObject);
                if (child.GetComponent<Enemy>().Active)
                    activeEnemy = child.gameObject;
            }
        }

        // Timer logic
        timerCount += Time.deltaTime;
        if ((timerCount >= ActiveEnemySwitchTime || activeEnemy == null) && enemies.Count > 0)
        {
            timerCount = 0.0f;

            // Get a new active enemy
            // This makes it so we don't select the already active enemy
            if (enemies.Count >= 2)
                enemies.Remove(activeEnemy);

            GameObject newActiveEnemy = enemies[Random.Range(0, enemies.Count - 1)];

            // Switch active enemy to newly selected enemy
            if (activeEnemy != null)
                activeEnemy.GetComponent<Enemy>().Active = false;
            newActiveEnemy.GetComponent<Enemy>().Active = true;
            activeEnemy = newActiveEnemy;
        }
    }
}
