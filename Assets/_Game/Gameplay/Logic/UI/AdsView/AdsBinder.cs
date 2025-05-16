using Zenject;
using R3;

namespace _Game.Gameplay.Logic.UI.AdsView
{
    public class AdsBinder : IInitializable
    {
        private readonly AdsPopupView _adsPopupView;
        private readonly AdsViewModel _adsViewModel;

        public AdsBinder(AdsPopupView adsPopupView, AdsViewModel adsViewModel)
        {
            _adsPopupView = adsPopupView;
            _adsViewModel = adsViewModel;
        }

        public void Initialize()
        {
            _adsPopupView.AdsButton
                .OnClickAsObservable()
                .Subscribe(_adsViewModel.ShowAdsCommand.Execute)
                .AddTo(_adsPopupView);

            _adsPopupView.ExitButton
                .OnClickAsObservable()
                .Subscribe(_adsViewModel.HidePopupCommand.Execute)
                .AddTo(_adsPopupView);
        }

        private void Hide()
        {
            _adsPopupView.Hide();
        }

        private void Show()
        {
            _adsPopupView.Show();
        }
    }
}