using UnityEngine;

namespace Com.Studio.Zomclick.Assets.Scripts.UI.StateMachines.Enemy.State {
    public class EnemyWalkState : EnemyBaseState
    {
        public override void EnterState(EnemyStateManager enemyContext)
        {

        }

        public override void UpdateState(EnemyStateManager enemyContext)
        {
            enemyContext.transform.position += 0.5f * Time.deltaTime * - enemyContext.transform.right;

        }

        public override void OnCollisionEnter2D(EnemyStateManager enemyContext, Collision2D collision)
        {
            if(collision.gameObject.CompareTag("Wall"))
            {
                enemyContext.SwitchState(enemyContext.attackState);
            }
        }
    }
}