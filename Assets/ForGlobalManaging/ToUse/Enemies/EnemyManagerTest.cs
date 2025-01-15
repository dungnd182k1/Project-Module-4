using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManagerTest : MonoBehaviour, IOnGameStates, IOnEnemyDie
{

    public int currentRound;
    public int waveInRound;
    public int currentWave;

    public int enemyInWave;
    public int enemyAlive;

    public GameObject[] enemyPrefabs;
    public Vector3 spawnPositionAbove;
    public Vector3 spawnPositionBelow;
    public float spawnDistance = 20f;
    public float timeBetweenSpawns = 0.5f;
    public float timeBetweenWaves = 5f;

    ITransformGettable player;
    List<IOnEnemyDie> dieDependencies;
    bool roundIsOver;

    public void OnGameStart(params object[] parameter)
    {
        foreach (var obj in parameter)
        {
            if (player != null && dieDependencies != null)
            {
                break;
            }
            if (obj is ITransformGettable playerInstance)
            {
                player = playerInstance;
            }
            if (obj is List<IOnEnemyDie> onEnemyDieInstances)
            {
                dieDependencies = onEnemyDieInstances;
            }
        }
    }

    public void OnStageStart()
    {
        roundIsOver = false;
        StartCoroutine(NextWave());
    }

    //Hàm spawn enemy ở một khoảng cánh so với player
    private IEnumerator SpawnEnemiesAbove()
    {
        spawnPositionAbove = player._transform.position + new Vector3(0, 0, spawnDistance);
        
        for (int i = 0; i < enemyInWave; i++)
        {
            Vector3 randomOffset = new Vector3(Random.Range(-spawnDistance / 4, spawnDistance / 4), 0, 0);
            GameObject enemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
            GameObject spawnedObj = Instantiate(enemyPrefab, spawnPositionAbove + randomOffset, Quaternion.identity);
            spawnedObj.GetComponent<IOnGameStates>().OnGameStart(player, dieDependencies);
            yield return new WaitForSeconds(timeBetweenSpawns);
        }
    }

    private IEnumerator SpawnEnemiesBelow()
    {
        spawnPositionBelow = player._transform.position - new Vector3(0, 0, spawnDistance);

        for (int i = 0; i < enemyInWave; i++)
        {
            Vector3 randomOffset = new Vector3(Random.Range(-spawnDistance / 4, spawnDistance / 4), 0, 0);
            GameObject enemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
            GameObject spawnedObj = Instantiate(enemyPrefab, spawnPositionBelow + randomOffset, Quaternion.identity);
            spawnedObj.GetComponent<IOnGameStates>().OnGameStart(player, dieDependencies);
            yield return new WaitForSeconds(timeBetweenSpawns);
        }
    }

    public void OnGameRunning()
    {
        if (roundIsOver)
        {
            return;
        }
        //Hàm respawn enemy khi enemy quá xa player
    }

    //Hàm kiểm tra xem toàn bộ enemy trong wave đã chết hết chưa, nếu hết rồi thì chuyển wave hoặc chuyển round nếu đã là wave cuối
    public void OnEnemyDie()
    {
        enemyAlive--;
        if (currentWave == waveInRound)
        {
            GameManager.instance.SetGameState(GameState.StageOver);
            return;
        }
        if (enemyAlive == 0)
        {
            StartCoroutine(NextWave());
        }
    }

    public void OnStageOver()
    {
        roundIsOver = true;
    }

    public void OnGameOver()
    {
        roundIsOver = true;
    }

    //Hàm chuyển wave
    private IEnumerator NextWave()
    {
        enemyAlive = enemyInWave * 2;
        yield return new WaitForSeconds(timeBetweenWaves);
        currentWave++;
        StartCoroutine(SpawnEnemiesAbove());
        StartCoroutine(SpawnEnemiesBelow());
    }
}
