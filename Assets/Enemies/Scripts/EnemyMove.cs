using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMove : StateMachineBehaviour
{
    Transform player;
    Rigidbody rb;

    public float moveSpeed = 5f;
    public float attackDistance = 3f;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = animator.GetComponent<Rigidbody>();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Vector3 direction = (player.position - animator.transform.position).normalized;
        Vector3 newPosition = animator.transform.position + direction * moveSpeed * Time.deltaTime;
        rb.MovePosition(newPosition);
        animator.transform.LookAt(player);

        float distanceToPlayer = Vector3.Distance(player.position, animator.transform.position);

        if (distanceToPlayer < attackDistance)
        {
            animator.SetBool("isAttacking", true);
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // agent.SetDestination(agent.transform.position);

        // SoundManager.Instance.zombieChannel2.Stop();
    }
}