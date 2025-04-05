using System.Collections.Generic;
using _Game.Gameplay.Logic.Ship;
using UnityEngine;

namespace _Game.Gameplay.Logic.Weapon
{
    public class Shoot
    {
        private List<IWeapon> _weapons;
        private IWeapon _currentWeapon;
        private ShipAbstract _ship;
        private int _currentIndexWeapon;

        public void Init(List<IWeapon> weapons, ShipAbstract shotPoint)
        {
            _weapons = weapons;
            _ship = shotPoint;
            _currentIndexWeapon = 0;
            _currentWeapon = _weapons[_currentIndexWeapon];
        }

        public void Shooting()
        {
            var bullet = _currentWeapon.GetBullets();
            SetDirection(bullet);
        }

        private void SetDirection(Bullet bullet)
        {
            bullet.SetTransform(_ship.ShipShootPoint.transform);
        }

        public void ChangeWeapon()
        {
            var lastElement = _weapons.Count - 1;
            if (_currentIndexWeapon == lastElement)
            {
                _currentIndexWeapon = 0;
            }
            else
            {
                _currentIndexWeapon++;
            }

            _currentWeapon = _weapons[_currentIndexWeapon];
        }
    }
}