using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class ConeSpell : MonoBehaviour {

    [SerializeField]
    private float damage;

    [SerializeField]
    private float perTime;
    [SerializeField]
    private float timeToLive;

    float lifeTimer = 0;
    bool canDamage = true;

    private void Awake()
    {
        GetComponent<BoxCollider2D>().isTrigger = true;
    }

    private void Update()
    {
        lifeTimer += Time.deltaTime;
        if (lifeTimer > timeToLive)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        lifeTimer = 0;
        resetCanDamage();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == Tags.ENEMY && canDamage)
        {
            canDamage = false;
            Invoke("resetCanDamage", perTime);
            collision.gameObject.GetComponent<ICharacter>().decrementHealth(damage);
        }
    }

    private void resetCanDamage()
    {
        canDamage = true;
    }

}
