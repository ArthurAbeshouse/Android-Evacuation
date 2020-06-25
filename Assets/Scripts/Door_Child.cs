using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door_Child : MonoBehaviour
{
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Went Through");
            animator.Play("Door");
            //  transform.GetChild(1).gameObject.SetActive(false);
        }
    }
    void Door_Off()
    {
        gameObject.SetActive(false);
    }
}
