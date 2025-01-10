using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    GameState gameState;
    //biến list chứa instances của interface IOnGameStates

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

    private void Update()
    {
        switch (gameState)
        {
            case GameState.Running:
                //if (Time.timeScale == 0)
                //{
                //    Time.timeScale = 1;
                //}
                return;
            case GameState.Pause:
                //Time.timeScale = 0;
                return;
            //gọi các phương thức tương ứng từ IOnGameStates instances
            case GameState.StageStart:
                return;
            case GameState.StageOver:
                return;
            case GameState.GameOver:
                return;
        }
    }

    public void SetGameState(GameState state)
    {
        gameState = state;
    }

    public void OnAttack(CharacterBase attacker, CharacterBase damageTaker)
    {
        if (damageTaker != null && attacker != null)
        {
            damageTaker.BeAttacked(attacker.damage);
        }
    }

}