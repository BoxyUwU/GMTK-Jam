using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 velocity = target.transform.position - transform.position;
        velocity.Normalize();
        velocity /= 16;
        transform.position += (Vector3)velocity;
    }
}
