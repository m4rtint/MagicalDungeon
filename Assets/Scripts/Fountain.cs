using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fountain : MonoBehaviour {

    [SerializeField]
    float amountHealed = 30f;

    private void OnTriggerEnter(Collider other)
    {
        incrementHealthIfNeeded(other.gameObject);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        incrementHealthIfNeeded(collision.gameObject);
    }

    void incrementHealthIfNeeded(GameObject character)
    {
        if (character.CompareTag(Tags.PLAYER))
        {
            character.GetComponent<ICharacter>().incrementHealth(amountHealed);
        }
    }

}
