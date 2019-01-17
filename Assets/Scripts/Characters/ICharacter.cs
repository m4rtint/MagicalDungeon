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
    private GameObject healthBar;

    private float maxHealth;

    protected virtual void Awake()
    {
        maxHealth = healthPoints;
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        decrementHealth(50);
    }

    public virtual void decrementHealth(float damage)
    {
        healthPoints = Mathf.Max(0, healthPoints - damage);
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
        return healthPoints == 0;
    }

    protected virtual void onDeath()
    {
        gameObject.SetActive(false);
    }

    public void incrementHealth(float heal)
    {
        healthPoints += heal;
        updateHealthBar();
    }

}
