using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : ISpell, IPooledObject
{

    //[SerializeField] float velocity;
    //[SerializeField] float secondsDuration;
    //[SerializeField] float damage;
    //private float angle;
    //private float currentDuration;
    //private string[] listOfObstacleTags = { Tags.ENEMY, Tags.SOLID_OBSTACLE };

    //private bool isMoving = true;

    //Animation
    const string ANIMATION_EXPLOSION = "Explode";
    const float EXPLOSION_ANIMATION_TIME = 0.4f;

    // Update is called once per frame
    //void Update()
    //{
    //    if (isMoving) { 
    //        transform.Translate(VectorFromAngle(angle) * velocity);
    //        currentDuration += Time.fixedDeltaTime;
    //        if (currentDuration > secondsDuration)
    //        {
    //            gameObject.SetActive(false);
    //        }
    //    }
    //}

    protected override void onMovementTimeToLiveStopped()
    {
        gameObject.SetActive(false);
    }


    //Vector2 VectorFromAngle(float theta)
    //{
    //    return new Vector2(Mathf.Cos(theta), Mathf.Sin(theta)); 
    //}

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
        if (hitTarget.tag == Tags.ENEMY)
        {
            hitTarget.GetComponent<EnemyController>().decrementHealth(damage);
        }

        base.OnTriggerEnter2D(col);
    }

    //void deactivateObjectIfNeeded(string tag)
    //{
    //    for(int i = 0; i < listOfObstacleTags.Length; i++)
    //    {
    //        if (tag == listOfObstacleTags[i])
    //        {
    //            isMoving = false;
    //            GetComponent<Animator>().SetTrigger(ANIMATION_EXPLOSION);
    //        }
    //    }
    //}

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
