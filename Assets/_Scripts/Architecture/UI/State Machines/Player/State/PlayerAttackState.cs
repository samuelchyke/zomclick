using UnityEngine;
using UnityEngine.UIElements;


public class PlayerAttackState : PlayerBaseState
{
    const string ATTACK_TRIGGER = "Attack";
    // Animator animator;

    

    public override void EnterState(PlayerStateManager playerContext)
    {
        // playerContext.FireProjectile();
        FireProjectile(playerContext);
        // animator = playerContext.GetComponent<Animator>(); 
        // FireProjectile(playerContext);
        // playerContext.SwitchState(playerContext.idleState);
    }

    public override void ExitState(PlayerStateManager playerContext)
    {
        
    }

    public override void InitaializeSubState(PlayerStateManager playerContext)
    {

    }

    public override void CheckSwitchStates(PlayerStateManager playerContext)
    {
        if (Input.GetButtonUp("Jump")) 
        {
            SwitchStates(playerContext, playerContext.idleState);
        }
    }

    public override void OnCollisionEnter2D(PlayerStateManager playerContext, Collision2D collision)
    {

    }

    public override void UpdateState(PlayerStateManager playerContext)
    {
        CheckSwitchStates(playerContext);
    }

    private void FireProjectile(PlayerStateManager playerContext)
    {
        
        playerContext.animator.SetTrigger(ATTACK_TRIGGER);
        var projectile = playerContext._container.InstantiatePrefab(playerContext.projectilePrefab, playerContext.launchOffest.position, playerContext.launchOffest.rotation, null);
        projectile.GetComponent<Projectile>();
        // playerContext.animator.SetTrigger(ATTACK_TRIGGER);
        // // playerContext.Instas();
        // playerContext.pto.GetComponent<AutoAimProjectile>();
    }
}