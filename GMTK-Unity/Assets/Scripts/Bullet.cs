﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public int Damage;
    public float Speed;

    // Start is called before the first frame update
    void Start()
    {
        //Rotates bullet to mouse
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.rotation = Quaternion.LookRotation(Vector3.forward, mousePos - transform.position);
        
    }

    // Update is called once per frame
    void Update()
    {

        //Bullet Movement
        transform.position += transform.up * Speed * Time.deltaTime;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If the bullet is on a different team than what it collided with, damage it then destroy the bullet.
        Team targetTeam = collision.gameObject.GetComponent<Team>();
        Team bulletTeam = this.GetComponent<Team>();
        Health targetHealth = collision.gameObject.GetComponent<Health>();

        if (bulletTeam == null || targetTeam == null || targetHealth == null)
        { return; }

        if (targetTeam.TeamID != bulletTeam.TeamID)
        {
            collision.gameObject.GetComponent<Health>().Amount -= Damage;
            Destroy(this.gameObject);
        }
    }



    }











