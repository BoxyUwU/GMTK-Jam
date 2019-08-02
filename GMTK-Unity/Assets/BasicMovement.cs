using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovement : MonoBehaviour
{

    //Variables
    public float speed;
    public float jump;
    float moveVelocity;
    bool onFloor = true;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //Jumping
        if (Input.GetKeyDown (KeyCode.W))
        {
            if(onFloor)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jump);

            }



            moveVelocity = 0;

            //Left Right Movement
            if (Input.GetKey(KeyCode.A))
            {
                moveVelocity = -speed;
            }
            if (Input.GetKey(KeyCode.D))
            {
                moveVelocity = speed;
            }

            GetComponent<Rigidbody2D>().velocity = new Vector2(moveVelocity, GetComponent<Rigidbody2D>().velocity.y);
            
            //Check if Grounded
            void OnCollisionEnter2D(Collision2D col)
            {
        onFloor = true;
        }
        void OnCollisionExit2D(Collision2D col)
            {
        onFloor = false;
        }


        }











    }
}
