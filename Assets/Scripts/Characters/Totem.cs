using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Totem : ICharacter {

    [SerializeField]
    SpawnManager spawnManager;

    protected override void Awake()
    {
        base.Awake();
        gameObject.SetActive(true);
        healthBar.SetActive(false);
        spawnManager = GetComponent<SpawnManager>();
    }



    public override void decrementHealth(float damage)
    {
        base.decrementHealth(damage);
        healthBar.SetActive(true);
    }

    protected override void onDeath()
    {
        spawnManager.DestroyTotem();
        gameObject.SetActive(false);
    }

    private void onDeathAnimationComplete()
    {
        gameObject.SetActive(false);
    }


}
