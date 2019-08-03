using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    GameObject target;
    public float Speed;
    public bool Active;

    public Sprite ActiveSprite;
    public Sprite InactiveSprite;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Player");
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

        Vector2 velocity = target.transform.position - transform.position;
        velocity.Normalize();
        velocity *= Speed;
        this.GetComponent<Rigidbody2D>().velocity = velocity;
    }
}
