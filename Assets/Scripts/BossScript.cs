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

    // Start is called before the first frame update
    void Start()
    {
        SR = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        Hitpoint Damage = GetComponent<Hitpoint>();
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isShooting)
            return;

        isShooting = true;

        //Shoot bullet
        GameObject bb = Instantiate(Boss_Bullet);
        bb.GetComponent<B_Bullet>();
        bb.transform.position = BulletSpawn.transform.position;

        Invoke("ResetShoot", shootDelay);

        if (isHurt)
            Invoke("ResetHurt", HurtDuration);

        if (health <= 0)
        {
            Destroy(gameObject);
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
            if (transform.position.x <= 7.772)
                isMovingRight = true;
        }
        else
        {
            rb2d.velocity = new Vector2(-AirSpeed, rb2d.velocity.y);
            if (transform.position.x >= 8.974)
                isMovingRight = false;
        }
    }

    void ResetShoot()
    {
        isShooting = false;
    }
}
