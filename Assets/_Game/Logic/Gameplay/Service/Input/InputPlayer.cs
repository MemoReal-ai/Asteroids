using System;
using _Game.Gameplay.Logic.Ship;
using _Game.Gameplay.Logic.Weapon;
using _Game.Logic.Gameplay.Service.Input;
using Zenject;

namespace _Game.Gameplay.Logic.Service
{
    public class InputPlayer : ITickable, IInitializable, IDisposable
    {
        private readonly ShipAbstract _shipMover;
        private readonly Shoot _shoot;
        private readonly GameTimeHandler _gameTimeHandler;
        private readonly IInput _input;


        public InputPlayer( GameTimeHandler gameTimeHandler,ShipAbstract shipMover, Shoot shoot, IInput input)
        {
            _gameTimeHandler = gameTimeHandler;
            _shipMover = shipMover;
            _shoot = shoot;
            _input = input;
        }

        public void Initialize()
        {
            _input.OnShoot += _shoot.Shooting;
            _input.OnChangeAmmo += _shoot.ChangeWeapon;
            _input.OnPressedPause += _gameTimeHandler.Pause;
            _input.OnPressedResume += _gameTimeHandler.Unpause;
        }

        public void Tick()
        {
            HandleInput();
        }

        public void Dispose()
        {
            _input.OnShoot -= _shoot.Shooting;
            _input.OnChangeAmmo -= _shoot.ChangeWeapon;
            _input.OnPressedPause -= _gameTimeHandler.Pause;
            _input.OnPressedResume -= _gameTimeHandler.Unpause;
        }


        private void HandleInput()
        {
            
            _shipMover.SetDirection(_input.GetAxisVertical());
            _shipMover.SetRotationAngle(_input.GetAxisHorizontal());
        }

    }
}