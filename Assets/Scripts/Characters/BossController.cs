using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : ICharacter {

    protected override void Awake()
    {
        base.Awake();
        healthBar.SetActive(false);
    }

    protected override void Update()
    {

    }


    public override void decrementHealth(float damage)
    {
        base.decrementHealth(damage);
        healthBar.SetActive(true);
    }

    protected override void onDeath()
    {
        GetComponent<Animator>().SetTrigger("Death");
        Invoke("onDeathAnimationComplete", 2.5f);
    }

    private void onDeathAnimationComplete()
    {
        gameObject.SetActive(false);
    }


}
