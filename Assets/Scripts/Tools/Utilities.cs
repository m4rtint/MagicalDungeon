using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utilities {

    public static Vector3 worldToScreenObjectPosition(GameObject gObj)
    {
        return Camera.main.WorldToScreenPoint(gObj.transform.position);
    }

    public static float getAngleDegBetween(float y, float x)
    {
        return Mathf.Atan2(y, x) * Mathf.Rad2Deg;
    }

    public static float getAngleDegBetween(GameObject gObj)
    {
        Vector3 dir = directionBetweenMouseAndCharacter(gObj);
        return getAngleDegBetween(dir.y, dir.x);
    }

    public static Vector3 directionBetweenMouseAndCharacter(GameObject gObj)
    {
        return Input.mousePosition - worldToScreenObjectPosition(gObj);
    }
}
