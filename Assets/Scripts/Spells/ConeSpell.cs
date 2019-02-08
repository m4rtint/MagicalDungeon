using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class ConeSpell : MonoBehaviour {

    [Header("Damage")]
    [SerializeField]
    private float damage;
    [SerializeField]
    private float perTime;

    [Header("Cone")]
    [SerializeField]
    float maxConeCapacity = 2000f;
    [SerializeField]
    float coneRecharge = 5f;
    [SerializeField]
    float coneDrain = 17f;
    float currentConeCapacity = 0f;

    bool canDamage = true;

    private void Awake()
    {
        currentConeCapacity = maxConeCapacity;
        GetComponent<BoxCollider2D>().isTrigger = true;
    }

    private void Update()
    {
        updateConeCapacity();
    }

    private void updateConeCapacity()
    {
        float powerChange = InputManager.skillTwoPressed() ? -coneDrain : coneRecharge;
        currentConeCapacity = Mathf.Clamp(currentConeCapacity + powerChange, 0, maxConeCapacity);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == Tags.ENEMY && canDamage)
        {
            canDamage = false;
            Invoke("resetCanDamage", perTime);
            collision.gameObject.GetComponent<ICharacter>().decrementHealth(damage);
        }
    }

    bool isConePowerEmpty()
    {
        return currentConeCapacity <= 0;
    }

    public void fireConeIfAble(bool isOn = false)
    {
        bool onIfAble = !isConePowerEmpty() && isOn;
        GetComponent<SpriteRenderer>().enabled = onIfAble;
        GetComponent<BoxCollider2D>().enabled = onIfAble;
    }

    private void resetCanDamage()
    {
        canDamage = true;
    }

}
