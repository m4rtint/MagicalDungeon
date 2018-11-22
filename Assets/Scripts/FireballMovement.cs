using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballMovement : MonoBehaviour,IPooledObject{
    public float force_x;
    public float force_y;
    public float force_z;
    public void OnObjectSpawn()
    {
        Vector3 spawnForce = new Vector3(force_x,force_y,force_z);
        GetComponent<Rigidbody>().velocity = spawnForce;
    }

}
