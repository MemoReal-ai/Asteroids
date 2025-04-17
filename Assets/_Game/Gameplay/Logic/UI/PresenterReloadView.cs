using System;
using System.Collections.Generic;
using _Game.Gameplay.Logic.Ship;
using _Game.Gameplay.Logic.Weapon;
using UnityEngine;
using Zenject;

namespace _Game.Gameplay.Logic.UI
{
    public class PresenterReloadView : IInitializable, IDisposable
    {
        private readonly Shoot _shoot;
        private readonly ReloadView _reloadView;

        private readonly List<LaserBullet> _laserBullets=new();

        public PresenterReloadView(Shoot shoot, ReloadView reloadView)
        {
            _reloadView = reloadView;
            _shoot = shoot;
        }

        public void Initialize()
        {
            BulletCounter();
        }

        private void BulletCounter()
        {
            foreach (var weapon in _shoot.Weapons)
            {
                if (weapon is LaserWeapon laserWeapon)
                {
                    foreach (var bullet in laserWeapon.Bullets)
                    {
                        if (bullet is LaserBullet laserBullet)
                        {
                            laserBullet.OnLaserReload += _reloadView.Filler;
                            _laserBullets.Add(laserBullet);
                        }
                    }
                }
            }
        }

        public void Dispose()
        {
            foreach (var bullet in _laserBullets)
            {
                bullet.OnLaserReload -= _reloadView.Filler;
                
            }
        }
    }
}