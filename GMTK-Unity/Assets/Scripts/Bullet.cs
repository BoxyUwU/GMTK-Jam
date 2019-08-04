using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public int Damage;
    public float Speed;
    public float InvulnerabilityDuration;

    // Start is called before the first frame update
    void Start()
    {   
    }

    // Update is called once per frame
    void Update()
    {
        //Bullet Movement
        transform.position += transform.up * Speed * Time.deltaTime;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Destroy bullet if it hits a wall
        if (collision.gameObject.layer == 12)
        {
            Destroy(this.gameObject);
            return;
        }

        // Make sure required variables are here
        Team targetTeam = collision.gameObject.GetComponent<Team>();
        Team bulletTeam = this.GetComponent<Team>();
        Health targetHealth = collision.gameObject.GetComponent<Health>();
        if (bulletTeam == null || targetTeam == null || targetHealth == null)
        { return; }

        // Make sure enemy bullets dont hit enemies and player bullets dont hit players
        if (targetTeam.TeamID != bulletTeam.TeamID)
        {
            // Layer 8 is enemies so this checks if the bullet hit an enemy
            if (collision.gameObject.layer == 8)
            {
                // Check the bullet hit a red dude as thet are the ONLY ONE we can attack
                if (collision.gameObject.GetComponent<CommanderEnemy>() != null)
                {
                    collision.gameObject.GetComponent<Health>().Amount -= Damage;
                }
            } // The only thing it could have hit here is a player
            else
            {
                // Dont deal damage if we are invincible
                if (collision.gameObject.GetComponent<Invincible>() == null)
                {
                    collision.gameObject.GetComponent<Health>().Amount -= Damage;

                    // Give temporary invincibility after being hit
                    Invincible invincible = collision.gameObject.AddComponent<Invincible>();
                    invincible.Duration = InvulnerabilityDuration;
                    // Flash the sprite red to show we got hit
                    if (collision.gameObject.GetComponent<FlashSpriteRed>() == null)
                    {
                        FlashSpriteRed flashRed = collision.gameObject.AddComponent<FlashSpriteRed>();
                        flashRed.Duration = 0.5f;
                        flashRed.startColor = new Color(1, 0, 0, 0.125f);
                        flashRed.targetColor = new Color(1, 0, 0, 1f);
                    }
                }
            }
            Destroy(this.gameObject);
        }
    }
}