using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpellController : MonoBehaviour {

    [SerializeField]
    GameObject coneSpellHolder;
    Cooldown cooldownHolder;

    private void Awake()
    {
        cooldownHolder = GetComponent<Cooldown>();
    }

    // Update is called once per frame
    void Update () {
        handleSkillInput();
        updateSpellConeRotation();
        activateFireball();
    }



    #region Skills
    void handleSkillInput()
    {   
        // AOE skill 
        if (InputManager.skillOnePressed() && !cooldownHolder.isCoolingDown(1))
        {
            activateFirestorm();
            // Update cooldown 1 here
            cooldownHolder.InitiateCooldown(1);
        }
        // Fire cone 
        else if (InputManager.skillTwoPressed() && !cooldownHolder.isCoolingDown(2))
        {
            coneSpellHolder.GetComponent<SpellHolder>().turnOnSpell();
            // Update cooldown 2 here
            cooldownHolder.InitiateCooldown(2);
        } else if (InputManager.skillThreePressed() && !cooldownHolder.isCoolingDown(3))
        {
            cooldownHolder.InitiateCooldown(3);
        } else if (InputManager.skillFourPressed() && !cooldownHolder.isCoolingDown(4))
        {
            cooldownHolder.InitiateCooldown(4);
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
        float angle = Utilities.getAngleDegBetweenMouseAnd(gameObject)+90;
        coneSpellHolder.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }


    private void activateFirestorm()
    {
        GameObject firestorm = ObjectPooler.Instance.SpawnFromPool(Pool.FIRESTORM, transform.position, getPlayerRotation());
        firestorm.GetComponent<Firestorm>().OnObjectSpawn();
    }

    private Quaternion getPlayerRotation()
    {
        float angle = Utilities.getAngleDegBetweenMouseAnd(gameObject);
        return Quaternion.AngleAxis(angle, Vector3.forward);
    }
    #endregion
}
