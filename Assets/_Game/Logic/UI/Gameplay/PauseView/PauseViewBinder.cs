using System;
using _Game.Gameplay.Logic.Service;
using Zenject;
using R3;

namespace _Game.Gameplay.Logic.UI
{
    public class PauseViewBinder : IInitializable, IDisposable
    {
        private readonly PauseViewModel _pauseViewModel;
        private readonly PauseView _pauseView;
        private readonly GameTimeHandler _gameTimeHandler;

        public PauseViewBinder(PauseViewModel pauseViewModel, PauseView pauseView, GameTimeHandler gameTimeHandler)
        {
            _gameTimeHandler = gameTimeHandler;
            _pauseViewModel = pauseViewModel;
            _pauseView = pauseView;
        }

        public void Initialize()
        {
            _gameTimeHandler.OnPaused += Show;
            _gameTimeHandler.OnResume += Hide;

            _pauseView.ExitButton
                .OnClickAsObservable()
                .Subscribe(_pauseViewModel.ExitCommand.Execute)
                .AddTo(_pauseView);

            _pauseView.ResumeButton.
                OnClickAsObservable().
                Subscribe(_pauseViewModel.ResumeCommand.Execute)
                .AddTo(_pauseView);
        }

        public void Dispose()
        {
            _pauseViewModel?.Dispose();
        }

        private void Hide()
        {
            _pauseView.Hide();
        }

        private void Show()
        {
            _pauseView.Show();
        }
    }
}