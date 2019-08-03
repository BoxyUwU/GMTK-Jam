using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretEnemy : MonoBehaviour
{

    public GameObject BulletPrefab;
    public float BulletAngleVariation;
    GameObject target;

    public float FireRate;
    public float BurstLength;
    public float BurstCoolDown;

    float fireRateCounter;
    float burstCooldownCounter;
    float burstLengthCounter;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        fireRateCounter += Time.deltaTime;
        if (fireRateCounter >= FireRate)
        {
            fireRateCounter -= ((int)(fireRateCounter / FireRate)) * FireRate;
            GameObject instantiatedBullet = Instantiate(BulletPrefab, GameObject.Find("BulletContainer").transform);

            instantiatedBullet.transform.position = this.transform.position;
            instantiatedBullet.transform.rotation = Quaternion.LookRotation(Vector3.forward, target.transform.position - instantiatedBullet.transform.position);

            instantiatedBullet.AddComponent<Team>();
            instantiatedBullet.GetComponent<Team>().TeamID = TeamIDs.Enemy;
        }
    }
}
