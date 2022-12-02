using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private GameObject _helicopter, _helicopterFlipped;
    [SerializeField] private Transform[] _helicopterSpawnPoints; 
    [SerializeField] private int _score = 10;
    [SerializeField] private int _helicoptersAtOnce = 2;
 

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(this);
        }
        instance = this;
        DontDestroyOnLoad(this);
    }

    public void HelicopterDestroyed(bool wasFlipped,bool finishedTrip)
    {
        if(finishedTrip)
        {
            StartCoroutine(spawnHelicopter(1.0f, wasFlipped));
        }
        else
        {
            StartCoroutine(spawnHelicopter(2.5f, wasFlipped));

        }
    }

    public IEnumerator spawnHelicopter(float timeBeforeSpawn,bool flipped)
    {
        yield return new WaitForSeconds(timeBeforeSpawn);
        if(flipped)
        {
            Instantiate(_helicopterFlipped, _helicopterSpawnPoints[0].position, Quaternion.identity);
        }
        else
        {
            Instantiate(_helicopter, _helicopterSpawnPoints[1].position, Quaternion.identity);

        }
    }    
}
