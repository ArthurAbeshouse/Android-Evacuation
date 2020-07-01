using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarGenerator : MonoBehaviour
{
    [SerializeField]
    GameObject Astar;

    [SerializeField]
    int MaximumStars;

    // Start is called before the first frame update
    void Start()
    {
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0)); // bottom-left side of the screen

        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1)); // top-right side of the screen

        for(int i = 0; i < MaximumStars; i++)
        {
            GameObject star = (GameObject)Instantiate(Astar);

            star.transform.position = new Vector2(Random.Range(min.x, max.x), Random.Range(min.y, max.y)); // Sets the stars position

            star.GetComponent<Star>().StarSpeed = -(1f * Random.value + 0.5f); // Randomly selected speed for stars

            star.transform.parent = transform; // Makes the star a child of the generator
        }
    }
}
