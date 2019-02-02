using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firestorm : ISpell, IPooledObject
{
    [SerializeField] float damageCoolDown = 0.25f;
    bool isCoolingDown;
    float currentCoolDown = 0;

    protected override void Update()
    {
        base.Update();
        if (!isMoving)
        {
            currentTimeToLive += Time.fixedDeltaTime;
            if (currentTimeToLive > spellMovementTimeToLive)
            {
                gameObject.SetActive(false);
            }
        }
        currentCoolDown += Time.fixedDeltaTime;
    }

    protected override void onMovementTimeToLiveStopped()
    {
        Vector3 dir = transform.rotation.eulerAngles;
        angle = Utilities.getAngleDegBetween(dir.y, dir.x) + 90;
        currentTimeToLive = spellMovementTimeToLive * 2;
        isMoving = false;
    }


    public void OnObjectSpawn()
    {
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
        throw new System.NotImplementedException();
    }
}
