using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    GameObject target;
    public bool Active;

    public float Speed;
    public float ActiveSpeed;

    public int Damage;
    public float DamageInvulnerabilityLength;

    public int ActiveDamage;
    public float ActiveDamageInvulnerabilityLength;

    public Sprite ActiveSprite;
    public Sprite InactiveSprite;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Player");
        RecalculateVelocity();
    }

    // Update is called once per frame
    void Update()
    {
        if (Active)
        {
            this.GetComponent<SpriteRenderer>().sprite = ActiveSprite;
        }
        else
        {
            this.GetComponent<SpriteRenderer>().sprite = InactiveSprite;
        }
    }

    private void FixedUpdate()
    {
        if (Active)
        {
            RecalculateVelocity();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 13)
        {
            if (GameObject.Find("Player").GetComponent<Invincible>() == null)
            {
                int damageToApply = 0;
                float invulnerabilityTime = 0.0f;
                if (Active)
                {
                    damageToApply = ActiveDamage;
                    invulnerabilityTime = ActiveDamageInvulnerabilityLength;
                }
                else
                {
                    damageToApply = Damage;
                    invulnerabilityTime = DamageInvulnerabilityLength;
                }

                GameObject.Find("Player").GetComponent<Health>().Amount -= damageToApply;
                Invincible invincible = GameObject.Find("Player").AddComponent<Invincible>();
                invincible.Duration = invulnerabilityTime;
                FlashSpriteRed flashRed = GameObject.Find("Player").AddComponent<FlashSpriteRed>();
                flashRed.Duration = 0.5f;
                flashRed.startColor = new Color(1, 0, 0, 0.125f);
                flashRed.targetColor = new Color(1, 0, 0, 1f);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 12)
            RecalculateVelocity();
    }

    public void RecalculateVelocity()
    {
        Vector2 velocity = target.transform.position - transform.position;
        velocity.Normalize();
        if (Active)
        { velocity *= ActiveSpeed; }
        else
        { velocity *= Speed; }
        this.GetComponent<Rigidbody2D>().velocity = velocity;
    }
}
