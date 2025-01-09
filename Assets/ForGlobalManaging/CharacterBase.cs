using UnityEngine;

public class CharacterBase : MonoBehaviour, IAttackable, IBeAttackedable
{
    public virtual int damage => 0;

    public virtual void BeAttacked(int damage) { }
}