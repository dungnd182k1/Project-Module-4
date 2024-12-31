using UnityEngine;

[CreateAssetMenu(fileName = "PlayerDataConfig", menuName = "Configs/Player Data Config")]
public class PlayerDataConfig : ScriptableObject
{
    [Header("Player Stats")]
    [Tooltip("Máu của nhân vật")]
    public float health = 100;

    [Tooltip("Tốc độ tấn công của nhân vật (đơn vị: lần/giây)")]
    public float attackSpeed = 1.0f;

    [Tooltip("Tốc độ di chuyển của nhân vật (đơn vị: m/s)")]
    public float moveSpeed = 15.0f;

    [Tooltip("Sát thương của nhân vật")]
    public float damage = 10;

    [Tooltip("Giáp của nhân vật (giảm sát thương nhận vào)")]
    public float armor = 1;

    /// <summary>
    /// Tính toán sát thương thực nhận dựa trên giáp.
    /// </summary>
    /// <param name="incomingDamage">Sát thương nhận vào</param>
    /// <returns>Sát thương thực nhận sau khi trừ giáp</returns>
    public float CalculateDamageTaken(float incomingDamage)
    {
        float reducedDamage = Mathf.Max(incomingDamage - armor, 0);
        return reducedDamage;
    }
}
