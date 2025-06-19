using System;
using _Game.Gameplay.Logic.Service;
using _Game.Gameplay.Logic.Ship;
using _Game.Gameplay.Logic.UI;
using _Game.Gameplay.Logic.Weapon;
using _Game.Logic.Gameplay.Weapon;
using R3;
using Zenject;

namespace _Game.Logic.UI.Gameplay.UserStatsView
{
    public class ViewModelUserStats : ITickable, IInitializable, IDisposable
    {
        private readonly ShipAbstract _ship;
        private readonly Shoot _shoot;
        private readonly UserView _userView;

        private readonly ReactiveProperty<string> CoordinateX = new();
        private readonly ReactiveProperty<string> CoordinateY = new();
        private readonly ReactiveProperty<string> Velocity = new();
        private readonly ReactiveProperty<string> AngleRotation = new();
        private readonly ReactiveProperty<string> BulletCount = new();

        public ViewModelUserStats(ShipAbstract ship, Shoot shoot, UserView userView)
        {
            _ship = ship;
            _shoot = shoot;
            _userView = userView;
        }

        public void Initialize()
        {
            BulletCounter();
            Bind();
        }

        public void Tick()
        {
            SetUIStats();
            BulletCounter();
        }

        public void Dispose()
        {
            CoordinateX?.Dispose();
            CoordinateY?.Dispose();
            Velocity?.Dispose();
            AngleRotation?.Dispose();
            BulletCount?.Dispose();
        }

        private void Bind()
        {
            CoordinateX.Subscribe(coordinateX => _userView.SetCoordinateX(coordinateX))
                .AddTo(_userView);
            CoordinateY.Subscribe(coordinateY => _userView.SetCoordinateY(coordinateY))
                .AddTo(_userView);
            AngleRotation.Subscribe(angleRotation => _userView.SetAngleRotation(angleRotation))
                .AddTo(_userView);
            Velocity.Subscribe(velocity => _userView.SetVelocity(velocity))
                .AddTo(_userView);
            BulletCount.Subscribe(count => _userView.SetCountLaser(count))
                .AddTo(_userView);
        }

        private void SetUIStats()
        {
            CoordinateX.Value = $"{Math.Round(_ship.transform.position.x, 2)}";
            CoordinateY.Value = $"{Math.Round(_ship.transform.position.y, 2)}";
            Velocity.Value = $"{Math.Round(_ship.Rigidbody2D.linearVelocity.magnitude, 2)}";
            AngleRotation.Value = $"{Math.Round(_ship.transform.eulerAngles.z)}";
        }
        
        private void BulletCounter()
        {
            foreach (var weapon in _shoot.Weapons)
            {
                if (weapon is LaserWeapon laserWeapon)
                {
                    var counter = 0;
                    foreach (var bullet in laserWeapon.Bullets)
                    {
                        if (bullet.IsAvailable)
                        {
                            counter++;
                        }
                    }

                    BulletCount.Value = $"{counter}";
                }
            }
        }
    }
}