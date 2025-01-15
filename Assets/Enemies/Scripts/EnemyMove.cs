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
    public float maxDistance = 21f; // Khoảng cách tối đa mà quái có thể ở xa người chơi

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

        if (distanceToPlayer > maxDistance)
        {
            // Dịch chuyển quái về gần người chơi
            Vector3 teleportPosition = player.position - direction * (maxDistance - 1f); // Dịch chuyển quái về gần người chơi nhưng vẫn giữ khoảng cách an toàn
            rb.MovePosition(teleportPosition);
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // agent.SetDestination(agent.transform.position);

        // SoundManager.Instance.zombieChannel2.Stop();
    }
}