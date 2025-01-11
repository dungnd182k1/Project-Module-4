using UnityEngine;
[CreateAssetMenu(fileName = "weaponConfig", menuName = "Weapons")]
public class ConfigWeapon : ScriptableObject
{
    public long iD;
    public string nameWeapon;
    public float damage;
    public float rangeAttack;
    public float attackSpeed;

}
