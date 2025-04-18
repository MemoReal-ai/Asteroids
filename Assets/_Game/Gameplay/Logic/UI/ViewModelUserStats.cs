using System;
using System.Collections.Generic;
using _Game.Gameplay.Logic.Ship;
using _Game.Gameplay.Logic.Weapon;
using MVVM;
using Zenject;
using R3;
using UnityEngine.UI;

namespace _Game.Gameplay.Logic.UI
{
    public class ViewModelUserStats : ITickable, IInitializable
    {
        [Data("CoordinateX")]
        public readonly ReactiveProperty<string> CoordinateX = new();

        [Data("CoordinateY")]
        public readonly ReactiveProperty<string> CoordinateY = new();

        [Data("Velocity")] public readonly ReactiveProperty<string> Velocity = new();

        [Data("AngleRotation")]
        public readonly ReactiveProperty<string> AngleRotation = new();

        [Data("BulletCount")]
        public readonly ReactiveProperty<string> BulletCount = new();

    
        private readonly ShipAbstract _ship;
        private readonly Shoot _shoot;

        private List<LaserBullet> _laserBullets = new();
        private bool _isSubscribe = false;

        public ViewModelUserStats(ShipAbstract ship, Shoot shoot)
        {
            _ship = ship;
            _shoot = shoot;
        }

        public void Tick()
        {
            SetUIStats();
            BulletCounter();
        }

        public void Initialize()
        {
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

            _isSubscribe = false;
        }
        
    }
}