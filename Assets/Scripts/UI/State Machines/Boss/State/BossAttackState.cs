using UnityEngine;

namespace Com.Studio.Zomclick.Assets.Scripts.UI.StateMachines.Boss.State {
    public class BossAttackState : BossBaseState
    {
        const string ATTACK_TRIGGER = "Attack";
        public float attackTimer = 0f;
        Animator animator;

        public override void CheckSwitchStates(BossStateManager bossContext)
        {
            // throw new System.NotImplementedException();
        }

        public override void EnterState(BossStateManager bossContext)
        {
            animator = bossContext.GetComponent<Animator>();
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
            attackTimer -= Time.deltaTime;

            if (attackTimer <= 0f)
            {
                OnAttack(bossContext);
            }
        }

        private void OnAttack(BossStateManager bossContext)
        {
            animator.SetTrigger(ATTACK_TRIGGER);
            bossContext.bossViewModel.InflictDamage();
            attackTimer = bossContext.bossViewModel.bossStats.attackSpeed;
        }
    }
}