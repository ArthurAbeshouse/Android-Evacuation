using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{
    public float StarSpeed;

    // Update is called once per frame
    void Update()
    {

        Vector2 position = transform.position; // Star's current position

        position = new Vector2(position.x, position.y + StarSpeed * Time.deltaTime); // Calculates the Star's new position

        transform.position = position;

        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0)); // bottom-left side of the screen

        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1)); // top-right side of the screen

        if (transform.position.y < min.y)
        {
            transform.position = new Vector2(Random.Range(min.x, max.x), max.y);
        }
    }
}
