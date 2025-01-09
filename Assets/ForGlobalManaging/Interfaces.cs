using UnityEngine;

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
    Transform GetTransform();
}