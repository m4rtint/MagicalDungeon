using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpellController : ICharacter
{

    [SerializeField]
    GameObject coneSpellHolder;
    [SerializeField]
    CharacterGlow glow;
    Cooldown cooldownHolder;

    [Header("Haste")]
    [SerializeField]
    float hasteSpeedModifier = 2f;
    [SerializeField]
    float hasteTimeToLive = 3f;


    [Header("Cone")]
    [SerializeField]
    float maxConeCapacity = 2000f;
    [SerializeField]
    float coneRecharge = 5f;
    [SerializeField]
    float coneDrain = 17f;

    float coneCapacity = 0f;

    protected override void Awake()
    {
        coneCapacity = maxConeCapacity;
        base.Awake();
        cooldownHolder = GetComponent<Cooldown>();
    }

    // Update is called once per frame
    void FixedUpdate () {
        handleSkillInput();
        updateSpellConeRotation();
        activateFireball();
        updateConeCapacity();
    }
    
    #region Input Control
    /*
     * Skill 1 - AOE
     * Skill 2 - Firecone
     * Skill 3 - Haste    
     * 
     */
    void handleSkillInput()
    {
        if (InputManager.skillOnePressed() && !cooldownHolder.isCoolingDown(1))
        {
            activateFirestorm();
            cooldownHolder.InitiateCooldown(1);
        }

        if (InputManager.skillTwoPressed() && coneCapacity > 0)
        {
            coneSpellHolder.GetComponent<SpellHolder>().turnOnSpell();
            Debug.Log("FIREEE");
            //cooldownHolder.InitiateCooldown(2);
        }
        else
        {
            Debug.Log("NOOOOO");
            coneSpellHolder.GetComponent<SpellHolder>().turnOffSpell();
        }


        if (InputManager.skillThreePressed() && !cooldownHolder.isCoolingDown(3)) {
            base.modifySpeed(hasteSpeedModifier, hasteTimeToLive);
            cooldownHolder.InitiateCooldown(3);
            onStateChange(STATE.HASTE);
        } 

        if (InputManager.skillFourPressed() && !cooldownHolder.isCoolingDown(4)){
            activeIceStorm();
            cooldownHolder.InitiateCooldown(4);
        }
    }
    #endregion

    #region PlayerGlowState
    public void onStateChange(STATE s)
    {
        glow.onStateChange(s);
    }


    private void updateConeCapacity()
    {

        if(InputManager.skillTwoPressed() == false)
        {
            if(coneCapacity + coneRecharge >= maxConeCapacity)
            {
                coneCapacity = maxConeCapacity;
            }
            else
            {
                coneCapacity += coneRecharge;
            }
        }
        else //skillTwo is Pressed
        {
            if(coneCapacity - coneDrain < 0)
            {
                coneCapacity = 0;
            }
            else
            {
                coneCapacity -= coneDrain;
            }
        }
 

    }



    #endregion

    #region Spell

    void activateFireball()
    {
        if (InputManager.isFiring() && !cooldownHolder.isCoolingDown(0))
        {
            cooldownHolder.InitiateCooldown(0);
            GameObject fireball = ObjectPooler.Instance.SpawnFromPool(Pool.FIREBALL, transform.position, getPlayerRotation());
            fireball.GetComponent<Fireball>().OnObjectSpawn();
        }
    }


    private void updateSpellConeRotation()
    {
        float angle = Utilities.getAngleDegBetweenMouseAnd(gameObject) - 90;
        coneSpellHolder.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private void activeIceStorm()
    {
        GameObject icestorm = ObjectPooler.Instance.SpawnFromPool(Pool.ICESTORM, transform.position, getPlayerRotation());
        icestorm.GetComponent<IceStorm>().OnObjectSpawn();
    }


    private void activateFirestorm()
    {
        GameObject firestorm = ObjectPooler.Instance.SpawnFromPool(Pool.FIRESTORM, transform.position, getPlayerRotation());
        firestorm.GetComponent<Firestorm>().OnObjectSpawn();
    }

    private Quaternion getPlayerRotation()
    {
        float angle = Utilities.getAngleDegBetweenMouseAnd(gameObject);
        Debug.Log(angle);
        return Quaternion.AngleAxis(angle, Vector3.forward);
    }
    #endregion

    #region Override
    public override void incrementHealth(float heal)
    {
        base.incrementHealth(heal);
        GetComponent<PlayerSpellController>().onStateChange(STATE.HEAL);
    }
    #endregion
}
