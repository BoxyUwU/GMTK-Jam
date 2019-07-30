using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGenerator : MonoBehaviour
{
    public GameObject PlantPrefab;
    public Camera WorldCamera;

    // Start is called before the first frame update
    void Start()
    {
        int width = WorldCamera.pixelWidth;
        int height = WorldCamera.pixelHeight;
        Vector2 topLeftBounds = new Vector2(-(((float)(width) / 2) / 16), -(((float)(height) / 2) / 16));
        Vector2 bottomRightBounds = new Vector2(((float)(width) / 2) / 16, ((float)(height) / 2) / 16);

        int amounToGenerate = 50;
        
        for (int i = 0; i < amounToGenerate; i++)
        {
            int x = (int)Random.Range(topLeftBounds.x, bottomRightBounds.x);
            int y = (int)Random.Range(topLeftBounds.y, bottomRightBounds.y);
            GameObject plant = Instantiate(PlantPrefab, transform);
            Vector3 spawnPos = new Vector3(x, y, 0);
            plant.transform.position = spawnPos;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
