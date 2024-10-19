using UnityEngine;
using UnityEngine.UIElements;

namespace Com.Studio.Zomclick.Assets.Scripts.UI.StateMachines.Ally.State {
    public class AllySpawnState : AllyBaseState
    {

        public override void EnterState(AllyStateManager allyContext)
        {
            allyContext.transform.Find("zombie_sprite_sheet_0").gameObject.SetActive(true);
        }

        public override void OnCollisionEnter2D(AllyStateManager allyContext, Collision2D collision)
        {
        }

        public override void UpdateState(AllyStateManager allyContext)
        {
        }

        public override void ExitState(AllyStateManager allyContext)
        {;
        }

        public override void CheckSwitchStates(AllyStateManager allyContext)
        {
        
        }

        public override void InitaializeSubState(AllyStateManager allyContext)
        {
        }
    }
}