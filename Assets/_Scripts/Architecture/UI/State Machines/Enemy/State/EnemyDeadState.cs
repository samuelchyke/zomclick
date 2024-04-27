using UnityEngine;

public class EnemyDeadState : EnemyBaseState
{
    public override void EnterState(EnemyStateManager enemyContext)
    {
        enemyContext.enemyViewModel.OnDeath();
        Object.Destroy(enemyContext.gameObject);
    }

    public override void OnCollisionEnter2D(EnemyStateManager enemyContext, Collision2D collision)
    {

    }

    public override void UpdateState(EnemyStateManager enemyContext)
    {

    }
}