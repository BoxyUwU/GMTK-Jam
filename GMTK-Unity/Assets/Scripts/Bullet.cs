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
        if (collision.gameObject.layer == 12)
        {
            Destroy(this.gameObject);
            return;
        }

        // If the bullet is on a different team than what it collided with, damage it then destroy the bullet.
        Team targetTeam = collision.gameObject.GetComponent<Team>();
        Team bulletTeam = this.GetComponent<Team>();
        Health targetHealth = collision.gameObject.GetComponent<Health>();

        if (bulletTeam == null || targetTeam == null || targetHealth == null)
        { return; }

        if (targetTeam.TeamID != bulletTeam.TeamID)
        {
            if (collision.gameObject.layer == 8)
            {
                if (collision.gameObject.GetComponent<CommanderEnemy>() != null)
                {
                    collision.gameObject.GetComponent<Health>().Amount -= Damage;
                }
            }
            else
            {
                if (collision.gameObject.GetComponent<Invincible>() == null)
                {
                    collision.gameObject.GetComponent<Health>().Amount -= Damage;
                    Invincible invincible = collision.gameObject.AddComponent<Invincible>();
                    invincible.Duration = InvulnerabilityDuration;
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