using System;
using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

// СТРОКУ using System.Random; МЫ УДАЛИЛИ!

public class SpawnerLogic : MonoBehaviour
{
    [SerializeField] private GameObject[] _enemyPrefabs;
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private int _maxEnemies = 10;
    [SerializeField] private float _lengthOffSpawner=2f;
    [SerializeField] private float _spawnDelay = 1f; 
    [SerializeField] private LayerMask _enemyLayer;
    private int _currentEnemies = 0; 

    private void Start()
    {
      
        StartCoroutine(SpawnEnemiesRoutine());
    }

    private void CheckerEnemies(object sender,EventArgs e)
    {
        _currentEnemies--;
    }

    private IEnumerator SpawnEnemiesRoutine()
    {
        while (true)
        {
            if (_currentEnemies < _maxEnemies)
            {
                int randomIndex = Random.Range(0, _spawnPoints.Length);
                Transform spawnPoint = _spawnPoints[randomIndex]; 
                
                // 2. ДОБАВЛЯЕМ _enemyLayer В РАДАР!
                Collider2D hitCollider = Physics2D.OverlapCircle(spawnPoint.position, _lengthOffSpawner, _enemyLayer);
                
                if (hitCollider == null)
                {
                    int randomEnemyIndex = Random.Range(0, _enemyPrefabs.Length);
                    GameObject enemyToSpawn = _enemyPrefabs[randomEnemyIndex];

                    Instantiate(enemyToSpawn, spawnPoint.position, spawnPoint.rotation);
                    _currentEnemies++; 
                    yield return new WaitForSeconds(_spawnDelay);
                }
                else
                {
                    yield return null; 
                }
            }
            else
            {
                yield return null;
            }
        }
    }


    private void OnEnable()
    {
        EnemyEntity.OnAnyDeath += CheckerEnemies;
    }

    private void OnDisable()
    {
        EnemyEntity.OnAnyDeath -= CheckerEnemies;
    }
}