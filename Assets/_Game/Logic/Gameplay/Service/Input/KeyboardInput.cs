using System;
using _Game.Logic.Gameplay.Service.Input;
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
        private bool _isInputPaused = false;

        public void Tick()
        {
            if (_isInputPaused)
            {
                return;
            }

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

        private void HandleInput()
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
                    OnPressedResume?.Invoke();
                    TogglePause();
                }
                else
                {
                    OnPressedPause?.Invoke();
                    TogglePause();
                }
            }
        }

        public void StopInput()
        {
            _isInputPaused = true;
        }

        public void ResumeInput()
        {
            _isInputPaused = false;
        }

        public void PressedResume()
        {
            OnPressedResume?.Invoke();
            TogglePause();
        }

        private void TogglePause()
        {
            _isPaused = !_isPaused;
        }
    }
}