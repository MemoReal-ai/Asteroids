using System;
using _Game.Logic.Gameplay.Service.Input;
using R3;
using UnityEngine;
using Zenject;

namespace _Game.Gameplay.Logic.Service
{
    public class KeyboardInput : ITickable, IInput, IDisposable, IInitializable
    {
        public event Action OnPressedPause;
        public event Action OnPressedResume;
        public event Action OnShoot;
        public event Action OnChangeAmmo;

        private bool _isPaused = false;
        private bool _isInputPaused = false;

        public ReactiveCommand PressedResumeCommand { get; } = new();

        public void Initialize()
        {
            SubscribeProperty();
        }

        public void Tick()
        {
            if (_isInputPaused)
            {
                return;
            }

            HandleInput();
        }

        public void Dispose()
        {
            DisposeProperty();
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

        private void DisposeProperty()
        {
            PressedResumeCommand.Dispose();
        }

        private void SubscribeProperty()
        {
            PressedResumeCommand.Subscribe(x => PressedResume());
        }

        private void TogglePause()
        {
            _isPaused = !_isPaused;
        }
    }
}