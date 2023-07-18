using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    //[SerializeField] private GameObject _enemyPrefab;

    //[SerializeField] private float _minimumSpawnTime;

    //[SerializeField] private float _maximumSpawnTime;

    //private float _timeUntilSpawn;

    //private void Awake()
    //{
    //    SetTimeUntilSpawn();
    //}

    //private void Update()
    //{
    //    _timeUntilSpawn -= Time.deltaTime;

    //    if(_timeUntilSpawn <= 0)
    //    {
    //        Instantiate(_enemyPrefab, transform.position, Quaternion.identity);
    //        SetTimeUntilSpawn();
    //    }
    //}
    //private void SetTimeUntilSpawn()
    //{
    //    _timeUntilSpawn = Random.Range(_minimumSpawnTime,_maximumSpawnTime);
    //}
    public GameObject Enemy;

    float maxSpawnRateInSeconds = 5f;
    void Start()
    {
    }

    void Update()
    {

    }
    void SpawnEnemy()
    {
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        GameObject anEnemy = (GameObject)Instantiate(Enemy);
        anEnemy.transform.position = new Vector2(Random.Range(min.x, max.x), max.y);

        ScheduleNextEnemySpawn();
    }

    void ScheduleNextEnemySpawn()
    {
        float spawnInSeconds;

        if (maxSpawnRateInSeconds > 1)
        {
            spawnInSeconds = Random.Range(1f, maxSpawnRateInSeconds);
        }
        else
        {
            spawnInSeconds = 1f;
        }

        Invoke("SpawnEnemy", spawnInSeconds);
    }

    void InreaseSpawnRate()
    {
        if (maxSpawnRateInSeconds > 1f)
        {
            maxSpawnRateInSeconds--;
        }
        if (maxSpawnRateInSeconds == 1f)
        {
            CancelInvoke("InreaseSpawnRate");
        }
    }

    public void ScheduleEnemySpawner()
    {
        float maxSpawnRateInSeconds = 5f;

        Invoke("SpawnEnemy", maxSpawnRateInSeconds);

        InvokeRepeating("InreaseSpawnRate", 0f, 30f);
    }
    public void UnscheduleEnemySpawner()
    {
        CancelInvoke("SpawnEnemy");
        CancelInvoke("InreaseSpawnRate");
    }
}
