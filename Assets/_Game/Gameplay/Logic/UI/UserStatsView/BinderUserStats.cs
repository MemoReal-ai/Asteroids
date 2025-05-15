using _Game.Gameplay.Logic.UI.UserStatsView;
using R3;
using Zenject;

namespace _Game.Gameplay.Logic.UI.UserStatsVVM
{
    public class BinderUserStats : IInitializable
    {
        private readonly UserView _userView;
        private readonly ViewModelUserStats _viewModelUserStats;

        public BinderUserStats(UserView userView, ViewModelUserStats viewModelUserStats)
        {
            _userView = userView;
            _viewModelUserStats = viewModelUserStats;
        }

        public void Initialize()
        {
            _viewModelUserStats.CoordinateX.Subscribe(coordinateX => _userView.SetCoordinateX(coordinateX))
                .AddTo(_userView);
            _viewModelUserStats.CoordinateY.Subscribe(coordinateY => _userView.SetCoordinateY(coordinateY))
                .AddTo(_userView);
            _viewModelUserStats.AngleRotation.Subscribe(angleRotation => _userView.SetAngleRotation(angleRotation))
                .AddTo(_userView);
            _viewModelUserStats.Velocity.Subscribe(velocity => _userView.SetVelocity(velocity))
                .AddTo(_userView);
            _viewModelUserStats.BulletCount.Subscribe(count => _userView.SetCountLaser(count))
                .AddTo(_userView);
        }
    }
}