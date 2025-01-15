using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int HP;

    [SerializeField] private int damage; //Sát thưởng của quái gây lên người chơi

    [SerializeField] private int exp; //Kinh nghiệm rớt ra khi quái chết
    private Animator animator;

    public bool isDead;

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    public void TakeDamage(int damageAmount)
    {
        if (isDead){
            return;
        }

        HP -= damageAmount;

        if (HP <= 0)
        {
            animator.SetTrigger("isDead");
        } else {
            animator.SetTrigger("damaged");
            // Viết hàm hiện damage ở đây
        }
    }
}
