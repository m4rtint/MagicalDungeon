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
}
