using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {

    Transform[] spawnPoints;

    [SerializeField]
    Monsters[] monstersToSpawn;

    [System.Serializable]
    public class Monsters
    {
        public MONSTERS tag;
        [Range(0, 1)]
        [SerializeField]
        public float percentage;
    }

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
        enemy.onCharacterDeath += enemyOnDeath;
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
        float rate = Random.Range(0.0f, 1.0f);
        MONSTERS m = generateMonsterBasedOnPercentage(rate);
        return monstersToSpawn.Length == 0 ? convertEnumToString(0) : convertEnumToString(m);
    }

    MONSTERS generateMonsterBasedOnPercentage(float rate)
    {
        MONSTERS chosenMonster = monstersToSpawn[0].tag;
        float accumulatedChance = accumulatedMonsterChances();
        foreach (Monsters m in monstersToSpawn)
        {
            float percentage = m.percentage / accumulatedChance;
            if (percentage <= rate)
            {
                chosenMonster = m.tag;
            }
        }

        return chosenMonster;
    }

    float accumulatedMonsterChances()
    {
        float accumulatedChance = 0;
        foreach (Monsters m in monstersToSpawn)
        {
            accumulatedChance += m.percentage;
        }

        return Mathf.Clamp(accumulatedChance, 0.0001f, 1);
    }



    string convertEnumToString(MONSTERS m)
    {
        switch(m)
        {
            case MONSTERS.MUSHROOM:
                return Pool.MUSHROOM;
            case MONSTERS.ZOMBIE:
                return Pool.ZOMBIE;
            case MONSTERS.RANGED_SLIME:
                return Pool.RANGED_SLIME;
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
    ZOMBIE,
    RANGED_SLIME,
}
