﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2D : MonoBehaviour
{
   // public List<string> items;
   // public PlayerHealthbar bars;

    Animator animator;
    Rigidbody2D rb2d;
    SpriteRenderer spriteRenderer;
    Collider2D playercolider;
    private Object bulletRef;
    bool isGrounded;
    private bool isShooting;
    private bool faceLeft;

    public int HealthBonus_big = 10;

    //public bool isMove;

    private bool isRunShooting;
    // private bool walkR;

    [Range(0, 28)]
    public int curHealth;

    public int maxHealth = 28;

    [SerializeField]
    GameObject bullet;

    [SerializeField]
    Transform BulletSpawn;

    [SerializeField]
    Transform groundCheck;

    [SerializeField]
    Transform groundCheckL;

    [SerializeField]
    Transform groundCheckR;

    [SerializeField]
    private float runSpeed = 1.5f;

    [SerializeField]
    private float runShootdelay = 1.5f;

    [Range(1, 10)]
    public float jumpSpeed;

    [SerializeField]
    private float shootDelay = 0f;

    bool pingas = true;

    // Start is called before the first frame update
    void Start()
    {
        //items = new List<string>();
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        playercolider = GetComponent<Collider2D>();
        playercolider.isTrigger = false;
        curHealth = maxHealth;
    }

    void Update()
	{
        var countOfExistingBullets = GameObject.FindGameObjectsWithTag("bullet").Length;
        if (Input.GetKeyDown("space") && isGrounded && Time.timeScale == 1)
        {
            rb2d.velocity = Vector2.up * jumpSpeed;
            animator.Play("player_jump");
            //MovebulletSpawn();
        }
        if (Input.GetKeyDown("h"))
        {
            curHealth -= 1;
        }
        if (Input.GetKeyDown("e") && countOfExistingBullets < 5 && Time.timeScale == 1)
        {
            if (isShooting)
                return;

            if (isGrounded && rb2d.velocity.x == 0)
                animator.Play("Player_Shoot");
            if (isGrounded && rb2d.velocity.x != 0 && !isRunShooting)
            {
                animator.Play("Player_walk_&_shoot");
                isRunShooting = true;
                Invoke("SetBoolBack", runShootdelay);
            }
            if (!isGrounded)
                animator.Play("Player_shoot_air");

            isShooting = true;

            //Shoot bullet
            GameObject b = Instantiate(bullet);
            b.GetComponent<Player_bullet>().StartShoot(faceLeft);
            b.transform.position = BulletSpawn.transform.position;

            Invoke("ResetShoot", shootDelay);
        }
        if(curHealth > maxHealth)
        {
            curHealth = maxHealth;
        }

        if (curHealth <= 0)
        {
            Dead ();
        }
    }

    void ResetShoot()
	{
        isShooting = false;
        if (isGrounded && rb2d.velocity.x == 0)
            animator.Play("player_idle");
        if (isGrounded && rb2d.velocity.x != 0 && !isRunShooting)
            animator.Play("player_run");
        if (!isGrounded)
            animator.Play("player_jump");
    }
    void SetBoolBack()
    {
        isRunShooting = false;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {

        if((Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"))) ||
          (Physics2D.Linecast(transform.position, groundCheckL.position, 1 << LayerMask.NameToLayer("Ground"))) ||
          (Physics2D.Linecast(transform.position, groundCheckR.position, 1 << LayerMask.NameToLayer("Ground"))))
        {
            isGrounded = true;
        }
		else
		{
            isGrounded = false;
            if (!isGrounded && !isShooting)
                animator.Play("player_jump");
        }

        if(Input.GetKey("d") || Input.GetKey("right"))
		{
            rb2d.velocity = new Vector2(runSpeed, rb2d.velocity.y);
            if (isGrounded && !isShooting && rb2d.velocity.x != 0 && !isRunShooting)
			{
                animator.Play("player_run");
			}
            transform.localScale = new Vector3(1, 1, 1);
            faceLeft = false;
        }
        else if(Input.GetKey("a") || Input.GetKey("left"))
		{
            rb2d.velocity = new Vector2(-runSpeed, rb2d.velocity.y);
            if (isGrounded && !isShooting && rb2d.velocity.x != 0 && !isRunShooting)
                animator.Play("player_run");
            transform.localScale = new Vector3(-1, 1, 1);
            faceLeft = true;
        }
        else
		{
            if (isGrounded && !isShooting && rb2d.velocity.x == 0)
                animator.Play("player_idle");
            rb2d.velocity = new Vector2(0, rb2d.velocity.y);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Pick_up") || collision.CompareTag("Big Health"))
        {
            Destroy(collision.gameObject);
            if(collision.CompareTag("Big Health"))
            {
                if (curHealth < maxHealth)
                    curHealth = curHealth + HealthBonus_big;
            }
        }
        Physics2D.IgnoreLayerCollision(11, 12, true);
     /*   if(collision.CompareTag("Enemy"))
        {
            //playercolider.isTrigger = true;
            curHealth--;
        }*/

    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            //playercolider.isTrigger = true;
            curHealth -= 2;
        }
    }
    /* private void OnCollisionEnter2D(Collision2D collision)
     {
         if (collision.gameObject.tag.Equals("Enemy"))
         {
             if (pingas)
                 curHealth -= 2;
         }
     }*/
    /*private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            playercolider.isTrigger = false;
           // curHealth -= 2;
        }
    }*/

    void Dead()
    {
        Application.LoadLevel(Application.loadedLevel);
    }
}
