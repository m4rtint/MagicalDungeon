using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {

    Transform[] spawnPoints;

    [SerializeField]
    MONSTERS[] monstersToSpawn;

    [Range(0, 20)]
    [SerializeField]
    int maxActiveEnemySpawned = 1;
    [Range(0, 40)]
    [SerializeField]
    int maxEnemySpawned = 1;
    int currentActiveEnemies;
    int enemiesAlreadySpawned = 0;

    [SerializeField]
    bool isRespawning = false;

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
        return  currentActiveEnemies < maxActiveEnemySpawned && (isRespawning || !isSpawnsCompleted());
    }

    bool isSpawnsCompleted()
    {
        return enemiesAlreadySpawned >= maxEnemySpawned;
    }

    void spawnEnemyIfNeeded()
    {
        if (isSpawnNeeded())
        {
            GameObject enemy = ObjectPooler.Instance.SpawnFromPool(generateRandomEnemy(), getSpawnPosition(), Quaternion.identity);
            setupEnemy(enemy.GetComponent<EnemyController>());
            currentActiveEnemies++;
            enemiesAlreadySpawned++;
        }

        if (!isRespawning && isSpawnsCompleted())
        {
            Destroy(gameObject);
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

    string generateRandomEnemy()
    {
        int length = Mathf.Max(0, monstersToSpawn.Length);
        int index = Random.Range(0, length);

        return monstersToSpawn.Length == 0 ? convertEnumToString(0) : convertEnumToString(monstersToSpawn[index]);
    }

    string convertEnumToString(MONSTERS m)
    {
        switch(m)
        {
            case MONSTERS.MUSHROOM:
                return Pool.MUSHROOM;
            case MONSTERS.ZOMBIE:
                return Pool.ZOMBIE;
            default:
                return Pool.MUSHROOM;
        }
    }

    #region Delegate
    void enemyOnDeath()
    {
        currentActiveEnemies--;
    }
    #endregion

    #region Debug
    void OnDrawGizmosSelected()
    {
        // Draw SPawn Points
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, 1);
    }
    #endregion

}

public enum MONSTERS
{
    MUSHROOM,
    ZOMBIE
}
