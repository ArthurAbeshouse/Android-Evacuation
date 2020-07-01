using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorEnding : MonoBehaviour
{
    Animator animator;

    public bool Open = true;

    public float delayTime;

    public GameObject Boss;

    AudioSource Door_sounds;

    [SerializeField]
    AudioClip[] Door_snds_lib;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        Door_sounds = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    public void Update()
    {

        if (Boss != null && Open)
        {
            if (Boss.activeSelf)
            {
                Door_snds(0);
                animator.Play("ExitDoor");
                Open = false;
            }
        }
        if (Boss == null && !Open)
        {
            StartCoroutine(Playsnds(delayTime));
        }
    }

     IEnumerator Playsnds(float delayTime)
     {
        yield return new WaitForSeconds(delayTime);
        if (!Open)
        {
            Door_snds(0);
            animator.Play("OpenExitDoor");
            Open = true;
        }
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    void Door_snds(int index)
    {
        Door_sounds.clip = Door_snds_lib[index];
        Door_sounds.Play();
    }
}
