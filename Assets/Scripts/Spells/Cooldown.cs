using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cooldown : MonoBehaviour
{
    [SerializeField]
    private Image cd1;
    [SerializeField]
    private Image cd2;
    [SerializeField]
    private Image cd3;
    [SerializeField]
    private Image cd4;

    private float quickCdTime;
    private float cd1Time;
    private float cd3Time;
    private float cd4Time;

    [SerializeField]
    ConeSpell coneSpellHolder;

    // Default cooldown times
    [SerializeField]
    private float quickSkillCD = 0.3f;
    [SerializeField]
    private float defaultCD1 = 3f;
    [SerializeField]
    private float defaultCD3 = 10f;
    [SerializeField]
    private float defaultCD4 = 2f;


    // Is Skill currently cooling down
    public bool isCoolingDown(int cdNum)
    {
        switch(cdNum)
        {
            case 0:
                return quickCdTime > 0;
            case 1:
                return cd1Time > 0;
            case 3:
                return cd3Time > 0;
            case 4:
                return cd4Time > 0;
        }
        return false;
    }

    void Update()
    {
        updateQuickSkillCoolDown();
        UpdateCooldown1();
        UpdateCooldown2();
        UpdateCooldown3();
        UpdateCooldown4();
    }

    public void InitiateCooldown(int cdNum)
    {
        switch(cdNum)
        {
            case 0:
                quickCdTime = quickSkillCD;
                break;
            case 1:
                cd1Time = defaultCD1;
                break;
            case 3:
                cd3Time = defaultCD3;
                break;
            case 4:
                cd4Time = defaultCD4;
                break;
            default:
                Debug.Log("Invalid cooldown number");
                break;

        }

    }

    private void updateQuickSkillCoolDown()
    {
        quickCdTime -= Time.deltaTime;
    }

    private void UpdateCooldown1()
    {
        if (cd1Time > 0)
        {
            cd1.fillAmount = cd1Time / defaultCD1;
            cd1Time -= Time.deltaTime;
        } else if (cd1Time <= 0)
        {
            cd1.fillAmount = 0;
        }
    }

    private void UpdateCooldown2()
    {

        cd2.fillAmount = 1 - coneSpellHolder.GetCurrentConeCapacity() / coneSpellHolder.GetMaxConeCapacity();
    }

    private void UpdateCooldown3()
    {
        if (cd3Time > 0)
        {   

            cd3.fillAmount = cd3Time / defaultCD3;
            cd3Time -= Time.deltaTime;
        }
        else if (cd3Time <= 0)
        {
            cd3.fillAmount = 0;

        }
    }

    private void UpdateCooldown4()
    {
        if (cd4Time > 0)
        {
            cd4.fillAmount = cd4Time / defaultCD4;
            cd4Time -= Time.deltaTime;
        }
        else if (cd4Time <= 0)
        {
            cd4.fillAmount = 0;
        }
    }


}
