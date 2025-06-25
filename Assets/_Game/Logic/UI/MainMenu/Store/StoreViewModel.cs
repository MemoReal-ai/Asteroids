using System;
using _Game.Logic.MetaService.DataHandler.SaveAndLoadHandler;
using _Game.Logic.MetaService.Purchasing_Service;
using R3;
using UnityEngine;
using Zenject;

namespace _Game.Logic.UI.MainMenu.Store
{
    public class StoreViewModel : IInitializable, IDisposable
    {
        private readonly IPurchasingService _purchasingService;
        private readonly DataSyncManager _dataSyncManager;
        private readonly StorePopupView _storeView;

        private ReactiveProperty<bool> IsAdsRemoved { get; } = new();
        private ReactiveCommand ShowCommand { get; } = new();
        private ReactiveCommand CloseCommand { get; } = new();
        private ReactiveCommand BuyRemoveAdsCommand { get; } = new();

        public StoreViewModel(IPurchasingService purchasingService, DataSyncManager dataSyncManager,
            StorePopupView storeView)
        {
            _purchasingService = purchasingService;
            _dataSyncManager = dataSyncManager;
            _storeView = storeView;
        }

        public async void Initialize()
        {
            try
            {
                await _dataSyncManager.WaitSetValidData();
                UpdateStateReactiveProperty(_purchasingService.HasPurchasingAdsSkip());
                _purchasingService.OnBuyRemoveAds += UpdateStateReactiveProperty;

                ShowCommand.Subscribe(_ => _storeView.Show());
                CloseCommand.Subscribe(_ => _storeView.Hide());
                BuyRemoveAdsCommand.Subscribe(_ => _purchasingService.BuyRemoveAds());
                Bind();
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
        }

        private void Bind()
        {
            _storeView.ShowPopUpButton
                .OnClickAsObservable()
                .Subscribe(ShowCommand.Execute)
                .AddTo(_storeView);

            _storeView.CloseButton
                .OnClickAsObservable()
                .Subscribe(CloseCommand.Execute)
                .AddTo(_storeView);

            _storeView.PaymentButton
                .OnClickAsObservable()
                .Subscribe(BuyRemoveAdsCommand.Execute)
                .AddTo(_storeView);

            IsAdsRemoved.Subscribe(canBuy => _storeView.PaymentButton.interactable = canBuy).AddTo(_storeView);
        }

        private void UpdateStateReactiveProperty(bool state)
        {
            IsAdsRemoved.Value = !state;
        }
    }
}