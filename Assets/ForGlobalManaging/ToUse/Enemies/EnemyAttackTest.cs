using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAttackTest : EnemyBehaviourBase
{

    public float stopAttackDistance = 2.5f;

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        LookAtPlayer(animator.transform);

        float distanceToPlayer = Vector3.Distance(player.position, animator.transform.position);
        if (distanceToPlayer >= stopAttackDistance)
        {
                animator.SetBool("isAttacking", false);
        }
    }

    private void LookAtPlayer(Transform _transform)
    {
        Vector3 direction = player.position - _transform.position;
        _transform.rotation = Quaternion.LookRotation(direction);

        var yRotaion = _transform.eulerAngles.y;
        _transform.rotation = Quaternion.Euler(0, yRotaion, 0);

    }
}
