using System;
using _Game.Gameplay.Logic.Service;
using Zenject;

namespace _Game.Gameplay.Logic.UI
{
    public class HandlerPopup : IInitializable, IDisposable
    {
        private readonly LoseView _loseView;
        private readonly PauseViewModel _pauseViewModel;
        private readonly PauseView _pauseView;
        private readonly ViewModelLose _viewModelLose;
        private readonly InputPlayer _inputPlayer;

        public HandlerPopup(LoseView loseView, PauseViewModel pauseViewModel, PauseView pauseView,
            ViewModelLose viewModelLose, InputPlayer inputPlayer)
        {
            _loseView = loseView;
            _pauseViewModel = pauseViewModel;
            _pauseView = pauseView;
            _viewModelLose = viewModelLose;
            _inputPlayer = inputPlayer;
        }

        public void Initialize()
        {
            _pauseViewModel.OnRemovePause += HidePausePopup;
            _viewModelLose.OnLose += ShowLosePopup;
            _inputPlayer.OnPause += ShowPausePopup;
        }

        public void Dispose()
        {
            _pauseViewModel.OnRemovePause -= HidePausePopup;
            _viewModelLose.OnLose -= ShowLosePopup;
            _inputPlayer.OnPause -= ShowPausePopup;
        }

        private void HidePausePopup()
        {
            _pauseView.gameObject.SetActive(false);
        }

        private void ShowPausePopup()
        {
            _pauseView.gameObject.SetActive(true);
        }

        private void ShowLosePopup()
        {
            _loseView.gameObject.SetActive(true);
        }
    }
}