using System;
using UnityEngine;
using Zenject;
using R3;
using Com.Studio.Zomclick.Assets.Scripts.Repositories.Models;
using Com.Studio.Zomclick.Assets.Scripts.Domain.UseCases.PlayerSkills;
using Com.Studio.Zomclick.Assets.Scripts.UI.Events;
using Com.Studio.Zomclick.Assets.Scripts.Data.Core.Enums;

namespace Com.Studio.Zomclick.Assets.Scripts.UI.ViewModel {
    public interface IPlayerSkillsViewModel
    {
        ReadOnlyReactiveProperty<PlayerSkill> bigBetty { get; }
        ReadOnlyReactiveProperty<PlayerSkill> turret { get; }
        ReadOnlyReactiveProperty<PlayerSkill> lightningRounds { get; }
        ReadOnlyReactiveProperty<PlayerSkill> rallyAllies { get; }
        ReadOnlyReactiveProperty<PlayerSkill> incendiaryRounds { get; }
        ReadOnlyReactiveProperty<PlayerSkill> midasRounds { get; }

        void ToggleIsSkillActive(string playerSkillId);

        void IncreasePlayerGold();
    }

    public class PlayerSkillsViewModelImpl : IPlayerSkillsViewModel, IInitializable, IDisposable
    {
        private CompositeDisposable _disposables = new CompositeDisposable();

        readonly IReadPlayerSkillUseCase readPlayerSkillUseCase;
        readonly IToggleSkillActiveUseCase toggleSkillActiveUseCase;
        readonly IIncreasePlayerGoldUseCase increasePlayerGoldUseCase;
    
        readonly EventsManager eventsManager;

        [Inject]
        public PlayerSkillsViewModelImpl(
            IReadPlayerSkillUseCase readPlayerSkillUseCase,
            IToggleSkillActiveUseCase toggleSkillActiveUseCase,
            IIncreasePlayerGoldUseCase increasePlayerGoldUseCase,
            EventsManager eventsManager
            )
        {
            this.readPlayerSkillUseCase = readPlayerSkillUseCase;
            this.toggleSkillActiveUseCase = toggleSkillActiveUseCase;
            this.increasePlayerGoldUseCase = increasePlayerGoldUseCase;
            this.eventsManager = eventsManager;
        }

        private ReactiveProperty<PlayerShopDetails> _shopDetails = new ();
        public ReadOnlyReactiveProperty<PlayerShopDetails> shopDetails => _shopDetails;

        private ReactiveProperty<PlayerSkill> _bigBetty = new ();
        public ReadOnlyReactiveProperty<PlayerSkill> bigBetty => _bigBetty;

        private ReactiveProperty<PlayerSkill> _turret = new ();
        public ReadOnlyReactiveProperty<PlayerSkill> turret => _turret;

        private ReactiveProperty<PlayerSkill> _lightningRounds = new ();
        public ReadOnlyReactiveProperty<PlayerSkill> lightningRounds => _lightningRounds;

        private ReactiveProperty<PlayerSkill> _rallyAllies = new ();
        public ReadOnlyReactiveProperty<PlayerSkill> rallyAllies => _rallyAllies;

        private ReactiveProperty<PlayerSkill> _incendiaryRounds = new ();
        public ReadOnlyReactiveProperty<PlayerSkill> incendiaryRounds => _incendiaryRounds;

        private ReactiveProperty<PlayerSkill> _midasRounds = new ();
        public ReadOnlyReactiveProperty<PlayerSkill> midasRounds => _midasRounds;

        public async void Initialize()
        {
            _bigBetty.Value = await readPlayerSkillUseCase.Invoke(Skill.BigBetty.id());
            _turret.Value = await readPlayerSkillUseCase.Invoke(Skill.Turret.id());
            _lightningRounds.Value = await readPlayerSkillUseCase.Invoke(Skill.LightningRounds.id());
            _rallyAllies.Value = await readPlayerSkillUseCase.Invoke(Skill.RallyAllies.id());
            _incendiaryRounds.Value = await readPlayerSkillUseCase.Invoke(Skill.IncendiaryRounds.id());
            _midasRounds.Value = await readPlayerSkillUseCase.Invoke(Skill.MidasRounds.id());

            Debug.Log("Player Skills ViewModel Initialized");
            eventsManager.TriggerEvent(GameEvent.PlayerSkillViewModelEvent.PLAYER_SKILL_VM_SETUP_COMPLETE);
            eventsManager.StartListening(GameEvent.PlayerShopViewModelEvent.UPDATE_PLAYER_SKILL, UpdatePlayerSkill);
        }

        public async void ToggleIsSkillActive(string playerSkillId)
        {
            await toggleSkillActiveUseCase.Invoke(playerSkillId);
            UpdatePlayerSkill(playerSkillId);
        }

        public async void IncreasePlayerGold()
        {
            await increasePlayerGoldUseCase.Invoke();
            eventsManager.TriggerEvent(GameEvent.PlayerSkillViewModelEvent.ON_MIDAS_ROUNDS_HIT);
        }

        async void UpdatePlayerSkill(string playerSkillId)
        {
            switch(playerSkillId) 
            {
                case "big_betty_id":
                    _bigBetty.Value = await readPlayerSkillUseCase.Invoke(playerSkillId);
                    break;
                case "turret_id":
                    _turret.Value = await readPlayerSkillUseCase.Invoke(playerSkillId);
                    break;
                case "lightning_rounds_id":
                    _lightningRounds.Value = await readPlayerSkillUseCase.Invoke(playerSkillId);
                    break;
                case "rally_allies_id":
                    _rallyAllies.Value = await readPlayerSkillUseCase.Invoke(playerSkillId);
                    break;
                case "incendiary_rounds_id":
                    _incendiaryRounds.Value = await readPlayerSkillUseCase.Invoke(playerSkillId);
                    break;
                case "midas_rounds_id":
                    _midasRounds.Value = await readPlayerSkillUseCase.Invoke(playerSkillId);
                    break;
            };
        }

        // async void UnlockPlayerSkill(string playerSkillId)
        // {
        //     switch(playerSkillId) 
        //     {
        //         case "big_betty_id":
        //             _bigBetty.Value = await readPlayerSkillUseCase.Invoke(playerSkillId);
        //             break;
        //         case "turret_id":
        //             _turret.Value = await readPlayerSkillUseCase.Invoke(playerSkillId);
        //             break;
        //         case "lightning_rounds_id":
        //             _lightningRounds.Value = await readPlayerSkillUseCase.Invoke(playerSkillId);
        //             break;
        //         case "rally_allies_id":
        //             _rallyAllies.Value = await readPlayerSkillUseCase.Invoke(playerSkillId);
        //             break;
        //         case "incendiary_rounds_id":
        //             _incendiaryRounds.Value = await readPlayerSkillUseCase.Invoke(playerSkillId);
        //             break;
        //         case "midas_rounds_id":
        //             _midasRounds.Value = await readPlayerSkillUseCase.Invoke(playerSkillId);
        //             break;
        //     };
        // }

        public void Dispose()
        {
            _disposables.Dispose();
        }

        public void Cleanup()
        {
            eventsManager.StopListening(GameEvent.PlayerShopViewModelEvent.UPDATE_PLAYER_SKILL, UpdatePlayerSkill);
        }
    }
}