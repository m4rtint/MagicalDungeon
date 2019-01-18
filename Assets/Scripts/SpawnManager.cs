using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {

    Transform[] spawnPoints;

    [Range(0, 10)]
    [SerializeField]
    int maxEnemySpawned = 1;
    int currentActiveEnemies;

    private void Awake()
    {
        spawnPoints = gameObject.GetComponentsInChildren<Transform>(); 
    }

    private void Update()
    {
        spawnEnemyIfNeeded();
    }

    bool isSpawnNeeded()
    {
        return currentActiveEnemies < maxEnemySpawned;
    }

    void spawnEnemyIfNeeded()
    {
        if (isSpawnNeeded())
        {
            GameObject enemy = ObjectPooler.Instance.SpawnFromPool(Pool.ENEMY, getSpawnPosition(), Quaternion.identity);
            setupEnemy(enemy.GetComponent<EnemyController>());
            currentActiveEnemies++;
        }
    }

    void setupEnemy(EnemyController enemy)
    {
        enemy.onDeathDelegate += enemyOnDeath;
        enemy.OnObjectSpawn();
    }

    Vector3 getSpawnPosition()
    {
        //Note - Need 1 since index 0 is parent SpawnManager.
        int index = Random.Range(1, spawnPoints.Length);
        return spawnPoints[index].position;
    }

    #region Delegate
    void enemyOnDeath()
    {
        currentActiveEnemies--;
    }
    #endregion

}
