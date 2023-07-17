using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private Vector3 _offset;
    [SerializeField] private bool _isSmoothnees = true;
    [SerializeField] private float _speed = 1.5f;
    private Vector3 _lastPosition;

    private void Start()
    { 
        if (!_target) _target = FindObjectOfType<Player>().transform;
    }

    private void Update()
    {
        if (_target)
        {
            Vector3 newPosition;
            if (_isSmoothnees)
            {
                newPosition = Vector3.Lerp(transform.position, _lastPosition, _speed * Time.deltaTime);
                _lastPosition = _target.position + _offset;
            }
            else
            {
                newPosition = _target.position + _offset;
            }

            transform.position = newPosition;
        }
    }
}
