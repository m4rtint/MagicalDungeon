using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firestorm : ISpell, IPooledObject
{
    [SerializeField] float damageCoolDown = 0.25f;
    [SerializeField] float idleTimeToLive = 3f;
    bool isCoolingDown;
    float currentCoolDown = 0;

    protected override void Update()
    {
        base.Update();
        if (!isMoving)
        {
            currentTimeToLive += Time.fixedDeltaTime;
            if (currentTimeToLive > idleTimeToLive)
            {
                gameObject.SetActive(false);
            }
        }
        currentCoolDown += Time.fixedDeltaTime;
    }

    protected override void onMovementTimeToLiveStopped()
    {
        currentTimeToLive = 0;
        isMoving = false;
    }


    public void OnObjectSpawn()
    {
        Vector3 dir = transform.rotation.eulerAngles;
        angle = Utilities.getAngleDegBetween(dir.y, dir.x) + 90;
        currentTimeToLive = 0;
        isMoving = true;
    }

    protected override void OnTriggerEnter2D(Collider2D col)
    {
        base.OnTriggerEnter2D(col);
        damageEnemyIfNeeded(col.gameObject);
    }

    void OnTriggerStay2D(Collider2D col)
    {
        damageEnemyIfNeeded(col.gameObject);
    }

    void damageEnemyIfNeeded(GameObject colGameObj)
    {
        if (colGameObj.tag == Tags.ENEMY)
        {
            colGameObj.GetComponent<EnemyController>().decrementHealth(damage);
        }
    }

    protected override void onSpellHitObject()
    {
        onMovementTimeToLiveStopped();
    }
}
