using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitpoint : MonoBehaviour
{
    Animator animator;

    AudioSource Boss_sounds;

    [SerializeField]
    AudioClip[] Boss_snds_lib;

    void Start()
    {
        animator = GetComponentInParent<Animator>();
        Boss_sounds = GetComponent<AudioSource>();
    }
    void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("bullet"))
        {
            GetComponentInParent<BossScript>().isHurt = true;
            GetComponentInParent<BossScript>().health -= Player_bullet.damage;
            Boss_sounds.clip = Boss_snds_lib[0];
            Boss_sounds.Play();
            animator.Play("Boss_doc_hurt");
            Destroy(collision.gameObject);
            // damage = 0;
            // rb2d.velocity = new Vector2(-rb2d.velocity.x, Mathf.Abs(rb2d.velocity.x));
            //Debug.Log("it went through");
        }
        /* if (collision.CompareTag("Boss_hitpoint"))
         {
             BossScript.health -= damage;
             Destroy(gameObject);
         } */
    }
}
