using System;
using System.Collections;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int _score { get; private set; } = 10;
    public static GameManager instance;
    public static event Action PlayerLost;

    [SerializeField] private GameObject _helicopter, _helicopterFlipped;
    [SerializeField] private Transform[] _helicopterSpawnPoints; 
    [SerializeField] private int _helicoptersAtOnce = 2;
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _playerLostText;


    [SerializeField] private int soldierOnLeft = 0,soldierOnRight = 0;
 

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(this);
            return;
        }
        instance = this;
        PlayerLost += PlayerLostBehaviour;

    }

    private void Start()
    {
        StartCoroutine(spawnHelicopter(1f));
        StartCoroutine(spawnHelicopterFlipped(3f));
    }

    public void HelicopterDestroyed(bool wasFlipped,bool finishedTrip)
    {
        if(finishedTrip)
        {
            if(wasFlipped)
                StartCoroutine(spawnHelicopterFlipped(1.0f));
            else
                StartCoroutine(spawnHelicopter(1.0f));

        }
        else
        {
            if (wasFlipped)
                StartCoroutine(spawnHelicopterFlipped(2.5f));
            else
                StartCoroutine(spawnHelicopter(2.5f));

        }
    }

    public void checkPlayerDead()
    {
        if(soldierOnLeft >= 4 || soldierOnRight >= 4 || _score < 0)
        {
            PlayerLost?.Invoke();
        }
    }

    

    public void UpdateScore(int score)
    {
        _score += score;
        _scoreText.text = _score.ToString();
    }

    public void LandSoldier(bool left)
    {
        if(left)
        {
            soldierOnLeft++;
        }
        else
        {
            soldierOnRight++;
        }
        checkPlayerDead();
    }

    public IEnumerator spawnHelicopter(float timeBeforeSpawn)
    {
        yield return new WaitForSeconds(timeBeforeSpawn);
        Instantiate(_helicopter, _helicopterSpawnPoints[1].position, Quaternion.identity);
        yield break;
    }

    public IEnumerator spawnHelicopterFlipped(float timeBeforeSpawn)
    {
        yield return new WaitForSeconds(timeBeforeSpawn);
        Instantiate(_helicopterFlipped, _helicopterSpawnPoints[0].position, Quaternion.identity);
        yield break;
    }

    private void PlayerLostBehaviour()
    {
        StopAllCoroutines();
        _playerLostText.text = $"Your Final Score {_score}";
        _playerLostText.enabled = true;
    }
}
