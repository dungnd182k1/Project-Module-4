using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Initializer
{
    List<IOnGameStates> gameElements;
    [SerializeField]
    GameObject[] gameElementObjects;
    List<IOnEnemyDie> enemyDieDependencies;
    ITransformGettable transformProvider;

    public void InjectAllAtGameStart()
    {
        gameElements = new List<IOnGameStates>();
        enemyDieDependencies = new List<IOnEnemyDie>();
        Init();
        InvokeStarts();
    }

    void Init()
    {
        foreach (GameObject obj in gameElementObjects)
        {
            if (obj.TryGetComponent(out ITransformGettable iTransformGettable))
            {
                transformProvider = iTransformGettable;
            }
            if (obj.TryGetComponent(out IOnEnemyDie iOnEnemyDie))
            {
                enemyDieDependencies.Add(iOnEnemyDie);
            }

            gameElements.AddRange(obj.GetComponents<IOnGameStates>());
        }
    }

    void InvokeStarts()
    {
        foreach (IOnGameStates element in gameElements)
        {
            element.OnGameStart(gameElements, enemyDieDependencies, transformProvider);
        }
    }
}
