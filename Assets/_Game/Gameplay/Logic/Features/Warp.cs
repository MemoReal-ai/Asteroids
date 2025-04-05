using _Game.Gameplay.Logic.Ship;
using UnityEngine;
using Zenject;

namespace _Game.Gameplay.Logic.Features
{
    public class Warp : ITickable
    {
        private readonly Camera _camera;
        private readonly ShipAbstract _shipAbstract;
        private bool _isWarping;

        public Warp(Camera camera, ShipAbstract shipAbstract)
        {
            _camera = camera;
            _shipAbstract = shipAbstract;
        }

        public void Tick()
        {
            CheckWarping();
        }

        private void CheckWarping()
        {
            var view = _camera.WorldToViewportPoint(_shipAbstract.transform.position);

            if (view.x > 1)
            {
                view.x = 0;
                _isWarping = true;
            }
            else if (view.y > 1)
            {
                view.y = 0;
                _isWarping = true;
            }
            else if (view.x < 0)
            {
                view.x = 1;
                _isWarping = true;
            }
            else if (view.y < 0)
            {
                view.y = 1;
                _isWarping = true;
            }


            if (_isWarping)
            {
                _shipAbstract.transform.position = _camera.ViewportToWorldPoint(view);
            }
        }
    }
}