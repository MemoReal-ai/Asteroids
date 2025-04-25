using System;
using _Game.Gameplay.Logic.Ship;
using _Game.Gameplay.Logic.Weapon;
using Zenject;

namespace _Game.Gameplay.Logic.Service
{
    public class InputPlayer : ITickable, IInitializable, IDisposable
    {
        private readonly ShipAbstract _shipMover;
        private float _rotateInput;
        private float _moveInput;
        private readonly Shoot _shoot;
        private readonly GameTimeHandler _gameTimeHandler;
        private readonly IInput _input;


        public InputPlayer(ShipAbstract shipMover, Shoot shoot, GameTimeHandler gameTimeHandler, IInput input)
        {
            _shipMover = shipMover;
            _shoot = shoot;
            _gameTimeHandler = gameTimeHandler;
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
            GetAxisInput();
            HandleInput();
        }

        public void Dispose()
        {
            _input.OnShoot -= _shoot.Shooting;
            _input.OnChangeAmmo -= _shoot.ChangeWeapon;
            _input.OnPressedPause -= _gameTimeHandler.Pause;
            _input.OnPressedResume -= _gameTimeHandler.Unpause;
        }


        public void HandleInput()
        {
            
            _shipMover.SetDirection(_moveInput);
            _shipMover.SetRotationAngle(_rotateInput);
        }

        public void GetAxisInput()
        {
            _moveInput = _input.GetAxisVertical();
            _rotateInput = _input.GetAxisHorizontal();
        }
    }
}