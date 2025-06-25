using System;
using R3;
using UnityEngine;
using UnityEngine.Purchasing;
using Zenject;

namespace _Game.Logic.MetaService.Purchasing_Service
{
    public class PurchasingService : IInitializable, IPurchasingService, IStoreListener
    {
        private const string REMOVE_ADS_KEY = "RemoveAds";

        public event Action<bool> OnBuyRemoveAds;

        private ConfigurationBuilder _builder;
        private bool _isPurchasingSkipAds;
        private IStoreController _storeController;
        private IExtensionProvider _extensionProvider;

        private bool IsInitialized => _storeController != null && _extensionProvider != null;

        public void Initialize()
        {
            if (IsInitialized)
            {
                return;
            }

            _builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
            _builder.AddProduct(REMOVE_ADS_KEY, ProductType.NonConsumable);
            UnityPurchasing.Initialize(this, _builder);
        }

        public void BuyRemoveAds()
        {
            if (!_isPurchasingSkipAds)
            {
                _isPurchasingSkipAds = true;
                OnBuyRemoveAds?.Invoke(_isPurchasingSkipAds);
                _storeController.InitiatePurchase(REMOVE_ADS_KEY);
            }
        }

        public bool HasPurchasingAdsSkip()
        {
            return _isPurchasingSkipAds;
        }

        public void SetFlagPurchasingAdsSkip(bool state)
        {
            _isPurchasingSkipAds = state;
        }

        public void OnInitializeFailed(InitializationFailureReason error)
        {
            Debug.LogError($"Initialization failed: {error}");
        }

        public void OnInitializeFailed(InitializationFailureReason error, string message)
        {
            Debug.LogError(message);
        }

        public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent)
        {
            return PurchaseProcessingResult.Complete;
        }

        public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
        {
            _isPurchasingSkipAds = false;
            OnBuyRemoveAds?.Invoke(_isPurchasingSkipAds);
        }

        public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
        {
            Debug.Log("Purchasing service initialized");
            _storeController = controller;
            _extensionProvider = extensions;
        }
    }
}