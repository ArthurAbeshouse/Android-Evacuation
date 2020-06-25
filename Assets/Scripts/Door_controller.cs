using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door_controller : MonoBehaviour
{
    Animator animator;

   // Collider2D Door_collision;

    private GameObject child;
    //Collider2D DoorCol;
   // private GameObject door;

    // Start is called before the first frame update
    void Start()
    {
        child = GameObject.Find("door_0");
        animator = GetComponentInChildren<Animator>();
      //  Door_collision = GetComponent<Collider2D>();
      //  Door_collision.enabled = true;
        //DoorCol = gameObject.GetComponentInChildren<Collider2D>();
        animator.Play("Close_door");
        // door = GetComponentInChildren<door_0>();
        // animator.Play("Door");
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Went Through");
           // Door_collision.enabled = !Door_collision.enabled;
            child.SetActive(true);
            animator.Play("Close_door");
          //  transform.GetChild(1).gameObject.SetActive(false);
        }
    }
}
