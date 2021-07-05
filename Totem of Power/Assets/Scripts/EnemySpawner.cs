using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] float minSpawnDelay = 1f;
    [SerializeField] float maxSpawnDelay = 5f;
    [SerializeField] Enemy[] enemyPrefabs;

    bool spawn = true;

    IEnumerator Start()
    {
        while (spawn)
        {
            yield return new WaitForSeconds(UnityEngine.Random.Range(minSpawnDelay, maxSpawnDelay));
            SpawnEnemy();
        }
    }

    public void StopSpawning()
    {
        spawn = false;
    }

    private void SpawnEnemy()
    {
        var randomEnemyIndex = UnityEngine.Random.Range(0, enemyPrefabs.Length);
        Spawn(enemyPrefabs[randomEnemyIndex]);
    }

    private void Spawn(Enemy myEnemy)
    {
        Enemy newEnemy = Instantiate
            (myEnemy, transform.position, transform.rotation)
            as Enemy;
        Transform enemyChildObject = newEnemy.transform.GetChild(0);
        // Set the Face as the enemy's parent
        newEnemy.transform.parent = transform.parent;
        enemyChildObject.SetParent(newEnemy.transform, true);
        newEnemy.transform.localScale = new Vector3(1, 1, 1);
    }
}
