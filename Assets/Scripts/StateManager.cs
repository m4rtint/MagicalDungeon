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
    GameObject gameOver;

    [SerializeField]
    GameObject gameWon;

    bool allowReset = false;

    private void Awake()
    {
        gameOver.SetActive(false);
        gameWon.SetActive(false);
        player.GetComponent<ICharacter>().onCharacterDeath += didLose;
        boss.GetComponent<ICharacter>().onCharacterDeath += didWin;
    }

	
	// Update is called once per frame
	void Update () {
		if (Input.anyKeyDown && allowReset)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    void didLose()
    {
        ResetButtonAppear();
        Invoke("activateReset", 2.5f);
    }

    void didWin()
    {
        YouWonAppear();
        Invoke("activateReset", 2.5f);
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
}
