using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public GameObject Pain;
    public int MaxHealth;
    public int Amount;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void PlayPainSound()
    {
        Pain.GetComponent<AudioSource>().Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (Amount <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
