using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryMusic : MonoBehaviour
{
    AudioSource SweetVictory;

    public GameObject Boss;

    public float delayTime;

    public bool Playbgm;

    // Start is called before the first frame update
    void Start()
    {
        SweetVictory = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Boss != null && !Playbgm)
        {
            Playbgm = true;
        }
        if (Boss == null && Playbgm)
        {
            StartCoroutine(Playsnds(delayTime));
        }
        if (Time.timeScale == 0)
            SweetVictory.Pause();
        else
            SweetVictory.UnPause();
    }

    IEnumerator Playsnds(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        if (Playbgm)
        {
            SweetVictory.Play();
            Playbgm = false;
        }
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
