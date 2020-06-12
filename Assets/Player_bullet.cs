using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_bullet : MonoBehaviour
{
   // Rigidbody2D rb2d;

    [SerializeField]
    float speed;

    [SerializeField]
    int damage;

    /*public void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }*/
    public void StartShoot(bool faceLeft)
	{
        Rigidbody2D rb2d = GetComponent<Rigidbody2D>();
        if (faceLeft)
		{
            rb2d.velocity = new Vector2(-speed, 0);
        }
		else
		{
            rb2d.velocity = new Vector2(speed, 0);
        }
    }
   /* public void Deflect(bool protection)
    {
        Rigidbody2D rb2d = GetComponent<Rigidbody2D>();
        rb2d.velocity = new Vector2(-rb2d.velocity.x, Mathf.Abs(rb2d.velocity.x));
    }*/
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

}
