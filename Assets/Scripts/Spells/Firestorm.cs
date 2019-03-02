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
    const float fadeInTime = 0.5f;
    const float fadeOutTime = 0.1f;

    protected override void Update()
    {
        base.Update();
        if (!isMoving)
        {
            currentTimeToLive += Time.fixedDeltaTime;
            if (currentTimeToLive > idleTimeToLive - fadeOutTime)
            {
                endFireStorm();
            }
        }
        currentCoolDown += Time.fixedDeltaTime;
    }

    void endFireStorm()
    {
        Hashtable ht = new Hashtable();
        ht.Add("alpha", 0f);
        ht.Add("time", fadeOutTime);
        ht.Add("oncomplete", "onCompleteEndFireStormFade");
        iTween.FadeTo(gameObject, ht);
    }

    void starFireStorm()
    {
        Hashtable ht = new Hashtable();
        ht.Add("alpha", 1.0f);
        ht.Add("time", fadeInTime);
        iTween.FadeTo(gameObject, ht);
    }

    void onCompleteEndFireStormFade()
    {
        //AUDIO
        AudioManager.instance.StopFireStorm();
        gameObject.SetActive(false);
    }

    protected override void onMovementTimeToLiveStopped()
    {
        currentTimeToLive = 0;
        isMoving = false;
    }


    public void OnObjectSpawn()
    {
        starFireStorm();
        Vector3 dir = transform.rotation.eulerAngles;
        angle = Utilities.getAngleDegBetween(dir.y, dir.x);
        currentTimeToLive = 0;
        isMoving = true;
        AudioManager.instance.PlayFireStorm();
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
