using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : ICharacter {

    public bool awakened;

    protected override void Awake()
    {
        base.Awake();
        healthBar.SetActive(false);
    }

    protected override  void Update()
    {
        if (awakened)
        {

        }
    }




}
