using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause_game : MonoBehaviour
{
    public static bool isPaused = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("p"))
		{
            if (isPaused)
                Time.timeScale = 1;
            else
                Time.timeScale = 0;
            isPaused = !isPaused;
        }
    }
}
