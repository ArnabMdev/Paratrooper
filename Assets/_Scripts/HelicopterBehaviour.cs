using System;
using UnityEngine;

public class HelicopterBehaviour : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private bool isFlipped;
    [SerializeField] private GameObject _paraTrooper;
    [SerializeField] private int _minSpawns,_maxSpawns;
    [SerializeField] private float _spawnStart,_spawnEnd;

    private Vector3[] _spawnPoints;
    private int _spawnCount;
    private bool tripFinished;

    
    
    void Start()
    {
        _spawnCount = UnityEngine.Random.Range(_minSpawns, _maxSpawns + 1);
        _spawnPoints = new Vector3[_spawnCount];
        for (var i = 0; i < _spawnCount; i++)
        {
            var spawnPoint = new Vector3(UnityEngine.Random.Range(_spawnStart, _spawnEnd), transform.position.y - 0.1f);
            _spawnPoints[i] = spawnPoint;
        }
        Array.Sort(_spawnPoints);

    }

    void FixedUpdate()
    {
        if (isFlipped)
            transform.position += new Vector3(-0.1f * _speed, 0);
        else
            transform.position += new Vector3(0.1f * _speed, 0);

        if(_spawnCount < 0)
        {
            return;
        }

        if(Vector3.Distance(transform.position,_spawnPoints[_spawnCount-1]) <= 0.2f)
        {
            SpawnParatrooper();
            _spawnCount--;
        }

        if(transform.position.x < _spawnStart - 2.0f || transform.position.x > _spawnEnd + 2.0f)
        {
            tripFinished = true;
            Destroy(this.gameObject);
        }
        else
        {
            tripFinished = false;
        }    
    }

    private void SpawnParatrooper()
    {
        Instantiate(_paraTrooper, transform.position, Quaternion.identity);
    }

    private void OnDestroy()
    {
        if(tripFinished)
            GameManager.instance.HelicopterDestroyed(isFlipped,true);
        else
            GameManager.instance.HelicopterDestroyed(isFlipped,false);
    }




}
