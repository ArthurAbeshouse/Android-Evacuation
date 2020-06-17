using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class B_Bullet : MonoBehaviour
{

    Rigidbody2D rb2d;

    [SerializeField]
    float speed;

    [SerializeField]
    private float DropGravity;

    [SerializeField]
    private float StartSpeed;

    private GameObject player = null;

    [SerializeField]
    public static int damage = 4;

    float DropDuration;

    // Vector2 Direction;

    private void Start()
    {
        Rigidbody2D rb2d = GetComponent<Rigidbody2D>();
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player");
        DropDuration = Mathf.Sqrt(StartSpeed * StartSpeed - 2 * DropGravity * rb2d.transform.position.y) / DropGravity;
        rb2d.velocity = new Vector2((player.transform.position.x - rb2d.transform.position.x) / DropDuration, -StartSpeed);
       // Vector2 Playerpos = new Vector2(player.transform.position.x, player.transform.position.y);
        //transform.rotation = Quaternion.LookRotation(Playerpos);
      //  Direction = (player.transform.position - transform.position).normalized * speed;
       // rb2d.velocity = new Vector2(Direction.x, Direction.y);
    }

    // Update is called once per frame
    void Update()
    {
        //Rigidbody2D rb2d = GetComponent<Rigidbody2D>();
      //  rb2d.velocity = (player.transform.position - transform.position).normalized * speed;
        rb2d.AddForce(new Vector2(0, rb2d.gravityScale));
        //rb2d.velocity = (player.transform.position - transform.position).normalized * speed;



        //transform.LookAt(player.transform.position);

        //transform.position += transform.forward * speed * Time.deltaTime;

       // transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        //rb2d.velocity = transform.position;
        // rb2d.velocity = new Vector2(-rb2d.velocity.x, Mathf.Abs(rb2d.velocity.x));

        //Direction = (player.position - transform.position).normalized * speed;
        // rb2d.velocity = new Vector2(player.transform.position.x, player.transform.position.y);
    }
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
