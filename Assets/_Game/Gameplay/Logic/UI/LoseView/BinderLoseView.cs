using System;
using _Game.Gameplay.Logic.Ship;
using Zenject;
using R3;

namespace _Game.Gameplay.Logic.UI.LoseUI
{
    public class BinderLoseView : IInitializable, IDisposable
    {
        private readonly LoseView _loseView;
        private readonly ViewModelLose _viewModelLose;
        private readonly ShipAbstract _ship;

        public BinderLoseView(ShipAbstract ship, LoseView loseView,
            ViewModelLose viewModelLose)
        {
            _ship = ship;
            _loseView = loseView;
            _viewModelLose = viewModelLose;
        }

        public void Initialize()
        {
            _ship.OnLoseLastLife += _loseView.Show;

            _loseView.RestartButton.
                OnClickAsObservable().
                Subscribe(_viewModelLose.RestartCommand.Execute).
                AddTo(_loseView);
            
            _loseView.QuitButton.
                OnClickAsObservable().
                Subscribe(_viewModelLose.QuitCommand.Execute).
                AddTo(_loseView);
            
            _viewModelLose.Points.
                Subscribe(points=>_loseView.ShowPoints(points)).
                AddTo(_loseView);
        }

        public void Dispose()
        {
            _ship.OnLoseLastLife -= _loseView.Show;
        }
    }
}