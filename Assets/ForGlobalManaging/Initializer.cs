using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Initializer
{
    List<IOnGameStates> gameElements;
    [SerializeField]
    GameObject[] gameElementObjects;
    IOnEnemyDie enemyDieDependency;
    ITransformGettable transformProvider;

    public void InjectAllAtGameStart()
    {
        gameElements = new List<IOnGameStates>();
        Init();
        InvokeStarts();
    }

    void Init()
    {
        foreach (GameObject obj in gameElementObjects)
        {
            if (obj.TryGetComponent(out IOnEnemyDie iOnEnemyDie)
                && obj.TryGetComponent(out ITransformGettable iTransformGettable))
            {
                enemyDieDependency = iOnEnemyDie;
                transformProvider = iTransformGettable;
            }
            gameElements.AddRange(obj.GetComponents<IOnGameStates>());
        }
    }

    void InvokeStarts()
    {
        foreach (IOnGameStates element in gameElements)
        {
            element.OnGameStart(gameElements, enemyDieDependency, transformProvider);
        }
    }
}
