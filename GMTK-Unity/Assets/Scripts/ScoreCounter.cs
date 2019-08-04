using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    public GameObject Text;
    public int Score;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Text.GetComponent<UnityEngine.UI.Text>().text = Score.ToString();
    }
}
