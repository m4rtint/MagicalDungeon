using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StateManager : MonoBehaviour {

    [SerializeField]
    GameObject player;

    [SerializeField]
    public GameObject gameOver;

    private bool playerDead;

    private void Awake()
    {
        playerDead = false;
        gameOver.SetActive(false);
        player.GetComponent<PlayerSpellController>().onDeathDelegate += didLose;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.anyKeyDown && playerDead)
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

    }

    private void ResetButtonAppear()
    {
        gameOver.SetActive(true);
    }
}
