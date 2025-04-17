using System;
using _Game.Gameplay.Logic.Ship;
using _Game.Gameplay.Logic.UI;
using _Game.Gameplay.Logic.Weapon;
using UnityEngine;
using Zenject;

namespace _Game.Gameplay.Logic.Service
{
    public class InputPlayer : ITickable
    {
        public event Action OnPause;
        private readonly ShipAbstract _shipMover;
        private Vector2 _inputVector;
        private float _horizontalInput;
        private float _verticalInput;
        private readonly Shoot _shoot;
        private readonly GameTimeHandler _gameTimeHandler;

        public InputPlayer(ShipAbstract shipMover, Shoot shoot, GameTimeHandler gameTimeHandler)
        {
            _shipMover = shipMover;
            _shoot = shoot;
            _gameTimeHandler = gameTimeHandler;
        }

        public void Tick()
        {
            _verticalInput = Input.GetAxis("Vertical");
            _horizontalInput = Input.GetAxis("Horizontal");
            _inputVector = new Vector2(_horizontalInput, _verticalInput).normalized;
            _shipMover.SetDirection(_inputVector);

            HandleInput();
        }

        private void HandleInput()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _shoot.Shooting();
            }

            if (Input.GetMouseButtonDown(1))
            {
                _shoot.ChangeWeapon();
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                OnPause?.Invoke();
                _gameTimeHandler.Pause();
            }
        }
    }
}