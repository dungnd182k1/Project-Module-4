using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviourBase : StateMachineBehaviour
{
    protected Transform player;
    public void SetPlayerTransform(Transform playerTransform)
    {
        player = playerTransform;
    }

}
