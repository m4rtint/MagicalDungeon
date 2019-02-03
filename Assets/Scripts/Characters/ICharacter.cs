using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ICharacter : MonoBehaviour {

    [SerializeField]
    float healthPoints;
    [SerializeField]
    protected float moveSpeed;
    [SerializeField]
    float invulnerableTimer;
    bool isInvulnerable = false;

    [SerializeField]
    private GameObject healthBar;

    private float maxHealth;

    protected virtual void Awake()
    {
        maxHealth = healthPoints;
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    public virtual void decrementHealth(float damage)
    {
        healthPoints = Mathf.Clamp(healthPoints - damage, 0, maxHealth);
        if (isHealthZero())
        {
            onDeath();
        }
        updateHealthBar();
    }

    private void updateHealthBar()
    {
        healthBar.transform.localScale = new Vector3(healthPoints / maxHealth, 1, 1);
    }

    protected bool isHealthZero()
    {
        return healthPoints <= 0;
    }

    protected virtual void onDeath()
    {
        gameObject.SetActive(false);
    }

    public virtual void incrementHealth(float heal)
    {
        healthPoints = Mathf.Clamp(healthPoints + heal, 0, maxHealth);
        updateHealthBar();
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
