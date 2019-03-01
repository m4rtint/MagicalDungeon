using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceStorm : ISpell
{
    [SerializeField] float slowCoolDown = 2.5f;
    [SerializeField] float idleTimeToLive = 0.5f;
    [SerializeField] float slowDownPercentage = 0.25f;

    //Animation
    float fadeTime = 0.5f;

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
            if (currentTimeToLive > idleTimeToLive - fadeTime)
            {
                endIceStorm();
            }
        }
    }

    void endIceStorm()
    {
        Hashtable ht = new Hashtable();
        ht.Add("alpha", 0);
        ht.Add("time", fadeTime);
        ht.Add("oncomplete", "onCompleteEndIceStormFade");
        iTween.FadeTo(gameObject, ht);
    }

    void onCompleteEndIceStormFade()
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
