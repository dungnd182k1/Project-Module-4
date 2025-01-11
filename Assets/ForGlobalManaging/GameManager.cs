using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    GameState gameState;
    IOnGameStates[][] gameElements;
    [SerializeField]
    GameObject[] gameElementObjects;
    IOnEnemyDie enemyDieDependency;
    ITransformGettable transformProvider;

    private void OnEnable()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnDisable()
    {
        instance = null;
    }

    private void Start()
    {
        gameElements = new IOnGameStates[gameElementObjects.Length][];
        Init(out int dependencyIndex);
        InvokeStarts(dependencyIndex);
        SetGameState(GameState.StageStart);
    }

    void Init(out int dependencyIndex)
    {
        dependencyIndex = -1;
        for (int i = 0; i < gameElementObjects.Length; i++)
        {
            if (gameElementObjects[i].TryGetComponent(out IOnEnemyDie iOnEnemyDie)
                && gameElementObjects[i].TryGetComponent(out ITransformGettable iTransformGettable))
            {
                enemyDieDependency = iOnEnemyDie;
                transformProvider = iTransformGettable;
                dependencyIndex = i;
            }
            gameElements[i] = gameElementObjects[i].GetComponents<IOnGameStates>();
        }
    }

    void InvokeStarts(int dependencyIndex)
    {
        for (int i = 0; i < gameElements.Length; i++)
        {
            for (int j = 0; j < gameElements[i].Length; j++)
            {
                if (i == dependencyIndex)
                {
                    gameElements[i][j].OnGameStart();
                }
                else
                {
                    gameElements[i][j].OnGameStart(enemyDieDependency, transformProvider);
                }
            }
        }
    }

    private void Update()
    {
        if (gameState == GameState.None)
        {
            return;
        }

        for (int i = 0; i < gameElements.Length; i++)
        {
            for (int j = 0; j < gameElements[i].Length; j++)
            {
                InvokeGameStates(gameElements[i][j]);
            }
        }
    }

    void InvokeGameStates(IOnGameStates gameElement)
    {
        switch (gameState)
        {
            case GameState.Running:
                gameElement.OnGameRunning();
                return;
            case GameState.Pause:
                Time.timeScale = 0;
                gameElement.OnGamePause();
                SetGameState(GameState.None);
                return;
            case GameState.GameOver:
                gameElement.OnGameOver();
                SetGameState(GameState.None);
                return;
            case GameState.StageStart:
                gameElement.OnStageStart();
                SetGameState(GameState.Running);
                return;
            case GameState.StageOver:
                gameElement.OnStageOver();
                SetGameState(GameState.None);
                return;
        }
    }

    public void SetGameState(GameState state)
    {
        gameState = state;
        if (Time.timeScale == 0 && gameState == GameState.Running)
        {
            Time.timeScale = 1;
        }
    }

    public void OnAttack(CharacterBase attacker, CharacterBase damageTaker)
    {
        if (damageTaker != null && attacker != null)
        {
            damageTaker.BeAttacked(attacker.damage);
        }
    }

}