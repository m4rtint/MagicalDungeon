using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpellController : MonoBehaviour {

    [SerializeField]
    GameObject coneSpellHolder;
    [SerializeField]
    float firestormSpawnDistance;
    [SerializeField]
    GameObject cooldownHolder;

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
        if (InputManager.skillOnePressed())
        {
            activateFirestorm();
            // Update cooldown 1 here
            cooldownHolder.GetComponent<Cooldown>().InitiateCooldown(1);
        }
        // Fire cone 
        else if (InputManager.skillTwoPressed())
        {
            coneSpellHolder.GetComponent<SpellHolder>().turnOnSpell();
            // Update cooldown 2 here
            cooldownHolder.GetComponent<Cooldown>().InitiateCooldown(2);
        } else if (InputManager.skillThreePressed())
        {
            cooldownHolder.GetComponent<Cooldown>().InitiateCooldown(3);
        } else if (InputManager.skillFourPressed())
        {
            cooldownHolder.GetComponent<Cooldown>().InitiateCooldown(4);
        }
    }
    #endregion

    void activateFireball()
    {
        if (InputManager.isFiring())
        {
            float angle = Utilities.getAngleDegBetween(gameObject);
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            GameObject fireball = ObjectPooler.Instance.SpawnFromPool(Pool.FIREBALL, transform.position, rotation);
            fireball.GetComponent<Fireball>().OnObjectSpawn();
        }
    }

    #region Spell
    
    private void updateSpellConeRotation()
    {
        float angle = Utilities.getAngleDegBetween(gameObject)+90;
        coneSpellHolder.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }


    private void activateFirestorm()
    {
        Vector3 dir = Utilities.directionBetweenMouseAndCharacter(gameObject).normalized;
        GameObject firestorm = ObjectPooler.Instance.SpawnFromPool(Pool.FIRESTORM, transform.position + dir * firestormSpawnDistance, Quaternion.identity);
        firestorm.GetComponent<Firestorm>().OnObjectSpawn();
    }
    #endregion
}
