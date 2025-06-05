using System;
using _Game.Gameplay.Logic.Weapon;

namespace _Game.Gameplay.Logic.Enemy
{
    [Serializable]
    public class CometConfig
    {
        public float DistanceToFade;
        public float MinSpeed;
        public int CountSmallComet;
        public int MaxSpeed;
    }
}