using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : EnemyController {

    [SerializeField]
    private float attackRange = 6;
    private bool attackTriggered = false;

    [SerializeField]
    float attackTime = 2f;
    float currentAttackCoolDown = 0;

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        attackPlayerIfNeeded();
        if (currentAttackCoolDown > 0)
        {
            currentAttackCoolDown -= Time.deltaTime;
        }
    }

    #region Motion
    protected override void gotoPlayerIfNeeded()
    {
        if (isMovementNeeded())
        {
            attackTriggered = true;
            base.goToPlayer();
        }
    }

    bool isMovementNeeded()
    {
        float distance = Vector3.Distance(player.transform.position, transform.position);
        return distance <= this.attackRange && distance > this.agroRange;
    }

    #endregion

    #region Attack

    void attackPlayerIfNeeded()
    {
        // If player is within attack range 
        if (isAttackNeeded()) { 
            currentAttackCoolDown = attackTime;
            attackPlayer();
        }
    }

    bool isAttackNeeded()
    {
        return Vector3.Distance(player.transform.position, transform.position) <= this.attackRange && 
                    attackTriggered &&
                    currentAttackCoolDown <= 0 &&
                    !isHealthZero();
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

    #endregion

    #region PlayerDeath
    protected override void onPlayerDeath()
    {
        base.onPlayerDeath();
        attackTriggered = false;
    }
    #endregion


    #region Gizmo
    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, attackRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, this.agroRange);
    }
    #endregion
}

