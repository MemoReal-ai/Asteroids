using System;

namespace _Game.Gameplay.Logic.Enemy
{
    
    public interface IEnemy
    {
        event Action<int> OnDied;
    }
}