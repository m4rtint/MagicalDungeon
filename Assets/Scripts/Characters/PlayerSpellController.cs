using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpellController : ICharacter
{

    [SerializeField]
    GameObject coneSpellHolder;
    SpellHolder spellHolder;
    [SerializeField]
    CharacterGlow glow;
    Cooldown cooldownHolder;

    [Header("Haste")]
    [SerializeField]
    float hasteSpeedModifier = 2f;
    [SerializeField]
    float hasteTimeToLive = 3f;

    protected override void Awake()
    {
        base.Awake();
        spellHolder = coneSpellHolder.GetComponent<SpellHolder>();
        cooldownHolder = GetComponent<Cooldown>();
    }

    // Update is called once per frame
    void FixedUpdate () {
        handleSkillInput();
        updateSpellConeRotation();
        activateFireball();
        //updateConeCapacity();
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

        if (InputManager.skillTwoPressed())
        {
            spellHolder.fireConeIfAble(true);
            //cooldownHolder.InitiateCooldown(2);
        } else
        {
            spellHolder.fireConeIfAble(false);
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
