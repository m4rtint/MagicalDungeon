using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MouseHover : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler {


    [SerializeField]
    AudioClip hover;
    [SerializeField]
    AudioClip click;

    Text myText;
    AudioSource audioSource;
    bool gameStart = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        myText = GetComponent<Text>();
    }

    public void OnPointerEnter (PointerEventData eventData)
    {
        if (!gameStart) { 
            playSound(hover);
        }
    }
    
    public void OnPointerDown(PointerEventData eventData)
    {
        playSound(click);
        MenuManager.instance.fadeOut();
        gameStart = true;
    }



    void playSound(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }
}
