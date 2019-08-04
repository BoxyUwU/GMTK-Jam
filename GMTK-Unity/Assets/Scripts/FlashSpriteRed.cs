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
        // Lerps the color from the startColor -> targetColor -> startColor again
        // Sets the color back to what it was before lerping to make sure there are no slight floating point errors
        // during the lerp that would leave us with a slightly discolored sprite

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
