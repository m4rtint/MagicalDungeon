using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Haste : MonoBehaviour
{
    private float defaultCooldown;
    private float currentCooldown;
    private float hasteSpeedModifier = 2f;

    private void Awake()
    {
        currentCooldown = 0;
        defaultCooldown = 2;
    }

    void Update()
    {
        if (currentCooldown > 0)
        {
            currentCooldown -= Time.deltaTime;
        }
        else
        {
            GetComponent<PlayerController>().speedModifier = 1;
        }

    }

    public void InitiateHaste()
    {
        currentCooldown = defaultCooldown;
        GetComponent<PlayerController>().speedModifier = hasteSpeedModifier;
    }
}
