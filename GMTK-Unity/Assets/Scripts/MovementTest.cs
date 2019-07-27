using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementTest : MonoBehaviour
{
    public Camera WorldCamera;
    Vector2 topLeftBounds;
    Vector2 bottomRightBounds;
    Transform transform;
    Vector2 velocity;
    float speed;

    // Start is called before the first frame update
    void Start()
    {
        int width = WorldCamera.pixelWidth;
        int height = WorldCamera.pixelHeight;
        transform = this.GetComponent<Transform>();
        topLeftBounds = new Vector2( -(((float)(width) / 2)/16), -(((float)(height) / 2)/16));
        bottomRightBounds = new Vector2(((float)(width) / 2) / 16, ((float)(height) / 2) / 16);
        speed = 0.1f;
        velocity = new Vector2(speed, speed);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x <= topLeftBounds.x)
            velocity.x = speed;
        if (transform.position.y <= topLeftBounds.y)
            velocity.y = speed;
        if (transform.position.x >= bottomRightBounds.x)
            velocity.x = -speed;
        if (transform.position.y >= bottomRightBounds.y)
            velocity.y = -speed;
        Vector2 newPos = transform.position += new Vector3(velocity.x, velocity.y);
        newPos.x = Mathf.Clamp(newPos.x, topLeftBounds.x, bottomRightBounds.x);
        newPos.y = Mathf.Clamp(newPos.y, topLeftBounds.y, bottomRightBounds.y);
        transform.position = new Vector3(newPos.x, newPos.y);
    }
}
