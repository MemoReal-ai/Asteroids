using System;
using _Game.Gameplay.Logic.Ship;
using _Game.Gameplay.Logic.Weapon;
using R3;
using Zenject;

namespace _Game.Gameplay.Logic.UI.UserStatsView
{
    public class ViewModelUserStats : ITickable, IInitializable
    {
        public readonly ReactiveProperty<string> CoordinateX = new();

        public readonly ReactiveProperty<string> CoordinateY = new();

        public readonly ReactiveProperty<string> Velocity = new();

        public readonly ReactiveProperty<string> AngleRotation = new();

        public readonly ReactiveProperty<string> BulletCount = new();


        private readonly ShipAbstract _ship;
        private readonly Shoot _shoot;


        public ViewModelUserStats(ShipAbstract ship, Shoot shoot)
        {
            _ship = ship;
            _shoot = shoot;
        }

        public void Initialize()
        {
            BulletCounter();
        }
        public void Tick()
        {
            SetUIStats();
            BulletCounter();
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