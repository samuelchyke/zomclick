using UnityEngine;
using UnityEngine.UIElements;


public class PlayerIdleState : PlayerBaseState
{
    private float timeElapsed;
    const string IDLE_TRIGGER = "Idle";
    // Animator animator;

    public override void EnterState(PlayerStateManager playerContext)
    {
        // animator = playerContext.GetComponent<Animator>(); 
        timeElapsed = 0;
    }

    public override void OnCollisionEnter2D(PlayerStateManager playerContext, Collision2D collision)
    {

    }

    public override void UpdateState(PlayerStateManager playerContext)
    {
        CheckSwitchStates(playerContext);

        if (timeElapsed < 5){
            timeElapsed += Time.deltaTime;
        }else{
            playerContext.animator.SetTrigger(IDLE_TRIGGER);
        }
    }

    public override void ExitState(PlayerStateManager playerContext)
    {
        timeElapsed = 0;
        // throw new System.NotImplementedException();
    }

    public override void CheckSwitchStates(PlayerStateManager playerContext)
    {
        if (Input.GetButtonDown("Jump")) 
        {
            SwitchStates(playerContext, playerContext.attackState);
        }
    }

    public override void InitaializeSubState(PlayerStateManager playerContext)
    {
        // throw new System.NotImplementedException();
    }
}