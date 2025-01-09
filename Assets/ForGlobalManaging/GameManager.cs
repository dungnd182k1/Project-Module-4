using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField]
    GameObject player;
    IOnEnemyDie enemyDieDependency;
    ITransformGettable transformProvider;

    private void OnEnable()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnDisable()
    {
        instance = null;
    }

    private void Start()
    {
        enemyDieDependency = player.GetComponent<IOnEnemyDie>();
        transformProvider = player.GetComponent<ITransformGettable>();
        //chạy hàm khởi tạo của enemies spawner trong đó
            //có gọi hàm khởi tạo của enemies,
            //nhận tham số kiểu IOnEnemyDie và ITransformGettable
    }

    public void OnAttack(CharacterBase attacker, CharacterBase damageTaker)
    {
        if (damageTaker != null && attacker != null)
        {
            damageTaker.BeAttacked(attacker.damage);
        }
    }

}