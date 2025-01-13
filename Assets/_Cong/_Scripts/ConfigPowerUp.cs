using UnityEngine;
[CreateAssetMenu(fileName = "powerBuffConfig", menuName = "PowerBuff")]
public class ConfigPowerUp : ScriptableObject
{
    public long iD;
    public int level;
    public Sprite iconBuff;
    public string namePowerUp;
    public float damage;
    public float rangeAttack;
    public float attackSpeed;
    public float moveSpeed;
    public float armor;
    public float bloodSucking;
    public string descriptionBuff;
}
