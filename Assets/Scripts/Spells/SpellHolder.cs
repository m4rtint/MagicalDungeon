using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellHolder : MonoBehaviour {

    [SerializeField]
    GameObject spell;

    public void fireConeIfAble(bool isOn)
    {
        spell.GetComponent<ConeSpell>().fireConeIfAble(isOn);
    }


}
