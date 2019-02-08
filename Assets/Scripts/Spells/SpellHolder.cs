using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellHolder : MonoBehaviour {

    [SerializeField]
    GameObject spell;

	public void setSpell(bool isOn)
    {
        //TODO : spell.SetActive(isOn);
    }

    public bool isConePowerEmpty()
    {
        return spell.GetComponent<ConeSpell>().isConePowerEmpty();
    }

}
