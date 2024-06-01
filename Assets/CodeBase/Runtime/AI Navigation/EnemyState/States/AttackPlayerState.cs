using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPlayer : IEnemyState
{
    private readonly EnemyStateMachine _enemyStateMachine;

    public AttackPlayer(EnemyStateMachine enemyStateMachine)
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
