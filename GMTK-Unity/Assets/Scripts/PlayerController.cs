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

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Vector2 inputVelocity = new Vector2();
        //Jumping
        if (Input.GetKeyDown(KeyCode.W))
        {
            inputVelocity.y += JumpSpeed;
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
    }
}
