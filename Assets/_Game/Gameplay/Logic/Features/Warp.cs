using System.Collections.Generic;
using _Game.Gameplay.Logic.Ship;
using UnityEngine;
using Zenject;

namespace _Game.Gameplay.Logic.Features
{
    public class Warp : ITickable
    {
        private readonly Camera _camera;
        private List<IWarping> _warpCreature;
        
        private List<Vector3> _positionsCreature;

        public Warp(Camera camera)
        {
            _camera = camera;
        }
        public void Init( List<IWarping> warpCreature)
        {
            _warpCreature = warpCreature;
        }
        //Переделать без костыля только Zenject!!

        public void Tick()
        {
            CheckWarping();
        }

        private void CheckWarping()
        {
            foreach (var creature in _warpCreature)
            {
                var transformCreature = creature.GetTransform();
                var view = _camera.WorldToViewportPoint(transformCreature.position);
                bool  isWarping=false;
                
                if (view.x > 1)
                {
                    view.x = 0;
                    isWarping = true;
                }
                else if (view.y > 1)
                {
                    view.y = 0;
                    isWarping = true;
                }
                else if (view.x < 0)
                {
                    view.x = 1;
                    isWarping = true;
                }
                else if (view.y < 0)
                {
                    view.y = 1;
                    isWarping = true;
                }


                if (isWarping)
                {
                    creature.SetPosition(_camera.ViewportToWorldPoint(view));
                }
            }

        }

        
    }
}