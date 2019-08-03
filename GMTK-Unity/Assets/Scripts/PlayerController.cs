using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    //Variables
    public float WalkSpeed;
    public float JumpSpeed;
    bool grounded = true;
    Rigidbody2D rigidbody;
    public float MaxSpeed;

    public bool JumpResetsVelocity = false;


    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Vector2 inputVelocity = new Vector2();
        //Jumping
        if (Input.GetKeyDown(KeyCode.W) && grounded)
        {
            grounded = false;
            inputVelocity.y += JumpSpeed;

            if (JumpResetsVelocity && rigidbody.velocity.y < 0)
            {
                rigidbody.velocity = new Vector2(rigidbody.velocity.x, 0);
            }
        }
        //Left Right Movement
        if (Input.GetKey(KeyCode.A))
        {
            inputVelocity.x -= WalkSpeed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            inputVelocity.x += WalkSpeed;
        }

        rigidbody.velocity += inputVelocity;

        if (rigidbody.velocity.x > MaxSpeed)
        {
            rigidbody.velocity = new Vector2(MaxSpeed, rigidbody.velocity.y);
        }
        if (rigidbody.velocity.x < -MaxSpeed)
        {
            rigidbody.velocity = new Vector2(-MaxSpeed, rigidbody.velocity.y);
        }

        if (rigidbody.velocity.x < 0)
        {
            this.GetComponent<SpriteRenderer>().flipX = false;
        }
        else
        {
            this.GetComponent<SpriteRenderer>().flipX = true;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<GivesJump>() != null)
            grounded = true;
    }
}
