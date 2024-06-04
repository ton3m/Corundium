using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPlayer : IEnemyState
{
    private readonly EnemyInstance _enemyInstance;
    private readonly EnemyStateMachine _enemyStateMachine;
    

    public AttackPlayer(EnemyStateMachine enemyStateMachine, EnemyInstance enemyInstance)
    {
        _enemyInstance = enemyInstance;
        _enemyStateMachine = enemyStateMachine;
    }

    public void EnterState()
    {
        StartAttack();
    }
    
    public void ExitState()
    {
        StopAttack();
    }

    private void StartAttack()
    {
        _enemyInstance.IsCanAttack = true;
        SetAnimation();
    }
    private void StopAttack()
    {
        _enemyInstance.IsCanAttack = false;
        SetAnimation();
    }
    
    private void SetAnimation()
    {
        _enemyStateMachine.Animator.SetBool("AttackEnemy", _enemyInstance.IsCanAttack);
    }
}
