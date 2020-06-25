using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicScript : MonoBehaviour
{
    AudioSource audioSource;
    // Start is called before the first frame update
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        //GetComponentInParent<BossScript>().health;
        if (Time.timeScale == 0)
            audioSource.Pause();
        else
            audioSource.UnPause();
        if (GetComponentInParent<BossScript>().health <= 0)
            audioSource.Stop();
        // audioSource.Play();
    }
}
