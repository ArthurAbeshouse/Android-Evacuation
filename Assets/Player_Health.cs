using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Health : MonoBehaviour
{
    public Sprite[] Healthbars;

    public Image HeartUI;

    private PlayerController2D player;

    void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController2D>();
    }

    void Update ()
    {
        HeartUI.sprite = Healthbars[player.curHealth];
    }
}
