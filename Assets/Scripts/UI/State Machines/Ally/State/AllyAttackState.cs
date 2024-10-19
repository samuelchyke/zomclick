using UnityEngine;
using UnityEngine.UIElements;
using System.Collections;
using Com.Studio.Zomclick.Assets.Scripts.UI.StateMachines.Player;

namespace Com.Studio.Zomclick.Assets.Scripts.UI.StateMachines.Ally.State {
    public class AllyAttackState : AllyBaseState
    {
        const string ATTACK_TRIGGER = "Attack";
        // Animator animator;

        

        public override void EnterState(AllyStateManager allyContext)
        {
            allyContext.StartCoroutine(FireProjectile(allyContext));
            // playerContext.FireProjectile();
            
            // animator = playerContext.GetComponent<Animator>(); 
            // FireProjectile(playerContext);
            // playerContext.SwitchState(playerContext.idleState);
        }

        public override void ExitState(AllyStateManager playerContext)
        {
            
        }

        public override void InitaializeSubState(AllyStateManager playerContext)
        {

        }

        public override void CheckSwitchStates(AllyStateManager allyContext)
        {
            // if (Input.GetButtonUp("Jump")) 
            // {
            //     SwitchStates(allyContext, allyContext.idleState);
            // }
        }

        public override void OnCollisionEnter2D(AllyStateManager allyContext, Collision2D collision)
        {

        }

        public override void UpdateState(AllyStateManager allyContext)
        {
            CheckSwitchStates(allyContext);
        }

        // private void FireProjectile(AllyStateManager allyContext)
        // {
            
        //     allyContext.animator.SetTrigger(ATTACK_TRIGGER);
        //     // var projectile = allyContext._container.InstantiatePrefab(allyContext.projectilePrefab, allyContext.launchOffest.position, allyContext.launchOffest.rotation, null);
        //     // projectile.GetComponent<Projectile>();
        //     // playerContext.animator.SetTrigger(ATTACK_TRIGGER);
        //     // // playerContext.Instas();
        //     // playerContext.pto.GetComponent<AutoAimProjectile>();
        // }

        private void StartSpawnBoss()
        {
            
        }

        IEnumerator FireProjectile(AllyStateManager allyContext)
        {
            var rallyAllies = allyContext.playerSkillsViewModel.rallyAllies.CurrentValue;
            var attackSpeed = 1f;
            if(rallyAllies.isActive)
            {
                attackSpeed = 0.5f;
            }
            while(true)
            {
                yield return new WaitForSeconds(attackSpeed);

                var projectile = allyContext._container.InstantiatePrefab(allyContext.projectilePrefab, allyContext.transform.position, allyContext.transform.rotation, null);
                projectile.GetComponent<Projectile>();
            }
        }
    }
}