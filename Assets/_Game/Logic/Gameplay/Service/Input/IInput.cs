using System;
using R3;

namespace _Game.Logic.Gameplay.Service.Input
{
    public interface IInput
    {
        event Action OnPressedPause;
        event Action OnPressedResume;
        event Action OnShoot;
        event Action OnChangeAmmo;

        float GetAxisHorizontal();

        float GetAxisVertical();

        void StopInput();

        void ResumeInput();

        ReactiveCommand PressedResumeCommand { get; }
    }
}