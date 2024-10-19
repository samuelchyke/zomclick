using UnityEngine;

namespace Com.Studio.Zomclick.Assets.Scripts.UI.StateMachines.Boss.State {
    public class BossWalkState : BossBaseState
    {
        public override void EnterState(BossStateManager bossContext)
        {

        }

        public override void UpdateState(BossStateManager bossContext)
        {
            bossContext.transform.position += 0.5f * Time.deltaTime * -bossContext.transform.right;
        }

        public override void OnCollisionEnter2D(BossStateManager bossContext, Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Wall"))
            {
                bossContext.SwitchState(bossContext.attackState);
            }
        }

        public override void ExitState(BossStateManager bossContext)
        {
            // throw new System.NotImplementedException();
        }

        public override void CheckSwitchStates(BossStateManager bossContext)
        {
            // throw new System.NotImplementedException();
        }

        public override void InitaializeSubState(BossStateManager bossContext)
        {
            // throw new System.NotImplementedException();
        }
    }
}