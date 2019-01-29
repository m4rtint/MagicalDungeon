using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PolyNavAgent))]
public class EnemyController : ICharacter, IPooledObject {

    const string ANIMATION_DEATH = "Death";
    const string ANIMATION_DAMAGED = "Damaged";

    public delegate void EnemyDelegate();
    public EnemyDelegate onDeathDelegate;

    private PolyNavAgent agent;
    private GameObject player;

    [SerializeField]
    float agroRange = 4;
    
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
    }

    void setSpeed()
    {
        agent.maxSpeed = base.moveSpeed;
    }

    void Update()
    {
        gotoPlayerIfNeeded();
        updateSpriteDirection();
    }

    #region Motion
    private void gotoPlayerIfNeeded()
    {
        if (player == null)
        {
            agent.enabled = false;
        }
        else
        {
            if (Vector3.Distance(player.transform.position, transform.position) <= agroRange)
            {
                agent.SetDestination(player.transform.position);
            }
        }
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
            runAnimation(ANIMATION_DAMAGED);
        }
    }

    protected override void onDeath()
    {
        runAnimation(ANIMATION_DEATH);
        agent.enabled = false;
        Invoke("completeDeathAnimation", 1.5f);
    }

    void runAnimation(string name)
    {
        GetComponent<Animator>().SetTrigger(name);
    }

    public void completeDeathAnimation()
    {
        gameObject.SetActive(false);
        onDeathDelegate();
        onDeathDelegate = null;
    }

    void OnCollisionStay2D(Collision2D other)
    {
        GameObject player = other.gameObject;

        if (player.tag == Tags.PLAYER)
        {
            player.GetComponent<ICharacter>().getKnockedBackSolid(2000, transform.position);
            player.GetComponent<PlayerController>().damagedByAttacker(1);
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
