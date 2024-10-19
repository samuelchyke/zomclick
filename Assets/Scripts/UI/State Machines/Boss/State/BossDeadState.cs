using UnityEngine;

namespace Com.Studio.Zomclick.Assets.Scripts.UI.StateMachines.Boss.State {
    public class BossDeadState : BossBaseState
    {
        public override void CheckSwitchStates(BossStateManager bossContext)
        {
            // throw new System.NotImplementedException();
        }

        public override void EnterState(BossStateManager bossContext)
        {
            bossContext.bossViewModel.OnDeath();
            Object.Destroy(bossContext.gameObject);
        }

        public override void ExitState(BossStateManager bossContext)
        {
            // throw new System.NotImplementedException();
        }

        public override void InitaializeSubState(BossStateManager bossContext)
        {
            // throw new System.NotImplementedException();
        }

        public override void OnCollisionEnter2D(BossStateManager bossContext, Collision2D collision)
        {

        }

        public override void UpdateState(BossStateManager bossContext)
        {

        }
    }
}