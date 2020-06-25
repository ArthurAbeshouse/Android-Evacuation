using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rb2d;

    [SerializeField]
    int health = 3;
   
    [SerializeField]
    public static int Contact_Damage = 4;

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

    [SerializeField]
    private float shootDelay = 0f;

    [SerializeField]
    private float animDelay = 3f;

    AudioSource Enemy_sounds;

    [SerializeField]
    AudioClip[] Enemy_snds_lib;

    private bool faceLeft;

    private bool isShooting;

    private bool awake;

    public bool protection;

    // Start is called before the first frame update
    void Start()
    {
        SR = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        matWhite = Resources.Load("WhiteFlash", typeof(Material)) as Material;
        matDefault = SR.material;
        protection = true;
        gameObject.SetActive(false);
        Enemy_sounds = GetComponent<AudioSource>();
    }

    public void Update()
    {
        float distToPlayer = Vector2.Distance(transform.position, player.position);

        if (distToPlayer < Wakeup)
        {
            if (isShooting)
                return;

            if (transform.position.x < player.position.x && !awake)
            {
                animator.Play("met_wake up");
                awake = true;
                En_bulletshootsnd();
                transform.localScale = new Vector2(-1, 1);
                faceLeft = false;
                protection = false;
                Invoke("ResetAnimation", animDelay);
            }
            if (transform.position.x > player.position.x && !awake)
            {
                animator.Play("met_wake up");
                awake = true;
                En_bulletshootsnd();
                transform.localScale = new Vector2(1, 1);
                faceLeft = true;
                protection = false;
                Invoke("ResetAnimation", animDelay);
            }
            isShooting = true;
            GameObject en_B = Instantiate(en_Bullet);
            GameObject en_B2 = Instantiate(en_Bullet);
            GameObject en_B3 = Instantiate(en_Bullet);
            en_B.transform.position = BulletSpawn.transform.position;
            en_B2.transform.position = BulletSpawn.transform.position;
            en_B3.transform.position = BulletSpawn.transform.position;
            en_B.GetComponent<Enemy_bullet>().StartShoot(faceLeft);
            en_B2.GetComponent<Enemy_bullet>().StartShoot2(faceLeft);
            en_B3.GetComponent<Enemy_bullet>().StartShoot3(faceLeft);
            Invoke("ResetShoot", shootDelay);
        }
        else
        {
            animator.Play("enemy_backtobed");
        }
    }

    void En_bulletshootsnd()
    {
        Enemy_sounds.clip = Enemy_snds_lib[0];
        Enemy_sounds.Play();
    }

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
                SR.material = matDefault;
            }
            else
            {
                Destroy(collision.gameObject);
                health -= Player_bullet.damage;
                SR.material = matWhite;
            }
            DamageHandler();
        }
    }

    void DamageHandler()
    {
        float rand = Random.value;
        if (health <= 0)
        {
            PlayExplosion();
            if (rand < 0.1f) // Items spawn 10% of the time
            {
                Instantiate(bigHealth, transform.position, Quaternion.identity);
            }
            else if (rand >= 0.1f && rand < 0.2f) // Items spawn 50% of the time
            {
                Instantiate(smallHealth, transform.position, Quaternion.identity);
            }
            else // Items spawn 30% of the time
            {
                // do nothing
            }
            Die();
        }
        else
        {
            Invoke("MaterialFlash", .05f);
        }
    }

    void MaterialFlash()
    {
        SR.material = matDefault;
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
