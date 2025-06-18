using System;
using _Game.Gameplay.Logic.Service.SaveAndLoadHandler;
using _Game.Logic.MetaService.AuthenticatorService;
using _Game.Logic.MetaService.DataHandler.SaveAndLoadHandler;
using _Game.Purchasing_Service;
using R3;
using UnityEngine;
using Zenject;

namespace _Game.Logic.UI.MainMenu.Store
{
    public class StoreViewModel : IInitializable, IDisposable
    {
        public readonly ReactiveProperty<bool> IsAdsRemoved = new();
        public ReactiveCommand BuyCommand { get; private set; } = new();

        private readonly IPurchasingService _purchasingService;
        private readonly DataSyncManager _dataSyncManager;

        public StoreViewModel(IPurchasingService purchasingService, DataSyncManager dataSyncManager)
        {
            _purchasingService = purchasingService;
            _dataSyncManager = dataSyncManager;
        }

        public async void Initialize()
        {
            try
            {
                await _dataSyncManager.WaitSetValidData();
                _purchasingService.OnBuyRemoveAds += UpdateStateReactiveProperty;
                UpdateStateReactiveProperty(_purchasingService.HasPurchasingAdsSkip());
                BuyCommand.Subscribe(x => _purchasingService.BuyRemoveAds());
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }

        public void Dispose()
        {
            _purchasingService.OnBuyRemoveAds -= UpdateStateReactiveProperty;
            IsAdsRemoved?.Dispose();
            BuyCommand?.Dispose();
        }

        private void UpdateStateReactiveProperty(bool state)
        {
            IsAdsRemoved.Value = !state;
        }
    }
}