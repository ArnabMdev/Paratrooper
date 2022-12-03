using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HelicopterBehaviour : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private bool isFlipped;
    [SerializeField] private GameObject _paraTrooper;
    [SerializeField] private int _minSpawns,_maxSpawns;
    [SerializeField] private float _spawnStart,_spawnEnd;

    private bool tripFinished;

    private void Start()
    {
        StartCoroutine(SpawnParatrooper(UnityEngine.Random.Range(_minSpawns, _maxSpawns + 1)));
    }
    void FixedUpdate()
    {
        if (isFlipped)
            transform.position += new Vector3(-0.1f * _speed, 0);
        else
            transform.position += new Vector3(0.1f * _speed, 0);

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

    private void SpawnTrooper()
    {
        Instantiate(_paraTrooper, transform.position, Quaternion.identity);
    }

    private void OnDestroy()
    {
        if(tripFinished)
            GameManager.instance.HelicopterDestroyed(isFlipped,true);
        else
            GameManager.instance.HelicopterDestroyed(isFlipped,false);

        GameManager.instance.UpdateScore(10);
    }

    IEnumerator SpawnParatrooper(int spawnCount)
    {
        while(spawnCount-- > 0)
        {
            yield return new WaitForSeconds(UnityEngine.Random.Range(0.3f, 2));
            SpawnTrooper();
        }
        yield break;
    }


}
