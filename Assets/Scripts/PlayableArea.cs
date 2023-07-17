using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayableArea : MonoBehaviour
{
    private static PlayableArea _instance;
    public static PlayableArea Instance { get { return _instance; } }

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

    [SerializeField] private Rect _areaSize;
    [SerializeField] private float _translateAmount = -0.99f;


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(new Vector2(_areaSize.x + _areaSize.width / 2, _areaSize.y + _areaSize.height / 2), 
            new Vector2(_areaSize.width, _areaSize.height));
    }

    public void CheckContains(Transform transform)
    {
        if (_areaSize.Contains(transform.position))
            return;

        if (transform.position.x < _areaSize.x || transform.position.x > _areaSize.width/2)
        {
            transform.position = new Vector2(transform.position.x * _translateAmount, transform.position.y);
        }
        else if (transform.position.y < _areaSize.y || transform.position.y > _areaSize.height/2)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y * _translateAmount);
        }

    }
}
