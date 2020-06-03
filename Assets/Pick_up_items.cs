using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pick_up_items : MonoBehaviour
{

    private PlayerController2D player;

    public int HealthBonus_small = 2;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController2D>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(player.curHealth < player.maxHealth)
            player.curHealth = player.curHealth + HealthBonus_small;
    }

    //public string itemType;

}
