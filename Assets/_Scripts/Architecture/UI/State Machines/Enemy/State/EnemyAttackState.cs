using UnityEngine;
using UnityEngine.UIElements;


public class EnemyAttackState : EnemyBaseState
{
    const string ATTACK_TRIGGER = "Attack";
    public float attackTimer = 0f;
    Animator animator;

    public override void EnterState(EnemyStateManager enemyContext)
    {
        animator = enemyContext.GetComponent<Animator>(); 
    }

    public override void OnCollisionEnter2D(EnemyStateManager enemyContext, Collision2D collision)
    {

    }

    public override void UpdateState(EnemyStateManager enemyContext)
    {
        attackTimer -= Time.deltaTime;

        if (attackTimer <= 0f)
        {
            OnAttack(enemyContext);
        }
    }

    private void OnAttack(EnemyStateManager enemyContext)
    {
        animator.SetTrigger(ATTACK_TRIGGER);
        attackTimer = enemyContext.enemyViewModel.enemyStats.CurrentValue.attackSpeed;
    }
}