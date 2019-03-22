using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PolyNavAgent))]
public class EnemyController : ICharacter, IPooledObject {

    const string ANIMATION_DEATH = "Death";
    const string ANIMATION_DAMAGED = "Damaged";

    private Color frozenColor = new Color(0, 255, 255, 255);

    protected PolyNavAgent agent;
    protected GameObject player;

    [SerializeField]
    protected float meleeDamage = 10;

    [SerializeField]
    protected float agroRange = 4;

    [SerializeField]
    MONSTERS monster = MONSTERS.MUSHROOM;
    
    protected override void Awake()
    {
        base.Awake();
        player = GameObject.FindGameObjectWithTag(Tags.PLAYER);
        agent = GetComponent<PolyNavAgent>();
        setSpeed();
    }

    public void OnObjectSpawn()
    {
        setSpeed();
        agent.enabled = true;
        GetComponent<Collider2D>().enabled = true;
    }

    protected virtual void FixedUpdate()
    {
        gotoPlayerIfNeeded();
        updateSpriteDirection();
        ifPlayerDeath();
    }

    #region Motion
    void setSpeed()
    {
        agent.maxSpeed = base.moveSpeed * base.speedModifier;
    }

    public override void modifySpeed(float mod, float time)
    {
        base.modifySpeed(mod, time);
        setSpeed();
        GetComponent<SpriteRenderer>().color = frozenColor;
    }

    protected override void resetSpeedCoolDown()
    {
        base.resetSpeedCoolDown();
        GetComponent<SpriteRenderer>().color = Color.white;
        setSpeed();
    }


    protected virtual void gotoPlayerIfNeeded()
    {
        if (Vector3.Distance(player.transform.position, transform.position) <= this.agroRange)
        {
            goToPlayer();
        }
    }

    protected virtual void goToPlayer()
    {
        agent.SetDestination(player.transform.position);
    }

    void updateSpriteDirection()
    {
        if (agent.movingDirection.x > 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        } else
        {
            transform.localScale = Vector3.one;
        }
    }
    #endregion

    #region LifeDeath
    public override void decrementHealth(float damage)
    {
        base.decrementHealth(damage);
        if (!isHealthZero())
        {
            goToPlayer();
            runAnimation(ANIMATION_DAMAGED);
            //AUDIO
            playHurtAudio();
        }
    }

    void ifPlayerDeath()
    {
        if (player == null)
        {
            onPlayerDeath();
        }
    }

    protected virtual void onPlayerDeath()
    {
        stopMovement();
    }

    protected override void onDeath()
    {
        runAnimation(ANIMATION_DEATH);
        GetComponent<Collider2D>().enabled = false;
        stopMovement();
        playDeathAudio();
        Invoke("completeDeathAnimation", 1.2f);
    }

    void stopMovement()
    {
        agent.enabled = false;
    }

    void runAnimation(string name)
    {
        GetComponent<Animator>().SetTrigger(name);
    }

    public void completeDeathAnimation()
    {
        gameObject.SetActive(false);
        if (onCharacterDeath != null) {
            onCharacterDeath();
            //AUDIO
            playDeathAudio();
        }
        onCharacterDeath = null;
    }

    void OnCollisionStay2D(Collision2D other)
    {
        GameObject player = other.gameObject;

        if (player.tag == Tags.PLAYER && !isHealthZero())
        {
            player.GetComponent<ICharacter>().damagedByAttacker(meleeDamage);
        }
    }

    #endregion

    #region Audio
    private void playHurtAudio()
    {
        switch (monster)
        {
            case MONSTERS.MUSHROOM:
                AudioManager.instance.PlayMushroomHurt();
                break;
            case MONSTERS.ZOMBIE:
                AudioManager.instance.PlayZombieHurt();
                break;
            default:
                return;
        }
    }

    private void playDeathAudio()
    {
        switch(monster)
        {
            case MONSTERS.MUSHROOM:
                AudioManager.instance.PlayMushroomDeath();
                break;
            case MONSTERS.ZOMBIE:
                AudioManager.instance.PlayZombieDeath();
                break;
            default:
                return;
        }
    }

    #endregion

    #region Gizmo
    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, agroRange);
    }
    #endregion
}
