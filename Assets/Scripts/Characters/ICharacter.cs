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
    [SerializeField]
    float invulnerableTimer;
    bool isInvulnerable = false;

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
        return healthPoints <= 0;
    }

    protected virtual void onDeath()
    {
        gameObject.SetActive(false);
    }

    public void incrementHealth(float heal)
    {
        healthPoints += heal;
    }

    public void damagedByAttacker(float damage)
    {
        if (!isInvulnerable)
        {
            isInvulnerable = true;
            decrementHealth(damage);
            Invoke("resetInvulnerable", invulnerableTimer);
        }
    }

    public void getKnockedBackSolid(float knockBackAmount, Vector3 attackPos)
    {
        if (!isInvulnerable)
        {
            Vector3 knockBack = attackPos - transform.position;
            GetComponent<Rigidbody2D>().AddForce(knockBack.normalized * -knockBackAmount);
        }
    }


    public void resetInvulnerable()
    {
        isInvulnerable = false;
    }
}
