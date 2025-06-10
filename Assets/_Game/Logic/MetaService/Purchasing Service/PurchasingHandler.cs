using UnityEngine;
using UnityEngine.Purchasing;
using Zenject;

namespace _Game.Purchasing_Service
{
    public class PurchasingHandler : IInitializable, IPurchasingService, IStoreListener
    {
        private const string REMOVE_ADS_KEY = "RemoveAds";

        private bool _isPurchasingSkipAds;
        private IStoreController _storeController;
        private IExtensionProvider _extensionProvider;
        private bool IsInitialized => _storeController != null && _extensionProvider != null;

        public void Initialize()
        {
            InitializePurchasing();
        }

        private void InitializePurchasing()
        {
            if (IsInitialized)
            {
                return;
            }

            var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
            builder.AddProduct(REMOVE_ADS_KEY, ProductType.NonConsumable);
            UnityPurchasing.Initialize(this, builder);
        }

        public void BuyRemoveAds()
        {
            if (!_isPurchasingSkipAds)
            {
                _isPurchasingSkipAds = true;
                _storeController.InitiatePurchase(REMOVE_ADS_KEY);
                Debug.Log("Purchasing service buy");
            }
        }

        public bool HasPurchasingAdsSkip()
        {
            return _isPurchasingSkipAds;
        }

        public void SetFlagPurchasingAdsSkip(bool skip)
        {
            _isPurchasingSkipAds = skip;
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
            Debug.Log("Purchasing service failed");
        }

        public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
        {
            Debug.Log("Purchasing service initialized");
            _storeController = controller;
            _extensionProvider = extensions;
        }
    }
}