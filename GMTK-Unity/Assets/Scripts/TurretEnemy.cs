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
    public GameObject ShootSound;
    AudioClip shootSoundClip;
    public float LaserStartWidth;
    public float LaserEndWidth;
    public Color LaserStartColor;
    public Color LaserEndColor;

    float fireRateCounter;
    float burstCooldownCounter;
    float burstDurationCounter;

    bool firing;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Player");
        if (BurstCoolDown >= 0.5f)
        {
            burstCooldownCounter = BurstCoolDown - 0.5f;
        }
        shootSoundClip = ShootSound.GetComponent<AudioSource>().clip;
    }

    // Update is called once per frame
    void Update()
    {
        // The shooty AI currently works by having periods of shooting
        // and then a cooldown period where it doesn't shoot
        // the periods of shooting are referred to here as "bursts"
        // The firerate is the time between each individual bullet is fired during a burst

        // Draw laser between Turret and Commander
        LineRenderer lineRenderer = GetComponent<LineRenderer>();
        GameObject commander = GameObject.Find("CommanderEnemy(Clone)");
        
        if (commander == null)
        {
            lineRenderer.enabled = false;
        }
        else
        {
            lineRenderer.enabled = true;
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, commander.transform.position);
            lineRenderer.startWidth = LaserStartWidth;
            lineRenderer.endWidth = LaserEndWidth;
            lineRenderer.startColor = LaserStartColor;
            lineRenderer.endColor = LaserEndColor;
        }

        // If we are in a burst
        if (firing)
        {
            // Fire bullets
            fireRateCounter += Time.deltaTime;
            if (fireRateCounter >= FireRate)
            {
                fireRateCounter -= ((int)(fireRateCounter / FireRate)) * FireRate;
                ShootSound.GetComponent<AudioSource>().PlayOneShot(shootSoundClip);
                GameObject instantiatedBullet = Instantiate(BulletPrefab, GameObject.Find("BulletContainer").transform);

                instantiatedBullet.transform.position = this.transform.position;
                instantiatedBullet.transform.rotation = Quaternion.LookRotation(Vector3.forward, target.transform.position - instantiatedBullet.transform.position);
                // Add some small variation to bullet trajectory so it isn't a laser (although a laser would be easier to dodge)
                instantiatedBullet.transform.Rotate(0, 0, Random.Range(-BulletAngleVariation, BulletAngleVariation));

                instantiatedBullet.AddComponent<Team>();
                instantiatedBullet.GetComponent<Team>().TeamID = this.GetComponent<Team>().TeamID;
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
