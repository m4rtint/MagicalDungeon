using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum STATE
{
    NORMAL,
    HEAL,
    HASTE
}

public class CharacterGlow : MonoBehaviour {

    const float colorGlowTimer = 2f;
    SpriteRenderer glowColour;
    float currentGlowTimer = 0;
    bool isGlowing = false;

    private void Awake()
    {
        glowColour = GetComponent<SpriteRenderer>();
        resetColour();
    }

    public void onStateChange(STATE s)
    {
        switch(s)
        {
            case STATE.NORMAL:
                resetColour();
                break;
            case STATE.HEAL:
                healingColor();
                break;
            case STATE.HASTE:
                hasteColor();
                break;
            default:
                resetColour();
                break;
        }

        isGlowing = true;
        currentGlowTimer = colorGlowTimer;
    }

    private void Update()
    {
        if (isGlowing) 
        {
            currentGlowTimer -= Time.fixedDeltaTime;

            if (currentGlowTimer <= 0)
            {
                isGlowing = false;
                resetColour();
            }
        }
    }

    void resetColour()
    {
        glowColour.color = Color.clear;
    }

    void healingColor()
    {
        glowColour.color = Color.green;
    }

    void hasteColor()
    {
        glowColour.color = Color.blue;
    }
}
