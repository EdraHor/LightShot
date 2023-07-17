using Pool;
using System;
using UnityEngine;

[RequireComponent(typeof(PoolObject))]
public class Enemy : MonoBehaviour, IDamagable
{
    [SerializeField] private Player _target;
    [SerializeField] private float _speed;
    [SerializeField] private GameObject _enemy;
    [SerializeField] private GameObject _dieObject;
    private PoolObject _poolObject;
    private Collider2D _collider;
    private Rigidbody2D _rigidBody;

    void Start()
    {
        _collider = GetComponent<Collider2D>();
        _poolObject = GetComponent<PoolObject>();
        if (!_target)
            _target = FindObjectOfType<Player>();
    }

    void Update()
    {
        PlayableArea.Instance.CheckContains(transform);
        if (_enemy.activeInHierarchy)
            transform.position = Vector2.MoveTowards(transform.position, _target.transform.position, 
                _speed * Time.deltaTime);
    }

    public void Kill()
    {
        _enemy.SetActive(false);
        _collider.enabled = false;
        _collider.attachedRigidbody.simulated = false;
        _dieObject.SetActive(true);
        _poolObject.ReturnToPoolDelay(5);
        EnemyController.Instance.InvokeEnemyDied();
    }

    private void OnEnable()
    {
        _enemy.SetActive(true);
        if (_collider)
        {
            _collider.enabled = true;
            _collider.attachedRigidbody.simulated = true;
        }
        _dieObject.SetActive(false);
    }
}

