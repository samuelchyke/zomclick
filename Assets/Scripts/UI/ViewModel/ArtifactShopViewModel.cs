using System;
using UnityEngine;
using Zenject;
using R3;
using System.Collections.Generic;
using Com.Studio.Zomclick.Assets.Scripts.Repositories.Models;
using Com.Studio.Zomclick.Assets.Scripts.UI.Events;
using Com.Studio.Zomclick.Assets.Scripts.Domain.UseCases.Artifact;

namespace Com.Studio.Zomclick.Assets.Scripts.UI.ViewModel {
    public interface IArtifactShopViewModel
    {
        ReadOnlyReactiveProperty<ArtifactShopDetails> artifactsShopDetails { get; }
        ReadOnlyReactiveProperty<List<Artifact>> artifacts { get; }

        void UpgradeArtifact(string artifactId);
        void UnlockArtifact();
    }

    public class ArtifactShopViewModelImpl : IArtifactShopViewModel, IInitializable, IDisposable
    {
        private CompositeDisposable _disposables = new CompositeDisposable();

        readonly IReadArtifactShopDetailsUseCase readArtifactShopDetailsUseCase;
        readonly IReadUnlockedArtifactsUseCase readUnlockedArtifactsUseCase;
        readonly IReadArtifactUseCase readArtifactUseCase;
        readonly IUnlockArtifactUseCase unlockArtifactUseCase;
        readonly IUpgradeArtifactUseCase upgradeArtifactUseCase;
    
        readonly EventsManager eventsManager;

        [Inject]
        public ArtifactShopViewModelImpl(
            IReadArtifactShopDetailsUseCase readArtifactShopDetailsUseCase,
            IReadUnlockedArtifactsUseCase readUnlockedArtifactsUseCase,
            IReadArtifactUseCase readArtifactUseCase,
            IUnlockArtifactUseCase unlockArtifactUseCase,
            IUpgradeArtifactUseCase upgradeArtifactUseCase,
            EventsManager eventsManager
            )
        {
            this.readArtifactShopDetailsUseCase = readArtifactShopDetailsUseCase;
            this.readArtifactUseCase = readArtifactUseCase;
            this.readUnlockedArtifactsUseCase = readUnlockedArtifactsUseCase;
            this.unlockArtifactUseCase = unlockArtifactUseCase;
            this.upgradeArtifactUseCase = upgradeArtifactUseCase;
            this.eventsManager = eventsManager;
        }

        ReactiveProperty<ArtifactShopDetails> _artifactsShopDetails = new ();
        public ReadOnlyReactiveProperty<ArtifactShopDetails> artifactsShopDetails => _artifactsShopDetails;

        ReactiveProperty<List<Artifact>> _artifacts = new ();
        public ReadOnlyReactiveProperty<List<Artifact>> artifacts => _artifacts;

        public async void Initialize()
        {
            _artifactsShopDetails.Value = await readArtifactShopDetailsUseCase.Invoke();
            _artifacts.Value = await readUnlockedArtifactsUseCase.Invoke();

            Debug.Log("Shop ViewModel Initialized");
            eventsManager.TriggerEvent(GameEvent.PlayerShopViewModelEvent.SHOP_VM_SETUP_COMPLETE);
            eventsManager.StartListening(GameEvent.PlayerSkillViewModelEvent.ON_MIDAS_ROUNDS_HIT, UpdateShopDetails);
            eventsManager.StartListening(GameEvent.EnemyViewModelEvent.ON_DEATH, UpdateShopDetails);
            eventsManager.StartListening(GameEvent.BossViewModelEvent.ON_DEATH, UpdateShopDetails);
            eventsManager.StartListening(GameEvent.AllyShopViewModelEvent.UPDATE_ALLIES, UpdateShopDetails);
        }

        async void UpdateShopDetails()
        {
            _artifactsShopDetails.Value = await readArtifactShopDetailsUseCase.Invoke();
        }

        public async void UpgradeArtifact(string artifactId)
        {
            await upgradeArtifactUseCase.Invoke(artifactId);
            UpdateShopDetails();
            // eventsManager.TriggerEvent(GameEvent.PlayerShopViewModelEvent.UPDATE_PLAYER_STATS);
        }

        public async void UnlockArtifact()
        {
            await unlockArtifactUseCase.Invoke();
            UpdateArtifacts();
            UpdateShopDetails();
            // eventsManager.TriggerEvent(GameEvent.PlayerShopViewModelEvent.UPDATE_PLAYER_SKILL, playerSkillId);
            // eventsManager.TriggerEvent(GameEvent.PlayerShopViewModelEvent.UNLOCK_PLAYER_SKILL, playerSkillId);
        }

        async void UpdateArtifacts()
        {
            _artifacts.Value = await readUnlockedArtifactsUseCase.Invoke();
            // eventsManager.TriggerEvent(GameEvent.AllyShopViewModelEvent.UPDATE_ALLIES);
        }

        public void Dispose()
        {
            _disposables.Dispose();
        }

        public void Cleanup()
        {
            // eventsManager.StopListening(GameEvent.EnemyViewModelEvent.ON_DEATH, UpdateShopDetails);
            // eventsManager.StopListening(GameEvent.BossViewModelEvent.ON_DEATH, UpdateShopDetails);
            // eventsManager.StopListening(GameEvent.AllyShopViewModelEvent.UPDATE_ALLIES, UpdateShopDetails);
        }
    }
}