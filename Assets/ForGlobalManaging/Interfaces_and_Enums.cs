using System;
using UnityEngine;

#region Interfaces
public interface IAttackable
{
    int _damage { get; }
}

public interface IBeAttackedable
{
    void BeAttacked(int damage);
}

public interface IOnEnemyDie
{
    void OnEnemyDie();
}

public interface ITransformGettable
{
    Transform _transform { get; }
}

public interface IMapGenerattable
{
    void GenerateUnits(Func<Vector3> PivotSetter = null);
}

public interface IOnGameStates
{
    void OnGameStart(params object[] parameter);
    void OnGameRunning() { }
    void OnGamePause() { }
    void OnStageStart() { }
    void OnStageOver() { }
    void OnGameOver() { }
}
#endregion
#region Enums
public enum GameState
{
    None,
    Running,
    Pause,
    StageStart,
    StageOver,
    GameOver
}
#endregion