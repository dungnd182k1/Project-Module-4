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
#endregion
#region Enums
public enum GameState
{
    Running,
    Pause,
    StageStart,
    StageOver,
    GameOver
}
#endregion