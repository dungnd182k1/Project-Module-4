using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTest : CharacterBase, IOnGameStates
{
    [SerializeField] private int HP;

    [SerializeField] private int damage; //Sát thưởng của quái gây lên người chơi

    [SerializeField] private int exp; //Kinh nghiệm rớt ra khi quái chết
    private Animator animator;

    public bool isDead;

    ITransformGettable playerInstance;
    List<IOnEnemyDie> dieDependencies;

    public void OnGameStart(params object[] parameter)
    {
        foreach (var obj in parameter)
        {
            if (playerInstance != null && dieDependencies != null)
            {
                break;
            }
            if (obj is ITransformGettable player)
            {
                playerInstance = player;
            }
            if (obj is List<IOnEnemyDie> onEnemyDieInstances)
            {
                dieDependencies = onEnemyDieInstances;
            }
        }
        animator = GetComponent<Animator>();
        foreach (EnemyBehaviourBase behavior in animator.GetBehaviours<EnemyBehaviourBase>())
        {
            behavior.SetPlayerTransform(playerInstance._transform);
        }
    }

    public override int _damage => damage;

    public override void BeAttacked(int damageAmount)
    {
        if (isDead){
            return;
        }

        HP -= damageAmount;

        if (HP <= 0)
        {
            foreach (IOnEnemyDie dependency in dieDependencies)
            {
                dependency.OnEnemyDie();
            }
            animator.SetTrigger("isDead");
        } else {
            animator.SetTrigger("damaged");
            // Viết hàm hiện damage ở đây
        }
    }
}
