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

            Debug.Log("Arfticat Shop View Model Initialized");
            eventsManager.TriggerEvent(GameEvent.ArtifactShopViewModelEvent.SHOP_VM_SETUP_COMPLETE);
        }

        async void UpdateShopDetails()
        {
            _artifactsShopDetails.Value = await readArtifactShopDetailsUseCase.Invoke();
        }

        public async void UpgradeArtifact(string artifactId)
        {
            await upgradeArtifactUseCase.Invoke(artifactId);
            UpdateShopDetails();
            eventsManager.TriggerEvent(GameEvent.ArtifactShopViewModelEvent.UPDATE_PLAYER_STATS);
        }

        public async void UnlockArtifact()
        {
            await unlockArtifactUseCase.Invoke();
            UpdateArtifacts();
            UpdateShopDetails();
        }

        async void UpdateArtifacts()
        {
            _artifacts.Value = await readUnlockedArtifactsUseCase.Invoke();
        }

        public void Dispose()
        {
            _disposables.Dispose();
        }

        public void Cleanup(){}
    }
}