using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour {

    public bool isStart;
    public bool isQuit;

    void OnMouseClick()
    {
        SceneManager.LoadScene("Main", LoadSceneMode.Single);
    }
}
