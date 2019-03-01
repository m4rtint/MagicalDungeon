using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Totem : ICharacter {

    protected override void Awake()
    {
        base.Awake();
        gameObject.SetActive(true);
        healthBar.SetActive(false);
    }

    protected override void Update()
    {
        if (isHealthZero())
        {
            onDeath();
        }
    }

    public override void decrementHealth(float damage)
    {
        base.decrementHealth(damage);
        healthBar.SetActive(true);
    }

    protected override void onDeath()
    {
        gameObject.SetActive(false);
    }


}
