using System;

namespace _Game.Gameplay.Logic.Service
{
    public class GameTimeHandler
    {
        public event Action OnPaused;
        public event Action OnLoseGame;
        public event Action OnResume;
        public event Action OnPauseToAds;

        public void Pause()
        {
            OnPaused?.Invoke();
        }

        public void PauseToAds()
        {
            OnPauseToAds?.Invoke();
        }

        public void LoseGame()
        {
            OnLoseGame?.Invoke();
        }

        public void Unpause()
        {
            OnResume?.Invoke();
        }
    }
}