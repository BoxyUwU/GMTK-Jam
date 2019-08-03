using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public GameObject Player;
    public List<GameObject> Hearts;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < Hearts.Count; i++)
        {
            Hearts[i].SetActive(false);
        }
        for (int i = 0; i < Player.GetComponent<Health>().Amount; i++)
        {
            Hearts[i].SetActive(true);
        }
    }
}
