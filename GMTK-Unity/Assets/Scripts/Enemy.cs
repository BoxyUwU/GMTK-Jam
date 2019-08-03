using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    GameObject target;
    public float Speed;
    public float ActiveSpeed;
    public bool Active;

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
            RecalculateVelocity();
        }
        else
        {
            this.GetComponent<SpriteRenderer>().sprite = InactiveSprite;
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
