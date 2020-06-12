using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rb2d;
    //Rigidbody2D rb2d_B;
    private int health = 3;
  //  public float range;

    [SerializeField]
    GameObject en_Bullet;

    [SerializeField]
    Transform BulletSpawn;

    //materials
    private Material matWhite;
    private Material matDefault;

    SpriteRenderer SR;

    [SerializeField]
    Transform player;

    [SerializeField]
    float Wakeup;

    [SerializeField]
    GameObject Explode;

    [SerializeField]
    GameObject smallHealth;

    [SerializeField]
    GameObject bigHealth;

   /* [SerializeField]
    GameObject Player_b;*/

    [SerializeField]
    private float shootDelay = 0f;

    [SerializeField]
    private float animDelay = 3f;

    private bool faceLeft;

    private bool isShooting;

    private bool awake;

    private bool protection;

    // Start is called before the first frame update
    void Start()
    {
        SR = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
    //    rb2d_B = en_Bullet.GetComponent<Rigidbody2D>();
        matWhite = Resources.Load("WhiteFlash", typeof(Material)) as Material;
        matDefault = SR.material;
        protection = true;
       // rot = transform.localEulerAngles;
    }

    void Update()
    {
        //distance to player
        float distToPlayer = Vector2.Distance(transform.position, player.position);

        if (distToPlayer < Wakeup)
        {
            if (isShooting)
                return;

            if (transform.position.x < player.position.x && !awake)
            {
                animator.Play("met_wake up");
                awake = true;
                transform.localScale = new Vector2(-1, 1);
                faceLeft = false;
                protection = false;
                Invoke("ResetAnimation", animDelay);
            }
            if (transform.position.x > player.position.x && !awake)
            {
                animator.Play("met_wake up");
                awake = true;
                transform.localScale = new Vector2(1, 1);
                faceLeft = true;
                protection = false;
                Invoke("ResetAnimation", animDelay);
            }
            //Invoke("ResetAnimation", animDelay);
            isShooting = true;
            //enemy_awake();
            GameObject en_B = Instantiate(en_Bullet);
            GameObject en_B2 = Instantiate(en_Bullet);
            GameObject en_B3 = Instantiate(en_Bullet);
            en_B.transform.position = BulletSpawn.transform.position;
            en_B2.transform.position = BulletSpawn.transform.position;
            en_B3.transform.position = BulletSpawn.transform.position;
            en_B.GetComponent<Enemy_bullet>().StartShoot(faceLeft);
            en_B2.GetComponent<Enemy_bullet>().StartShoot2(faceLeft);
            en_B3.GetComponent<Enemy_bullet>().StartShoot3(faceLeft);
            // rb2d_B.AddForce(Quaternion.AngleAxis(direction, Vector2));
            Invoke("ResetShoot", shootDelay);
        }
        else
        {
            //enemy_sleep();
            animator.Play("enemy_backtobed");
            //awake = false;
        }
    }

    /*public void Harm(Player_bullet player_b)
    {
        if (protection)
        {
            player_b.Deflect();
            return;
        }
    }*/

    void ResetShoot()
    {
        isShooting = false;
    }

    void ResetAnimation()
    {
        awake = false;
        animator.Play("enemy_backtobed");
        protection = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("bullet"))
        {
           // Destroy(collision.gameObject);
            if (protection)
            {
                health += 0;
                SR.material = matDefault;
                //Player_b.GetComponent<Player_bullet>().Deflect(protection);
                //Harm(player_b);
            }
            else
            {
                Destroy(collision.gameObject);
                health--;
                SR.material = matWhite;
            }
            float rand = Random.value;
         //   SR.material = matWhite;
           // SR.enabled = false;

            if (health <= 0)
            {
                PlayExplosion();
                 if (rand < 0.1f) // Items spawn 10% of the time
                {
                    Instantiate(bigHealth, transform.position, Quaternion.identity);
                    
                    bigHealth.transform.position = transform.position;
                } else if (rand >= 0.1f && rand < 0.2f) // Items spawn 50% of the time
                {
                    Instantiate(smallHealth, transform.position, Quaternion.identity);

                    smallHealth.transform.position = transform.position;
                }
                else // Items spawn 30% of the time
                {

                }
                Die();
            }
            else
            {
                Invoke("MaterialFlash", .05f);
            }
        }
    }

    void MaterialFlash()
    {
        SR.material = matDefault;
       // SR.enabled = true;
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    void PlayExplosion()
    {
        GameObject explosion = (GameObject)Instantiate(Explode);

        //set the position of the explosion
        explosion.transform.position = transform.position;
    }
}
