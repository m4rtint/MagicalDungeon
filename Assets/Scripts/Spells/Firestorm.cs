using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firestorm : ISpell, IPooledObject
{
    [SerializeField] float damageCoolDown = 0.25f;
    [SerializeField] float idleTimeToLive = 3f;
    bool isCoolingDown;
    float currentCoolDown = 0;

    //Animation
    float fadeTime = 0.5f;

    protected override void Update()
    {
        base.Update();
        if (!isMoving)
        {
            currentTimeToLive += Time.fixedDeltaTime;
            if (currentTimeToLive > idleTimeToLive - fadeTime)
            {
                endFireStorm();
            }
        }
        currentCoolDown += Time.fixedDeltaTime;
    }

    void endFireStorm()
    {
        Hashtable ht = new Hashtable();
        ht.Add("alpha", 0);
        ht.Add("time", fadeTime);
        ht.Add("oncomplete", "onCompleteEndFireStormFade");
        iTween.FadeTo(gameObject, ht);
    }

    void onCompleteEndFireStormFade()
    {
        gameObject.SetActive(false);
    }

    protected override void onMovementTimeToLiveStopped()
    {
        currentTimeToLive = 0;
        isMoving = false;
    }

    void resetAlpha()
    {
        GetComponent<SpriteRenderer>().color = Color.white;
    }


    public void OnObjectSpawn()
    {
        resetAlpha();
        Vector3 dir = transform.rotation.eulerAngles;
        angle = Utilities.getAngleDegBetween(dir.y, dir.x);
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
            colGameObj.GetComponent<ICharacter>().decrementHealth(damage);
        }
    }

    protected override void onSpellHitObject()
    {
        onMovementTimeToLiveStopped();
    }
}
