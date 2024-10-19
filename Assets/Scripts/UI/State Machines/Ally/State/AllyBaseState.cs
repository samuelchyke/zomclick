using System.Linq.Expressions;
using UnityEngine;
using Zenject;

namespace Com.Studio.Zomclick.Assets.Scripts.UI.StateMachines.Ally.State {
    public abstract class AllyBaseState : IInitializable
    {

        // GameObject projectilePrefab;
        // PlayerStateFactory playerStateFactory;
        // IPlayerViewModel playerViewModel;
        // DiContainer _container;

        // [Inject] 
        // public PlayerBaseState (
        //     GameObject projectilePrefab,
        //     PlayerStateFactory playerStateFactory,
        //     IPlayerViewModel playerViewModel,
        //     DiContainer _container
        //     )
        // {
        //     this.projectilePrefab = projectilePrefab;
        //     this.playerStateFactory = playerStateFactory;
        //     this.playerViewModel = playerViewModel;
        //     this._container = _container;
        // }



        public void Initialize()
        {
            
        }

        public abstract void EnterState(AllyStateManager playerContext);

        public abstract void UpdateState(AllyStateManager playerContext);

        public abstract void OnCollisionEnter2D(AllyStateManager playerContext, Collision2D collision);

        public abstract void ExitState(AllyStateManager playerContext);

        public abstract void CheckSwitchStates(AllyStateManager playerContext);

        public abstract void InitaializeSubState(AllyStateManager playerContext);

        protected void UpdateStates(){}

        protected void SwitchStates(AllyStateManager playerContext, AllyBaseState newState){
            ExitState(playerContext);

            playerContext.currentState = newState;

            newState.EnterState(playerContext);
        }

        protected void SetSuperState(){}

        protected void SetSubState(){}
    }

    public class AllyStateFactory
    {
        // readonly DiContainer container;
        // PlayerStateManager projectilePrefab;
        // GameObject projectilePrefab;
        // PlayerStateFactory playerStateFactory;
        // IPlayerViewModel playerViewModel;
        DiContainer container;

        // [Inject]
        public AllyStateFactory(
            // GameObject projectilePrefab,
            // PlayerStateFactory playerStateFactory,
            // IPlayerViewModel playerViewModel,
            DiContainer container
            )
        {
            // this.projectilePrefab = projectilePrefab;
            // this.playerStateFactory = playerStateFactory;
            // this.playerViewModel = playerViewModel;
            this.container = container;
        }

        public T Create<T>() where T : AllyBaseState
        {
            return container.Instantiate<T>();
        }

        public AllySpawnState CreateSpawnState()
        {
            return Create<AllySpawnState>();
        }

        public AllyAttackState CreateAttackState()
        {
            return Create<AllyAttackState>();
        }
    }

}