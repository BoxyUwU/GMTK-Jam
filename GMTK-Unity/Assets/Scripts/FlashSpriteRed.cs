using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashSpriteRed : MonoBehaviour
{
    Color initialColor;
    public Color startColor;
    public Color targetColor;
    public float Duration;
    float count;

    // Start is called before the first frame update
    void Start()
    {
        initialColor = GetComponent<SpriteRenderer>().color;
    }

    // Update is called once per frame
    void Update()
    {
        count += Time.deltaTime;
        if (count <= Duration / 2)
        {
            Color newColor = Color.Lerp(startColor, targetColor, count / (Duration / 2));
            this.GetComponent<SpriteRenderer>().color = newColor;
        }
        else
        {
            float lerpProgress = count - (Duration / 2);
            Color newColor = Color.Lerp(targetColor, initialColor, lerpProgress / (Duration / 2));
            this.GetComponent<SpriteRenderer>().color = newColor;
        }

        if (count >= Duration)
        {
            initialColor.a = 1;
            this.GetComponent<SpriteRenderer>().color = initialColor; 
            Destroy(this.GetComponent<FlashSpriteRed>());
        }
    }
}
