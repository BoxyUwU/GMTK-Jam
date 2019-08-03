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
    public int ActiveDamage;

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
            int damageToApply = 0;
            if (Active)
            {
                damageToApply = ActiveDamage;
            }
            else
            {
                damageToApply = Damage;
            }

            GameObject.Find("Player").GetComponent<Health>().Amount -= damageToApply;
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
