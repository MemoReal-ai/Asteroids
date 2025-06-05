using UnityEngine;

namespace _Game.Gameplay.Logic.Features
{
    public interface IWarping
    {
        Transform GetTransform();
        void SetPosition(Vector3 warpingPosition);
    }
}