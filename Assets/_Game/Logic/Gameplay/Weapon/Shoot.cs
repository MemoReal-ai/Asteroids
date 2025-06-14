using System;
using System.Collections.Generic;
using _Game.Gameplay.Logic.Ship;
using _Game.Logic.Gameplay.Service.Sound;

namespace _Game.Gameplay.Logic.Weapon
{
    public class Shoot
    {
        public event Action OnLaserShoot;
        public event Action OnShoot;

        private List<IWeapon> _weapons;
        private IWeapon _currentWeapon;
        private ShipAbstract _ship;
        private int _currentIndexWeapon;
        private bool _isPaused = false;
        private SoundHandler _soundHandler;

        public IEnumerable<IWeapon> Weapons => _weapons;

        public void Init(List<IWeapon> weapons, ShipAbstract shotPoint, SoundHandler soundHandler)
        {
            _weapons = weapons;
            _ship = shotPoint;
            _soundHandler = soundHandler;
            _currentIndexWeapon = 0;
            _currentWeapon = _weapons[_currentIndexWeapon];
        }

        public void Shooting()
        {
            if (_isPaused == true)
            {
                return;
            }

            var bullet = _currentWeapon.GetBullets();

            if (!bullet)
            {
                return;
            }

            SetDirection(bullet);

            if (bullet is LaserBullet bulletLaser)
            {
                OnLaserShoot?.Invoke();
                _soundHandler.PlayAudioShootLaser();
            }
            else
            {
                _soundHandler.PlayAudioShootDefault();
            }

            OnShoot?.Invoke();
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

        public void PauseObject()
        {
            _isPaused = true;
        }

        public void ResumeObject()
        {
            _isPaused = false;
        }
    }
}