using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommanderEnemy : MonoBehaviour
{
    public float Speed;
    public int Damage;
    public float InvulnerabilityLength;

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
        if (recalculateDirection)
            UpdateVelocity();
    }

    private void UpdateVelocity()
    {
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
        if (collision.gameObject.layer == 13)
        {
            if (GameObject.Find("Player").GetComponent<Invincible>() == null)
            {
                GameObject.Find("Player").GetComponent<Health>().Amount -= Damage;
                Invincible invincible = GameObject.Find("Player").AddComponent<Invincible>();
                invincible.Duration = InvulnerabilityLength;
                FlashSpriteRed flashRed = GameObject.Find("Player").AddComponent<FlashSpriteRed>();
                flashRed.Duration = 0.5f;
                flashRed.startColor = new Color(1, 0, 0, 0.125f);
                flashRed.targetColor = new Color(1, 0, 0, 1f);
            }
        }
    }
}
