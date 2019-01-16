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
    public bool invulnerable;

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

    public void damagedByEnemy(float damage)
    {
        if (!invulnerable)
        {
            invulnerable = true;
            decrementHealth(damage);
            Invoke("resetInvulnerable", 2);
        }
    }

    public void getKnockedBackSolid(Collision2D other, Rigidbody2D self, float knockBackAmount)
    {
        if (!invulnerable)
        {
            Vector2 knockBack = other.gameObject.transform.position - self.transform.position;
            self.AddForce(knockBack.normalized * -knockBackAmount);
        }
    }


    public void resetInvulnerable()
    {
        invulnerable = false;
    }
}
