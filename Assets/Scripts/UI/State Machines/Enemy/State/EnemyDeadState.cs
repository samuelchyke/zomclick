using UnityEngine;

    namespace Com.Studio.Zomclick.Assets.Scripts.UI.StateMachines.Enemy.State {
    public class EnemyDeadState : EnemyBaseState
    {
        public override void EnterState(EnemyStateManager enemyContext)
        {
            Debug.Log($"Enemy {enemyContext.GetInstanceID()} OnDeath called at time {Time.time}");
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
}