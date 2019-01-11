﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour, IPooledObject {

    [SerializeField] float velocity;
    [SerializeField] float secondsDuration;
    private float angle;
    private float currentDuration;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(VectorFromAngle(angle) * velocity);
        currentDuration += Time.fixedDeltaTime;
        if (currentDuration > secondsDuration)
        {
            gameObject.SetActive(false);
        }
    }

    Vector2 VectorFromAngle(float theta)
    {
        return new Vector2(Mathf.Cos(theta), Mathf.Sin(theta)); // Trig is fun
    }

    public void OnObjectSpawn()
    {
        Vector3 dir = transform.rotation.eulerAngles;
        angle = Utilities.getAngleDegBetween(dir.y, dir.x) + 90;
        currentDuration = 0; 
    }
}
