using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceStorm : ISpell
{
    [SerializeField] float slowCoolDown = 2.5f;
    [SerializeField] float idleTimeToLive = 0.5f;
    [SerializeField] float slowDownPercentage = 0.25f;

    //Animation
    const float fadeOutTime = 0.5f;
    const float fadeInTime = 0.1f;

    private void Awake()
    {
        slowDownPercentage = Mathf.Max(0.0001f, slowDownPercentage);
    }

    protected override void Update()
    {
        base.Update();
        if (!isMoving)
        {
            currentTimeToLive += Time.fixedDeltaTime;
            if (currentTimeToLive > idleTimeToLive - fadeOutTime)
            {
                endIceStorm();
            }
        }
    }

    void endIceStorm()
    {
        Hashtable ht = new Hashtable();
        ht.Add("alpha", 0f);
        ht.Add("time", fadeOutTime);
        ht.Add("oncomplete", "onCompleteEndIceStormFade");
        iTween.FadeTo(gameObject, ht);
    }

    void startIceStorm()
    {
        Hashtable ht = new Hashtable();
        ht.Add("alpha", 1f);
        ht.Add("time", fadeInTime);
        iTween.FadeTo(gameObject, ht);
    }

    void onCompleteEndIceStormFade()
    {
        gameObject.SetActive(false);
        //AUDIO
        AudioManager.instance.StopSpells();
    }

    protected override void onMovementTimeToLiveStopped()
    {
        currentTimeToLive = 0;
        isMoving = false;
    }

    public void OnObjectSpawn()
    {
        startIceStorm();
        Vector3 dir = transform.rotation.eulerAngles;
        angle = Utilities.getAngleDegBetween(dir.y, dir.x);
        currentTimeToLive = 0;
        isMoving = true;
        //AUDIO
        AudioManager.instance.ActiveIceStorm();
    }

    protected override void OnTriggerEnter2D(Collider2D col)
    {
        base.OnTriggerEnter2D(col);
        damageEnemyIfNeeded(col.gameObject);
        slowEnemyIfNeeded(col.gameObject);
    }

    void slowEnemyIfNeeded(GameObject gObject)
    {
        if (gObject.tag == Tags.ENEMY)
        {
            gObject.GetComponent<ICharacter>().modifySpeed(slowDownPercentage, slowCoolDown);
        }
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
