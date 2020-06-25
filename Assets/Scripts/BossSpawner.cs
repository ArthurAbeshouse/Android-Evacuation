using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject Boss;

    [SerializeField]
    Transform player;

    [SerializeField]
    float Wakeup;

    private bool awake;

    // Start is called before the first frame update
    void Start()
    {
     //   Boss = GetComponent<BossScript>();
    }

    // Update is called once per frame
    public void Update()
    {
        float distToPlayer = Vector2.Distance(transform.position, player.position);
        if (distToPlayer < Wakeup)
        {
            Boss.SetActive(true);
        }
    }
}
