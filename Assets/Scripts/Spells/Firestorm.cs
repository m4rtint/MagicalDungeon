using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firestorm : MonoBehaviour, IPooledObject
{

    [SerializeField] float secondsDuration;
    [SerializeField] float damage;
    private float currentDuration;
    private string[] listOfObstacleTags = { Tags.ENEMY, Tags.SOLID_OBSTACLE };


    // Update is called once per frame
    void Update()
    {
        currentDuration += Time.fixedDeltaTime;
        if (currentDuration > secondsDuration)
        {
            gameObject.SetActive(false);
        }
    }


    public void OnObjectSpawn()
    {
        currentDuration = 0;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        GameObject hitTarget = col.gameObject;
        if (hitTarget.tag == Tags.ENEMY)
        {
            hitTarget.GetComponent<EnemyController>().decrementHealth(damage);
        }

    }

}
