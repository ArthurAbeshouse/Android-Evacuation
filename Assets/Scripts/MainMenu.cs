using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class MainMenu : MonoBehaviour
{
    //  AudioSource TitleScreen;

    public GameObject PlayGame;

    public GameObject AboutGame;

    public void StartGame()
    {
        //TitleScreen = GetComponent<AudioSource>();
        //EventSystem.current.SetSelectedGameObject(null);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
