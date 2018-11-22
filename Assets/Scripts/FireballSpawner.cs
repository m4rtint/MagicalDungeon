using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballSpawner : MonoBehaviour {

    ObjectPooler objectPooler;

    private void Start()
    {
        objectPooler = ObjectPooler.Instance;
    }

    private void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Mouse Pressed");
            GameObject obj = objectPooler.SpawnFromPool("fireball", transform.position, Quaternion.identity);
            IPooledObject pooledObj =  obj.GetComponent<FireballMovement>();
            if (pooledObj != null)
            {
                pooledObj.OnObjectSpawn();
            }
        }
    }
}
