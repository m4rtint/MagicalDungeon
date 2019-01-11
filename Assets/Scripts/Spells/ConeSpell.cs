using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class ConeSpell : MonoBehaviour {

    [SerializeField]
    private float damage;

    [SerializeField]
    private float damagePerSecond;

    bool canDamage = true;

    private void Awake()
    {
        GetComponent<BoxCollider2D>().isTrigger = true;
    }

    private void OnEnable()
    {
        resetCanDamage();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == Tags.ENEMY && canDamage)
        {
            canDamage = false;
            Invoke("resetCanDamage", damagePerSecond);
            //TODO - Do something with enemy - decrement health or something
        }
    }

    private void resetCanDamage()
    {
        canDamage = true;
    }

}
