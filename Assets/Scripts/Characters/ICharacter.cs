using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class ICharacter : MonoBehaviour {

    [SerializeField]
    float healthPoints;
    [SerializeField]
    protected float moveSpeed;

    protected virtual void Awake()
    {
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    public virtual void decrementHealth(float damage)
    {
        healthPoints = Mathf.Max(0, healthPoints - damage);
        if (isHealthZero())
        {
            onDeath();
        }
    }

    protected bool isHealthZero()
    {
        return healthPoints == 0;
    }

    protected virtual void onDeath()
    {
        gameObject.SetActive(false);
    }

    public void incrementHealth(float heal)
    {
        healthPoints += heal;
    }

}
