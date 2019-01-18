using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpellController : MonoBehaviour {

    [SerializeField]
    GameObject coneSpellHolder;
    [SerializeField]
    float firestormSpawnDistance;

    private void Start()
    {
        coneSpellHolder.SetActive(false);
    }

    // Update is called once per frame
    void Update () {
        //activateSpellConeIfNeeded();
        updateSpellConeRotation();
        //activateFireball();
        activateFirestorm();
    }

    void activateFireball()
    {
        if (InputManager.isFiring())
        {
            Vector3 dir = Input.mousePosition - Utilities.worldToScreenObjectPosition(gameObject);
            float angle = Utilities.getAngleDegBetween(dir.y, dir.x);
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            GameObject fireball = ObjectPooler.Instance.SpawnFromPool(Pool.FIREBALL, transform.position, rotation);
            fireball.GetComponent<Fireball>().OnObjectSpawn();
        }
    }

    #region Spells
    private void activateSpellConeIfNeeded()
    {
        if (InputManager.isFiring())
        {
            coneSpellHolder.SetActive(true);

        } else if (InputManager.isNotFiring())
        {
            coneSpellHolder.SetActive(false);
        } 
    }
   
    
    private void updateSpellConeRotation()
    {
        Vector3 dir = Input.mousePosition - Utilities.worldToScreenObjectPosition(gameObject);
        float angle = Utilities.getAngleDegBetween(dir.y, dir.x) + 90;
        coneSpellHolder.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }


    private void activateFirestorm()
    {
        if (InputManager.isFiring())
        {
            Vector3 dir = (Input.mousePosition - Utilities.worldToScreenObjectPosition(gameObject)).normalized;
            GameObject firestorm = ObjectPooler.Instance.SpawnFromPool(Pool.FIRESTORM, transform.position + dir * firestormSpawnDistance, Quaternion.identity);
            firestorm.GetComponent<Firestorm>().OnObjectSpawn();
        }
    }
    #endregion
}
