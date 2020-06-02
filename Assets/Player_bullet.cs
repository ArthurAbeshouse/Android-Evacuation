using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_bullet : MonoBehaviour
{
    [SerializeField]
    float speed;

    [SerializeField]
    int damage;

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
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

}
