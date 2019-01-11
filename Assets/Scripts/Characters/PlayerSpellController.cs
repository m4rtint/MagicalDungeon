using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpellController : MonoBehaviour {

    [SerializeField]
    GameObject coneSpellHolder;

    private void Start()
    {
        coneSpellHolder.SetActive(false);
    }

    // Update is called once per frame
    void Update () {
        activateSpellConeIfNeeded();
        updateSpellConeRotation();
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
    #endregion
}
