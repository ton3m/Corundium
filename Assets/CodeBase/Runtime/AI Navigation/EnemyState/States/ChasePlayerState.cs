using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasePlayer : MonoBehaviour, IEnemyState
{
    private readonly EnemyStateMachine _enemyStateMachine;

    public ChasePlayer(EnemyStateMachine enemyStateMachine)
    {
        _enemyStateMachine = enemyStateMachine;
    }

    public void EnterState()
    {
        
    }

    public void ExitState()
    {
        
    }
}
