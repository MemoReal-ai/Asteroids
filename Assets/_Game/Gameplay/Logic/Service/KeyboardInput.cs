using System;
using UnityEngine;
using Zenject;

namespace _Game.Gameplay.Logic.Service
{
    public class KeyboardInput : ITickable, IInput
    {
        public event Action OnPressedPause;
        public event Action OnPressedResume;
        public event Action OnShoot;
        public event Action OnChangeAmmo;

        private bool _isPaused = false;

        public void Tick()
        {
            HandleInput();
        }

        public float GetAxisHorizontal()
        {
            return Input.GetAxis("Horizontal");
        }

        public float GetAxisVertical()
        {
            return Input.GetAxis("Vertical");
        }

        public void HandleInput()
        {
            if (Input.GetMouseButtonDown(0))
            {
                OnShoot?.Invoke();
            }

            if (Input.GetMouseButtonDown(1))
            {
                OnChangeAmmo?.Invoke();
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (_isPaused == true)
                {
                    _isPaused = false;
                    OnPressedResume?.Invoke();
                }
                else
                {
                    _isPaused = true;
                    OnPressedPause?.Invoke();
                }
            }
        }
    }
}