using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PolyNavAgent))]
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
        updateSpriteDirection();
    }

    void updateSpriteDirection()
    {
        if (GetComponent<PolyNavAgent>().movingDirection.x > 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        } else
        {
            transform.localScale = Vector3.one;
        }
    }

    public override void decrementHealth(float damage)
    {
        base.decrementHealth(damage);
        if (isHealthZero())
        {
            onDeath();
        } else
        {
            runAnimation("Damaged");
        }
    }

    void onDeath()
    {
        runAnimation("Death");
        GetComponent<PolyNavAgent>().enabled = false;
    }

    void runAnimation(string name)
    {
        GetComponent<Animator>().SetTrigger(name);
    }

    public void completeDeathAnimation()
    {
        gameObject.SetActive(false);
    }
}
