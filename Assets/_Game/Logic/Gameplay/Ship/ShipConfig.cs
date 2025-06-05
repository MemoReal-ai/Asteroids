using System;

namespace _Game.Gameplay.Logic.Ship
{
    [Serializable]
    public class ShipConfig
    {
        public string Name = "Basic";
        public float Speed = 9;
        public float RotationSpeed = 200;
        public float StopAcceleration = 0.5f;
    }
}