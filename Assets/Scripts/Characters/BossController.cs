using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : ICharacter {

    public delegate void BossDelegate();
    public BossDelegate onBossDeathDelegate;

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

        onBossDeathDelegate();
        GetComponent<Animator>().SetTrigger("Death");
        Invoke("onDeathAnimationComplete", 2.5f);
    }

    private void onDeathAnimationComplete()
    {
        gameObject.SetActive(false);
    }


}
