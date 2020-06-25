using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerPos : MonoBehaviour
{
    private PlayerManager pm;

    void Start()
    {
        pm = GameObject.Find("PlayerManager").GetComponent<PlayerManager>();
        transform.position = pm.lastPos;
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
