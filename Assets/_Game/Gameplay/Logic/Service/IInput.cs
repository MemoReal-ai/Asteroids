
using System;

namespace _Game.Gameplay.Logic.Service
{
    public  interface IInput
    {
        event Action  OnPressedPause;
        event Action  OnPressedResume;
        event Action OnShoot;
        event Action OnChangeAmmo;
        
        float GetAxisHorizontal();
        float GetAxisVertical();
        
        void HandleInput();
        
    }
}
