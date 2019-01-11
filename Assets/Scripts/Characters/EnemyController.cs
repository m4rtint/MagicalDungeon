using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : ICharacter, IPooledObject {

    public void OnObjectSpawn()
    {
        //TODO
        Debug.Log("Need to setup Enemy on Spawn");
    }


    void Update()
    {
        Vector2 playerPosition = GameObject.FindGameObjectWithTag(Tags.PLAYER).transform.position;
        GetComponent<PolyNavAgent>().SetDestination(playerPosition);
    }
}
