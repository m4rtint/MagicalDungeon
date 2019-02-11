using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ICharacter : MonoBehaviour {

    public delegate void CharacterDelegate();
    public CharacterDelegate onCharacterDeath;

    [Header("Movement")]
    [SerializeField]
    protected float moveSpeed;

    private float coolDownTime = 0;
    protected float speedModifier = 1;
    private bool isCoolingDown = false;

    [SerializeField]
    float invulnerableTimer;
    bool isInvulnerable = false;


    [Header("Health")]
    [SerializeField]
    float healthPoints;
    [SerializeField]
    public GameObject healthBar;

    private float maxHealth;

    #region Getter
    public float MoveSpeed()
    {
        return moveSpeed;
    }

    public float SpeedModifier()
    {
        return speedModifier;
    }
    #endregion

    #region Mono
    protected virtual void Awake()
    {
        maxHealth = healthPoints;
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    protected virtual void Update()
    {
        coolDownMovement();
    }

    #endregion

    #region Movement

    void coolDownMovement()
    {
        if (isCoolingDown)
        {
            coolDownTime -= Time.fixedDeltaTime;
            if (coolDownTime < 0)
            {
                resetSpeedCoolDown();
            }
        }
    }

    public virtual void modifySpeed(float mod, float time)
    {
        isCoolingDown = true;
        coolDownTime = time;
        speedModifier = mod;
    }

    protected virtual void resetSpeedCoolDown()
    {
        isCoolingDown = false;
        speedModifier = 1;
    }
    #endregion

    #region Health
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
        onCharacterDeath();
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

    #endregion
}
