using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invincible : MonoBehaviour
{
    public float Duration = 0.0f;
    float initialAlpha;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Turn player transparent
        Color color = GetComponent<SpriteRenderer>().color;
        initialAlpha = color.a;
        color.a = 0.75f;
        GetComponent<SpriteRenderer>().color = color;

        Duration -= Time.deltaTime;
        if (Duration <= 0)
        {
            color.a = 100;
            GetComponent<SpriteRenderer>().color = color;
            Destroy(this.GetComponent<Invincible>());
        }
    }
}
