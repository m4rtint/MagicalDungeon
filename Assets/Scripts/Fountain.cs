using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fountain : MonoBehaviour {

    [SerializeField]
    float amountHealed = 30f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        incrementHealthIfNeeded(other.gameObject);
        playAudioIfNeeded(other.gameObject);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        incrementHealthIfNeeded(collision.gameObject);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(Tags.PLAYER))
        {
            AudioManager.instance.StopHealPlayer();
        }
    }

    void playAudioIfNeeded(GameObject player)
    {
        if (player.CompareTag(Tags.PLAYER))
        {
            AudioManager.instance.PlayHealPlayer();
        }
    }

    void incrementHealthIfNeeded(GameObject character)
    {
        if (character.CompareTag(Tags.PLAYER))
        {
            character.GetComponent<ICharacter>().incrementHealth(amountHealed);
        }
    }
}
