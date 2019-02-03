using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {

    [SerializeField]
    float amountHealed = 30f;

    private void OnTriggerEnter(Collider other)
    {
        incrementHealthIfNeeded(other.gameObject);
        Destroy(gameObject);
    }

    void incrementHealthIfNeeded(GameObject character)
    {
        if (character.CompareTag(Tags.PLAYER))
        {
            character.GetComponent<ICharacter>().incrementHealth(amountHealed);
        }
    }

}
