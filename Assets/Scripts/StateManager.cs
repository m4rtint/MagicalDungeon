using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StateManager : MonoBehaviour {

    [SerializeField]
    GameObject player;

    [SerializeField]
    GameObject boss;

    [SerializeField]
    public GameObject gameOver;

    [SerializeField]
    public GameObject gameWon;

    private bool playerDead;
    private bool playerWon;

    private void Awake()
    {
        playerDead = false;
        playerWon = false;
        gameOver.SetActive(false);
        gameWon.SetActive(false);
        player.GetComponent<ICharacter>().onCharacterDeath += didLose;
        boss.GetComponent<ICharacter>().onCharacterDeath += didWin;
    }

	
	// Update is called once per frame
	void Update () {
		if (Input.anyKeyDown && playerDead)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        if (Input.anyKeyDown && playerWon)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    void didLose()
    {
        playerDead = true;
        ResetButtonAppear();
    }

    void didWin()
    {
        playerWon = true;
        YouWonAppear();
    }

    private void ResetButtonAppear()
    {
        gameOver.SetActive(true);
    }

    private void YouWonAppear()
    {
        gameWon.SetActive(true);
    }
}
