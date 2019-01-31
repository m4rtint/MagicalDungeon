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

    private float cd1Time;
    private float cd2Time;
    private float cd3Time;
    private float cd4Time;

    // Default cooldown times
    [SerializeField]
    private float defaultCD1 = 3f;
    [SerializeField]
    private float defaultCD2 = 4f;
    [SerializeField]
    private float defaultCD3 = 5f;
    [SerializeField]
    private float defaultCD4 = 2f;



    void Update()
    {
        UpdateCooldown1();
        UpdateCooldown2();
        UpdateCooldown3();
        UpdateCooldown4();
    }

    public void InitiateCooldown(int cdNum)
    {
        //Debug.Log("Cooldown initiated");
        switch(cdNum)
        {
            case 1:
                cd1Time = defaultCD1;
                break;
            case 2:
                cd2Time = defaultCD2;
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

    private void UpdateCooldown1()
    {
        if (cd1Time > 0)
        {
            cd1.fillAmount = (cd1Time * (360 / defaultCD1)) / 360;
            cd1Time -= Time.deltaTime;
        } else if (cd1Time <= 0)
        {
            cd1.fillAmount = 1f;
        }
    }

    private void UpdateCooldown2()
    {
        if (cd2Time > 0)
        {
            cd2.fillAmount = (cd2Time * (360 / defaultCD2)) / 360;
            cd2Time -= Time.deltaTime;
        }
        else if (cd2Time <= 0)
        {
            cd2.fillAmount = 1f;
        }
    }

    private void UpdateCooldown3()
    {
        if (cd3Time > 0)
        {
            cd3.fillAmount = (cd3Time * (360 / defaultCD3)) / 360;
            cd3Time -= Time.deltaTime;
        }
        else if (cd3Time <= 0)
        {
            cd3.fillAmount = 1f;
        }
    }

    private void UpdateCooldown4()
    {
        if (cd4Time > 0)
        {
            cd4.fillAmount = (cd4Time * (360 / defaultCD4)) / 360;
            cd4Time -= Time.deltaTime;
        }
        else if (cd4Time <= 0)
        {
            cd4.fillAmount = 1f;
        }
    }


}
