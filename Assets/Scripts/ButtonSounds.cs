using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonSounds : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public AudioSource btnsnds;

    public AudioClip hoverfx;

    public AudioClip clickfx;

    public bool isButtonPressed;

    public void HoverSound()
    {
        btnsnds.PlayOneShot(hoverfx);
    }

    public void ClickSound()
    {
        btnsnds.PlayOneShot(clickfx);
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        isButtonPressed = true;
        if (isButtonPressed)
        {
            EventSystem.current.SetSelectedGameObject(null);
        }
        else
        {
            EventSystem.current.SetSelectedGameObject(gameObject);
        }
        //  EventSystem.current.SetSelectedGameObject(null);
        //   EventSystem.current.SetSelectedGameObject(AboutGame);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isButtonPressed = false;
        //  EventSystem.current.SetSelectedGameObject(null);
        //   EventSystem.current.SetSelectedGameObject(PlayGame);
    }
}
