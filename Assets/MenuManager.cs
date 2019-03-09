using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

    [SerializeField]
    GameObject fade;

    [SerializeField]
    AudioSource backgroundMusic;

    public static MenuManager instance = null;
    const float fadeTime = 2f;

    private void Awake()
    {
        instance = this;
    }

    public void fadeOut()
    {
        Hashtable var = new Hashtable();
        var.Add("easetype", "easeInQuart");
        var.Add("scale", new Vector3(100f, 100f));
        var.Add("time", fadeTime);
        var.Add("oncompletetarget", gameObject);
        var.Add("oncomplete", "loadNextScene");

        iTween.ScaleTo(fade, var);



        Hashtable sound = new Hashtable();
        sound.Add("time", fadeTime);
        sound.Add("audiosource", backgroundMusic);
        sound.Add("volume", 0);
        iTween.AudioTo(gameObject, sound);
    }


    void loadNextScene()
    {
        SceneManager.LoadScene("Main", LoadSceneMode.Single);
    }

}
