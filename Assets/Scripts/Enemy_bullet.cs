using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_bullet : MonoBehaviour
{
    [SerializeField]
    float speed;

    [SerializeField]
    float direction;

    [SerializeField]
    public static int damage = 2;

    public void StartShoot(bool faceLeft)
    {
        Rigidbody2D rb2d = GetComponent<Rigidbody2D>();
        if (faceLeft)
        {
            rb2d.velocity = new Vector2(-speed, direction);
        }
        else
        {
            rb2d.velocity = new Vector2(speed, direction);
        }
    }
    public void StartShoot2(bool faceLeft)
    {
        Rigidbody2D rb2d = GetComponent<Rigidbody2D>();
        if (faceLeft)
        {
            rb2d.velocity = new Vector2(-speed + (-0.13f), 0);
        }
        else
        {
            rb2d.velocity = new Vector2(speed + 0.13f, 0);
        }
    }
    public void StartShoot3(bool faceLeft)
    {
        Rigidbody2D rb2d = GetComponent<Rigidbody2D>();
        if (faceLeft)
        {
            rb2d.velocity = new Vector2(-speed, -direction);
        }
        else
        {
            rb2d.velocity = new Vector2(speed, -direction);
        }
    }
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
