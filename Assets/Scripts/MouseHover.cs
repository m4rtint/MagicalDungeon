using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MouseHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler {

    Text myText;

    void Start()
    {
        myText = GetComponent<Text>();
    }

    public void OnPointerEnter (PointerEventData eventData)
    {
        myText.color = Color.white;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        myText.color = Color.black;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        SceneManager.LoadScene("Main", LoadSceneMode.Single);
    }
}
