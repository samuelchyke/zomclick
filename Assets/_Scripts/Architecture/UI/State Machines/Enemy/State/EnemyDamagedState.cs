using UnityEngine;

public class EnemyDamagedState : EnemyBaseState
{

    public override void EnterState(EnemyStateManager enemyContext)
    {
        // Debug.Log(currentHealth);
        // currentHealth -= enemyViewModel.TakeDamage();
        // Debug.Log(currentHealth);
        // if (currentHealth <= 0) 
        // { 
        //     enemyContext.SwitchState(enemyContext.deadState);  
        // }
        // else 
        // {
        //     // enemyContext.ReturnToPreviousState();
        // }
        
    }

    public override void OnCollisionEnter2D(EnemyStateManager enemyContext, Collision2D collision)
    {
        
    }

    public override void UpdateState(EnemyStateManager enemyContext)
    {

    }
}