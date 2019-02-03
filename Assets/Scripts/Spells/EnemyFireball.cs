using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFireball : ISpell, IPooledObject
{

    void Awake()
    {
        base.listOfObstacleTags = new string[]{Tags.PLAYER, Tags.SOLID_OBSTACLE};
    }

    private void checkIfSpellHitObject(string objTag)
    {
        for (int i = 0; i < this.listOfObstacleTags.Length; i++)
        {
            if (objTag == this.listOfObstacleTags[i])
            {
                onSpellHitObject();
            }
        }
    }

    //Animation
    const string ANIMATION_EXPLOSION = "Explode";
    const float EXPLOSION_ANIMATION_TIME = 0.4f;

    protected override void onMovementTimeToLiveStopped()
    {
        gameObject.SetActive(false);
    }

    public void OnObjectSpawn()
    {
        Vector3 dir = transform.rotation.eulerAngles;
        angle = Utilities.getAngleDegBetween(dir.y, dir.x) + 90;
        base.currentTimeToLive = 0;
        isMoving = true;
    }

    protected override void OnTriggerEnter2D(Collider2D col)
    {
        GameObject hitTarget = col.gameObject;
        if (hitTarget.tag == Tags.PLAYER)
        {
            hitTarget.GetComponent<PlayerController>().decrementHealth(damage);
        }

        this.checkIfSpellHitObject(col.tag);
    }

    protected override void onSpellHitObject()
    {
        isMoving = false;
        GetComponent<Animator>().SetTrigger(ANIMATION_EXPLOSION);
        Invoke("onFinishExplosionAnimation", EXPLOSION_ANIMATION_TIME);
    }

    void onFinishExplosionAnimation()
    {
        gameObject.SetActive(false);
    }

}
