using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerDataManager 
{
    public static PlayerDataManager instance;
    private static readonly object _lock = new object();
    public static PlayerDataManager Instance
    {
        get
        {
            if (instance == null)
            {
                lock (_lock)
                {
                    if (instance == null )
                    {
                        instance = new PlayerDataManager();
                    }
                }
            }
            return instance;
        }
    }

    private PlayerDataManager() { }

    private const string HealthKey = "PlayerHealth";
    private const string DamageKey = "PlayerDamage";
    private const string AttackSpeedKey = "PlayerAttackSpeed";
    private const string MoveSpeedKey = "PlayerMoveSpeed";
    private const string ArmorKey = "PlayerArmor";

    public float Health
    {
        get => PlayerPrefs.GetFloat(HealthKey, 100f);
        set => PlayerPrefs.SetFloat(HealthKey, value);
    }

    public float Damage
    {
        get => PlayerPrefs.GetFloat(DamageKey, 10f);
        set => PlayerPrefs.SetFloat(DamageKey, value);
    }

    public float AttackSpeed
    {
        get => PlayerPrefs.GetFloat(AttackSpeedKey, 1f);
        set => PlayerPrefs.SetFloat(AttackSpeedKey, value);
    }

    public float MoveSpeed
    {
        get => PlayerPrefs.GetFloat(MoveSpeedKey, 5f);
        set => PlayerPrefs.SetFloat(MoveSpeedKey, value);
    }

    public float Armor
    {
        get => PlayerPrefs.GetFloat(ArmorKey, 1f);
        set => PlayerPrefs.SetFloat(ArmorKey, value);
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.Save();
    }
}
