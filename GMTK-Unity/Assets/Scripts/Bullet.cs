using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int Damage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
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
