using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] GameObject _enemyPrefab;
    [SerializeField] GameObject _enemyPool;
    [SerializeField] GameObject[] _PowerUpPrefabs;
    [SerializeField] GameObject _powerUpPool;

    bool _isDead = false;

    void Start()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerUp());
    }

    IEnumerator SpawnEnemyRoutine()
    {
        while (_isDead != true)
        {
            Vector3 posTospawn = new Vector3(Random.Range(-8f, 9f), 9, 0);
            GameObject newEnemy = Instantiate(_enemyPrefab, posTospawn, Quaternion.identity);
            newEnemy.transform.parent = _enemyPool.transform;
            yield return new WaitForSeconds(5);
        }
    }
    IEnumerator SpawnPowerUp()
    {
        while (_isDead != true)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-8f, 8f), 9, 0);
            int randomPowerUpPrefabIndex = Random.Range(0, 2);
            GameObject newPoweUp = Instantiate(_PowerUpPrefabs[randomPowerUpPrefabIndex], posToSpawn, Quaternion.identity);
            newPoweUp.transform.parent = _powerUpPool.transform;
            yield return new WaitForSeconds(7f);
        }
    }

    public void OnTriggerDeth()
    {
        _isDead = true;
    }
}
