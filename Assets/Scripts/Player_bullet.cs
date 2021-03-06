﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_bullet : MonoBehaviour
{
   // Rigidbody2D rb2d;

    [SerializeField]
    float speed;

   // [SerializeField]
    public static int damage = 1;

    //[SerializeField]
    public GameObject Enemy_met;

    EnemyScript bool_true;

    AudioSource Player_bullet_sounds;

    [SerializeField]
    AudioClip[] Player_bullet_snds_lib;

    public void StartShoot(bool faceLeft)
	{
        Rigidbody2D rb2d = GetComponent<Rigidbody2D>();
        if (faceLeft)
		{
            rb2d.velocity = new Vector2(-speed, 0);
          //  Debug.Log("this works");
        }
		else
		{
            rb2d.velocity = new Vector2(speed, 0);
            //Debug.Log("that works");
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        Rigidbody2D rb2d = GetComponent<Rigidbody2D>();
        bool_true = Enemy_met.GetComponent<EnemyScript>();
        Player_bullet_sounds = GetComponent<AudioSource>();

        // Enemy_health = Ememy_met.GetComponent<EnemyScript>();
        // if (bool_true.protection)
        //collision.gameObject.
        if (!bool_true.protection && collision.CompareTag("Enemy"))
        {
            // damage = 0;
            rb2d.velocity = new Vector2(-rb2d.velocity.x, Mathf.Abs(rb2d.velocity.x));
            Player_bullet_sounds.clip = Player_bullet_snds_lib[0];
            Player_bullet_sounds.Play();
            //Debug.Log("it went through");
        }
    }
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

}
