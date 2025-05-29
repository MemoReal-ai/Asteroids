using UnityEngine;
using UnityEngine.Purchasing;
using Zenject;

namespace _Game.Purchasing_Service
{
    public class PurchasingHandler : IInitializable, IPurchasingService, IStoreListener
    {
        private bool _isPurchasingSkipAds;
        private EnumPurchasing _purchasing;
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
                return;

            var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
            builder.AddProduct(EnumPurchasing.PurchaseSkipAds.ToString(), ProductType.NonConsumable);

            UnityPurchasing.Initialize(this, builder);
        }

        public void Buy(EnumPurchasing purchasing)
        {
            _purchasing = purchasing;
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
        }

        public void OnInitializeFailed(InitializationFailureReason error, string message)
        {
            Debug.LogError(message);
        }

        public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent)
        {
            if (purchaseEvent.purchasedProduct.definition.id == _purchasing.ToString())
            {
                if (!_isPurchasingSkipAds)
                {
                    _isPurchasingSkipAds = true;
                    Debug.Log("Purchasing service buy");
                }
                else
                {
                    _storeController.InitiatePurchase(_purchasing.ToString());
                    return PurchaseProcessingResult.Pending;
                }
            }

            return PurchaseProcessingResult.Complete;
        }

        public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
        {
            Debug.Log("Purchasing service failed");
        }

        public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
        {
            _storeController = controller;
            _extensionProvider = extensions;
        }
    }
}