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
         
        private readonly ShipAbstract _shipMover;
        private Vector2 _inputVector;
        private float _horizontalInput;
        private float _verticalInput;
        private readonly Shoot _shoot;
        private readonly PresenterPause _presenterPause;
        private readonly  GameTimeHandler _gameTimeHandler;
        public InputPlayer(ShipAbstract shipMover, Shoot shoot,PresenterPause presenterPause, GameTimeHandler gameTimeHandler)
        {
            _shipMover = shipMover;
            _shoot = shoot;
            _presenterPause = presenterPause;
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
            
            if (Input.GetKey(KeyCode.Escape))
            {
                _presenterPause.Show();
                _gameTimeHandler.Pause();
                    
            }
        }
    }
}