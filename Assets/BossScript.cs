using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rb2d;
    SpriteRenderer SR;

    Player_bullet bool_true;

    [SerializeField]
    public int health;

    [SerializeField]
    public static int Contact_Damage = 8;

    [SerializeField]
    Transform BulletSpawn;

    [SerializeField]
    GameObject Boss_Bullet;

    [SerializeField]
    private float shootDelay;

    [SerializeField]
    private float AirSpeed;

    [SerializeField]
    private float Distance;

   // public GameObject P_bullet;

    private bool isMovingRight;

    private bool isShooting;

    public bool isHurt;

    [SerializeField]
    private float HurtDuration;

    [SerializeField]
    private float Blinking;

    [SerializeField]
    private float Immunity;

    private float BlinkingDuration;

    private float ImmunityDuration;

    // Start is called before the first frame update
    void Start()
    {
        SR = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        Hitpoint Damage = GetComponent<Hitpoint>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(health);

        //isHurt = gameObject.GetComponent<Player_bullet>();

        /* if (Immunity > 0)
         {
             ImmunityDuration -= Time.deltaTime;

             BlinkingDuration -= Time.deltaTime;

             if (BlinkingDuration <= 0)
             {
                 SR.enabled = !SR.enabled;

                 BlinkingDuration = Blinking;
             }
             if (ImmunityDuration <= 0)
             {
                 spriteRenderer.enabled = true;
             }
         }*/
      //  Debug.Log(isMovingRight);
       // Debug.Log(transform.position.x);

        if (isShooting)
            return;

        isShooting = true;

        //Shoot bullet
        GameObject bb = Instantiate(Boss_Bullet);
        bb.GetComponent<B_Bullet>();
        bb.transform.position = BulletSpawn.transform.position;

        Invoke("ResetShoot", shootDelay);

        if (isHurt)
        {
            animator.Play("Boss_doc_hurt");
            /*      if (faceLeft)
                      rb2d.MovePosition(transform.position + transform.right * Knockback);
                  else
                      rb2d.MovePosition(transform.position - transform.right * Knockback); */
            Invoke("ResetHurt", HurtDuration);
        }

        if (health <= 0)
        {
            gameObject.SetActive(false);
            // Dead();
        }
    }

    void ResetHurt()
    {
        isHurt = false;
        animator.Play("Boss_doc");
    }

    void FixedUpdate()
    {
        if (!isMovingRight)
        {
            rb2d.velocity = new Vector2(AirSpeed, rb2d.velocity.y);
            if (transform.position.x <= -3)
                isMovingRight = true;
        }
        else
        {
            rb2d.velocity = new Vector2(-AirSpeed, rb2d.velocity.y);
            if (transform.position.x >= -1)
                isMovingRight = false;
        }
    }

    void ResetShoot()
    {
        isShooting = false;
    }

 /*   private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("bullet"))
        {
            Destroy(collision.gameObject);
    /*        // Destroy(collision.gameObject);
            if (protection)
            {
                // health += 0;
                // Player_bul = collision.CompareTag("bullet").GetComponent<Player_bullet>().Deflect(protection);
                //other.gameObject.GetComponent<Player_bullet>.Deflect();
                SR.material = matDefault;

                //Player_b.GetComponent<Player_bullet>().Deflect(protection);
            }
            else
            {
                Destroy(collision.gameObject);
                health -= Player_bullet.damage;
                //DamageinTake();
                //health--;
                SR.material = matWhite;
            }
        }
    } */
}
