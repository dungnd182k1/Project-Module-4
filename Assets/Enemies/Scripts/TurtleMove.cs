using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TurtleMove : StateMachineBehaviour
{

    Transform player;

    // NavMeshAgent agent;

    public float chasingSpeed = 1f;

    // public float stopChasingDistance = 32f;

    public float attackDistance = 2.5f;


    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        // agent = animator.GetComponent<NavMeshAgent>();

        // agent.speed = chasingSpeed;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // if (!SoundManager.Instance.zombieChannel2.isPlaying)
        // {
        //     SoundManager.Instance.zombieChannel2.PlayOneShot(SoundManager.Instance.zombieChase);
        // }

        // agent.SetDestination(player.position);
        animator.transform.position = Vector3.MoveTowards(animator.transform.position, player.position, chasingSpeed * Time.deltaTime);
        animator.transform.LookAt(player);

        float distanceToPlayer = Vector3.Distance(player.position, animator.transform.position);

        // if ((distanceToPlayer >= stopChasingDistance) &&  !SoundManager.Instance.shootingChannel.isPlaying)
        // {
        //     animator.SetBool("isChasing", false);
        // }

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
