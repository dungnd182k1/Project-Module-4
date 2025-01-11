using UnityEngine;
[CreateAssetMenu(fileName = "powerBuffConfig", menuName = "PowerBuff")]
public class ConfigPowerBuff : ScriptableObject
{
    public long iD;
    public int level;
    public string namePowerBuff;
    public float damage;
    public float rangeAttack;
    public float attackSpeed;
    public float moveSpeed;
    public float armor;
    public float bloodSucking;

}
