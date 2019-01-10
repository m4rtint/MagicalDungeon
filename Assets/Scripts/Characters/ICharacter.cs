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

    protected void Awake()
    {
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    public void decrementHealth(float damage)
    {
        healthPoints -= damage;
        if (healthPoints < 0)
        {
            healthPoints = 0;
        }
    }

    public void incrementHealth(float heal)
    {
        healthPoints += heal;
    }
	
}
