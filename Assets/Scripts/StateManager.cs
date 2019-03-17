using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StateManager : MonoBehaviour {

    enum GameState
    {
        InGame,
        Win,
        Lose
    }

    [SerializeField]
    GameObject player;

    [SerializeField]
    GameObject boss;

    [SerializeField]
    GameObject gameOver;

    [SerializeField]
    GameObject gameWon;

    [SerializeField]
    GameObject fade;

    GameState currentState = GameState.InGame;

    bool allowReset = false;

    private void Awake()
    {
        gameOver.SetActive(false);
        gameWon.SetActive(false);
        player.GetComponent<ICharacter>().onCharacterDeath += didLose;
        boss.GetComponent<ICharacter>().onCharacterDeath += didWin;
        fadeIn();
    }

	
	// Update is called once per frame
	void Update () {
		if (Input.anyKeyDown && allowReset)
        {
            fadeOut();
        }
    }

    void loadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void didLose()
    {
        if (currentState == GameState.InGame)
        {
            currentState = GameState.Lose;
            ResetButtonAppear();
            Invoke("activateReset", 2.5f);
        }
    }

    void didWin()
    {
        if (currentState == GameState.InGame)
        {
            currentState = GameState.Win;
            YouWonAppear();
            Invoke("activateReset", 2.5f);
        }
    }

    void activateReset()
    {
        allowReset = true;
    }
    
    private void ResetButtonAppear()
    {
        gameOver.SetActive(true);
    }

    private void YouWonAppear()
    {
        gameWon.SetActive(true);
    }


    private void fadeOut()
    {
        Hashtable var = new Hashtable();
        var.Add("easetype", "easeInQuart");
        var.Add("scale", new Vector3(100f, 100f));
        var.Add("time", 2);
        var.Add("oncompletetarget", gameObject);
        var.Add("oncomplete", "loadScene");

        iTween.ScaleTo(fade, var);
    }

    private void fadeIn()
    {
        fade.transform.localScale = new Vector3(100f, 100f);
        Hashtable var = new Hashtable();
        var.Add("easetype", "easeInOutQuint");
        var.Add("scale", Vector3.zero);
        var.Add("time", 2);

        iTween.ScaleTo(fade, var);
    }
}
