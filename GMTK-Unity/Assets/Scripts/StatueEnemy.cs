﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatueEnemy : MonoBehaviour
{
    public float Speed;
    public int Damage;
    public float InvulnerabilityLength;

    GameObject target;
    Vector2 direction;
    public bool recalculateDirection = true;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Player");
        Debug.Log(this.GetComponent<Rigidbody2D>().velocity.magnitude);
        if (recalculateDirection && this.GetComponent<Rigidbody2D>().velocity.magnitude > 0.05f)
        {
            this.GetComponent<Rigidbody2D>().velocity = this.GetComponent<Rigidbody2D>().velocity.normalized * Speed;
            recalculateDirection = false;
        }
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
        Vector2 velocity = Random.insideUnitCircle.normalized;
        direction = velocity;
        // We don't multiply by deltaTime because rigidbody physics does it for us
        velocity *= Speed;
        this.GetComponent<Rigidbody2D>().velocity = velocity;
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
