using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour {
    [SerializeField]
    private List<Pool> pools;
    private Dictionary<string, Queue<GameObject>> poolDictionary;


    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    #region Singleton
    public static ObjectPooler Instance;

    private void Awake()
    {
        Instance = this;
    }
    #endregion Singleton

    


	// Use this for initialization
	void Start () {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach(Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(pool.tag,  objectPool);
        }
	}

    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning(tag + " does not exist");
            return null;
        }
       
        GameObject newObj =   poolDictionary[tag].Dequeue();
        newObj.SetActive(true);
        newObj.transform.position = position;
        newObj.transform.rotation = rotation;

        poolDictionary[tag].Enqueue(newObj);

        return newObj;
    }


}
