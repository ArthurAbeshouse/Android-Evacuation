using System.Collections;
using System.Collections.Generic;
using System.IO.MemoryMappedFiles;
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
    bool wasGrounded;
    private bool isShooting;
    private bool faceLeft;
    private bool isHurt;
  //  public Vector2 RespawnPoint;

    int HealthBonus_big = 10;

    int HealthBonus_small = 2;

    //public bool isMove;

    private bool isRunShooting;
    // private bool walkR;

    //materials
    private Material matWhite;
    private Material matDefault;

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
    private float shootDelay;

    AudioSource Player_sounds;

    [SerializeField]
    AudioClip[] Player_snds_lib;

    // How long the player has to suffer
    [SerializeField]
    private float HurtDuration;

    [SerializeField]
    private float Knockback;

    [SerializeField]
    private float Blinking;

    [SerializeField]
    private float Immunity;

    private float BlinkingDuration;

    private float ImmunityDuration;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        playercolider = GetComponent<Collider2D>();
        playercolider.isTrigger = false;
        curHealth = maxHealth;
        Player_sounds = GetComponent<AudioSource>();
    }

    void Update()
	{
        var countOfExistingBullets = GameObject.FindGameObjectsWithTag("bullet").Length;

        if (Immunity > 0)
        {
            ImmunityDuration -= Time.deltaTime;

            BlinkingDuration -= Time.deltaTime;

            if (BlinkingDuration <= 0)
            {
                spriteRenderer.enabled = !spriteRenderer.enabled;

                BlinkingDuration = Blinking;
            }
            if (ImmunityDuration <= 0)
            {
                spriteRenderer.enabled = true;
            }
        }
        if (Input.GetKeyDown("space") && isGrounded && Time.timeScale == 1 && !isHurt)
        {
            rb2d.velocity = Vector2.up * jumpSpeed;
            animator.Play("player_jump");
        }
        if (Input.GetKeyDown("h"))
        {
            gameObject.GetComponent<PlayerPos>().LoadScene();
        }
        if (Input.GetKeyDown("k") && countOfExistingBullets < 5 && Time.timeScale == 1 && !isHurt)
        {
            if (isShooting)
                return;

            if (isGrounded && rb2d.velocity.x == 0 && !isHurt)
                animator.Play("Player_Shoot");
            if (isGrounded && rb2d.velocity.x != 0 && !isRunShooting && !isHurt)
            {
                animator.Play("Player_walk_&_shoot");
                isRunShooting = true;
                Invoke("SetBoolBack", runShootdelay);
            }
            if (!isGrounded && !isHurt)
                animator.Play("Player_shoot_air");

            isShooting = true;

            if (isShooting)
            {
                Player_sounds.clip = Player_snds_lib[1];
                Player_sounds.Play();
            }

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

        if (!wasGrounded && isGrounded)
        {
            Player_sounds.clip = Player_snds_lib[0];
            Player_sounds.Play();
        }

        if (isHurt)
        {
            animator.Play("player_hurt");
            if (faceLeft)
                rb2d.MovePosition(transform.position + transform.right * Knockback);
            else
                rb2d.MovePosition(transform.position - transform.right * Knockback);
            Invoke("ResetHurt", HurtDuration);
        }
        wasGrounded = isGrounded;
    }

    void ResetHurt()
    {
        isHurt = false;
        if (isGrounded && rb2d.velocity.x == 0)
            animator.Play("player_idle");
        if (isGrounded && rb2d.velocity.x != 0 && !isRunShooting)
            animator.Play("player_run");
        if (!isGrounded)
            animator.Play("player_jump");
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
            if (!isGrounded && !isShooting && !isHurt)
                animator.Play("player_jump");
        }

        if(Input.GetKey("d") || Input.GetKey("right"))
		{
            rb2d.velocity = new Vector2(runSpeed, rb2d.velocity.y);
            if (isGrounded && !isShooting && rb2d.velocity.x != 0 && !isRunShooting && !isHurt)
			{
                animator.Play("player_run");
			}
            transform.localScale = new Vector3(1, 1, 1);
            faceLeft = false;
        }
        else if(Input.GetKey("a") || Input.GetKey("left"))
		{
            rb2d.velocity = new Vector2(-runSpeed, rb2d.velocity.y);
            if (isGrounded && !isShooting && rb2d.velocity.x != 0 && !isRunShooting && !isHurt)
                animator.Play("player_run");
            transform.localScale = new Vector3(-1, 1, 1);
            faceLeft = true;
        }
        else
		{
            if (isGrounded && !isShooting && rb2d.velocity.x == 0 && !isHurt)
                animator.Play("player_idle");
            rb2d.velocity = new Vector2(0, rb2d.velocity.y);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Pick_up") || collision.CompareTag("Big Health"))
        {
            Destroy(collision.gameObject);
            Player_sounds.clip = Player_snds_lib[3];
            Player_sounds.Play();
            if (collision.CompareTag("Big Health"))
            {
                if (curHealth < maxHealth)
                    curHealth = curHealth + HealthBonus_big;
            }
            if (collision.CompareTag("Pick_up"))
            {
                if (curHealth < maxHealth)
                    curHealth = curHealth + HealthBonus_small;
            }
        }
        Physics2D.IgnoreLayerCollision(11, 12, true);
        Physics2D.IgnoreLayerCollision(11, 15, true);

    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 13)
        {
            HandleImmunity(EnemyScript.Contact_Damage);
            //curHealth -= EnemyScript.Contact_Damage;
        }
        if (collision.CompareTag("Enemy bullet"))
        {
            HandleImmunity(Enemy_bullet.damage);
        }
        if (collision.CompareTag("Boss_bullet"))
        {
            HandleImmunity(B_Bullet.damage);
        }
        if (collision.gameObject.layer == 16)
        {
            HandleImmunity(BossScript.Contact_Damage);
        }
    }
    private void HandleImmunity(int dmg)
    {
        isHurt = true;
        if (ImmunityDuration <= 0)
        {
            curHealth -= dmg;
            Player_sounds.clip = Player_snds_lib[2];
            Player_sounds.Play();
            if (curHealth <= 0)
            {
                gameObject.GetComponent<PlayerPos>().LoadScene();
            }
            else
            {
                ImmunityDuration = Immunity;
                spriteRenderer.enabled = false;
                BlinkingDuration = Blinking;
            }
        }
    }

}
