using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : EnemyController {

    private float attackRange = 15;
    private bool attackTriggered = false;

    [SerializeField]
    float attackTime = 2f;
    float currentTime = 0;

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        attackPlayerIfNeeded();
        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
        }
    }

    #region Motion
    protected override void gotoPlayerIfNeeded()
    {
        if (player == null)
        {
            agent.enabled = false;
        }
        else
        {
            if (Vector3.Distance(player.transform.position, transform.position) <= this.agroRange)
            {
                goToPlayer();
                attackTriggered = true;
            }
        }
    }

    protected override void goToPlayer()
    {
        // Instead of going straight to player, go to fixed distance from the player and shoot fireballs at them
        agent.SetDestination((player.transform.position - transform.position)/2);
    }
    #endregion

    void attackPlayerIfNeeded()
    {
        if (player == null)
        {
            agent.enabled = false;
        }
        else
        {
            // If player is within attack range 
            if (Vector3.Distance(player.transform.position, transform.position) <= this.attackRange && attackTriggered)
            {
                if (currentTime <= 0)
                {
                    currentTime = attackTime;
                    attackPlayer();
                }
            }
        }
    }

    void attackPlayer()
    {
        GameObject fireball = ObjectPooler.Instance.SpawnFromPool(Pool.ENEMY_FIREBALL, transform.position, getEnemyRotation());
        fireball.GetComponent<EnemyFireball>().OnObjectSpawn();
    }

    Quaternion getEnemyRotation()
    {
        //Convert the enemy position to Screen coordinates
        Vector3 position = transform.position - player.transform.position;
        float angle = Utilities.getAngleDegBetween(position.y, position.x) + 180;
        return Quaternion.AngleAxis(angle, Vector3.forward);
    }

}

