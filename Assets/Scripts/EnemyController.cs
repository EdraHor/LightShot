using Pool;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private static EnemyController _instance;
    public static EnemyController Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    [SerializeField] private GameObject _enemyPrefab = default;
    [SerializeField] private Transform _poolContainer;
    [SerializeField] private Player _target;
    [SerializeField, Range(1, 20)] private float _spawnDistanceRadius = 10;
    [SerializeField, Range(1, 20)] private float _spawnSafeDistanceRadius = 5;
    [SerializeField] private int _enemySpawnCount = 20;
    [SerializeField] private int _liveEnemyLimit = 150;
    [SerializeField] private float _spawnRare = 15f;
    private float _currentSpawnTime = 0;
    private EnemyPooling _pool;
    public int EnemyLiveCount;
    public int Score;
    public bool isSpawn = true;

    public delegate void Enemy();
    public event Enemy EnemyDiedEvent;
    public event Enemy EnemySpawnEvent;

    void Start()
    {
        _spawnDistanceRadius += _spawnSafeDistanceRadius;
        _pool = new EnemyPooling(_enemyPrefab, 100, _poolContainer, true);
        _target = FindObjectOfType<Player>();
    }

    public void InvokeEnemyDied()
    {
        EnemyLiveCount--;
        Score ++;
        EnemyDiedEvent?.Invoke();
    }

    private void OnDrawGizmosSelected()
    {
        if (!_target)
            return;

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(_target.transform.position, _spawnDistanceRadius + _spawnSafeDistanceRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_target.transform.position, _spawnSafeDistanceRadius);
    }

    private void SpawnEnemy()
    {
        EnemyLiveCount += _enemySpawnCount;
        EnemySpawnEvent?.Invoke();

        for (int i = 0; i < _enemySpawnCount; i++)
        {
            Vector2 spawnPoint = _target.transform.position + UnityEngine.Random.onUnitSphere * _spawnDistanceRadius;

            while(Vector2.Distance(spawnPoint, _target.transform.position) < _spawnSafeDistanceRadius)
            {
                spawnPoint = _target.transform.position + UnityEngine.Random.onUnitSphere * _spawnDistanceRadius;
            }

            _pool.Spawn(spawnPoint);
        }
        
    }

    void Update()
    {
        if (!isSpawn)
            return;

        if ((_currentSpawnTime < Time.time || EnemyLiveCount == 0) && EnemyLiveCount < _liveEnemyLimit)
        {
            _currentSpawnTime = Time.time + _spawnRare;

            SpawnEnemy();
            _enemySpawnCount += Score / 10;
        }
    }
}
