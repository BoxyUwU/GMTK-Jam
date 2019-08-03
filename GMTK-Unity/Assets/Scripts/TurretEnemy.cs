using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretEnemy : MonoBehaviour
{

    public GameObject BulletPrefab;
    public float BulletAngleVariation;
    GameObject target;

    public float FireRate;
    public float BurstDuration;
    public float BurstCoolDown;

    float fireRateCounter;
    float burstCooldownCounter;
    float burstDurationCounter;

    bool firing;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        // If we are in a birst
        if (firing)
        {
            // Fire bullets
            fireRateCounter += Time.deltaTime;
            if (fireRateCounter >= FireRate)
            {
                fireRateCounter -= ((int)(fireRateCounter / FireRate)) * FireRate;
                GameObject instantiatedBullet = Instantiate(BulletPrefab, GameObject.Find("BulletContainer").transform);

                instantiatedBullet.transform.position = this.transform.position;
                instantiatedBullet.transform.rotation = Quaternion.LookRotation(Vector3.forward, target.transform.position - instantiatedBullet.transform.position);
                instantiatedBullet.transform.Rotate(0, 0, Random.Range(-BulletAngleVariation, BulletAngleVariation));

                instantiatedBullet.AddComponent<Team>();
                instantiatedBullet.GetComponent<Team>().TeamID = TeamIDs.Enemy;
            }

            // Increment burst duration counter
            burstDurationCounter += Time.deltaTime;
            if (burstDurationCounter >= BurstDuration)
            {
                burstDurationCounter = 0f;
                burstCooldownCounter = 0f;
                firing = false;
            }
        } // If we are in the cooldown period between bursts
        else if (firing == false)
        {
            burstCooldownCounter += Time.deltaTime;
            if (burstCooldownCounter >= BurstCoolDown)
            {
                burstCooldownCounter = 0.0f;
                firing = true;
                fireRateCounter = 0.0f;
            }
        }
    }
}
