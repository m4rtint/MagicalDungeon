using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InputManager{

    public static float MainHorizontal()
    {
        float r = 0.0f;
        r += Input.GetAxis("Joy_Horizontal");
        r += Input.GetAxis("Key_Horizontal");
        return Mathf.Clamp(r, -1.0f, 1.0f); //If both joystick and keyboard are being used, clamp between values
    }

    public static float MainVertical()
    {
        float r = 0.0f;
        r += Input.GetAxis("Joy_Vertical");
        r += Input.GetAxis("Key_Vertical");
        return Mathf.Clamp(r, -1.0f, 1.0f);
    }

    public static Vector3 MainInput()
    {
        return new Vector3(MainHorizontal(), MainVertical(), 0.0f );
    }

    public static bool isFiring()
    {
        return Input.GetButton("Fire");
    }

    public static bool skillOnePressed()
    {
        return Input.GetButtonDown("SkillOne");
    }

    public static bool skillTwoPressed()
    {
        return Input.GetButton("SkillTwo");
    }

    public static bool skillThreePressed()
    {
        return Input.GetButtonDown("SkillThree");
    }

    public static bool skillFourPressed()
    {
        return Input.GetButtonDown("SkillFour");
    }

    public static bool isCastingSpell()
    {
        return isFiring() || skillOnePressed() || skillTwoPressed() || skillFourPressed();
    }
}
