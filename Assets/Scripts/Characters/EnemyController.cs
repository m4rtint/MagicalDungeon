using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PolyNavAgent))]
public class EnemyController : ICharacter, IPooledObject {

    public delegate void EnemyDelegate();
    public EnemyDelegate onDeathDelegate;

    private PolyNavAgent agent;

    
    protected override void Awake()
    {
        base.Awake();
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
        agent.SetDestination(playerPosition());
        updateSpriteDirection();
    }

    #region Motion
    private Vector3 playerPosition()
    {
        return GameObject.FindGameObjectWithTag(Tags.PLAYER).transform.position;
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
            runAnimation("Damaged");
        }
    }

    protected override void onDeath()
    {
        runAnimation("Death");
        agent.enabled = false;
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

    #endregion
}
