using _Game.Purchasing_Service;
using Zenject;
using R3;

namespace _Game.MainMenu.Logic.UI.Store
{
    public class StoreBinder : IInitializable
    {
        private readonly StorePopupView _storePopupView;
        private readonly StoreViewModel _storeViewModel;

        public StoreBinder(StoreViewModel storeViewModel, StorePopupView storePopupView)
        {
            _storeViewModel = storeViewModel;
            _storePopupView = storePopupView;
        }

        public void Initialize()
        {
            _storePopupView.PaymentButton.
                OnClickAsObservable().
                Subscribe(_storeViewModel.BuyCommand.Execute).
                AddTo(_storePopupView);
            
            _storePopupView.CloseButton.
                OnClickAsObservable().
                Subscribe(x => HideStorePopup()).
                AddTo(_storePopupView);
            
            _storePopupView.ShowPopUpButton.
                OnClickAsObservable().
                Subscribe(x => ShowStorePopup()).
                AddTo(_storePopupView);
            
        }

        private void ShowStorePopup()
        {
            _storePopupView.Show();
        }

        private void HideStorePopup()
        {
            _storePopupView.Hide();
        }
    }
}