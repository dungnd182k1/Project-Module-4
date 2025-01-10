using UnityEngine;

#region Interfaces
public interface IAttackable
{
    int damage { get; }
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
    Transform transform { get; }
}

public interface IOnGameStates
{
    void OnGameStart(params object[] parameter);
    void OnGameRunning();
    void OnStageStart();
    void OnStageOver();
    void OnGameOver();
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