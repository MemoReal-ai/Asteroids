using System;
using UnityEngine;

namespace _Game.Gameplay.Logic.Infrastructure
{
    [Serializable]
    public class DataStatsForAnalitycs
    {
        [SerializeField] private int _countShoot;
        [SerializeField] private int _countShootLaser;
        [SerializeField] private int _countDefeatUfo;
        [SerializeField] private int _countDefeatComet;

        public void AddCounterShoot()
        {
            _countShoot++;
        }

        public void AddShootLaserCount()
        {
            _countShootLaser++;
        }

        public void AddDefeatUfo()
        {
            _countDefeatUfo++;
        }

        public void AddDefeatComet()
        {
            _countDefeatComet++;
        }
    }
}