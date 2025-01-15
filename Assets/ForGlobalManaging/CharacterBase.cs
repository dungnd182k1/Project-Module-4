using UnityEngine;

public class CharacterBase : MonoBehaviour, IAttackable, IBeAttackedable
{
    public virtual int _damage => 0;

    public virtual void BeAttacked(int damage) { }

}