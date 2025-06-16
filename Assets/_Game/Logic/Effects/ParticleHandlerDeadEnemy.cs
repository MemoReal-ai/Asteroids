using UnityEngine;

namespace _Game.Logic.Effects
{
    public class ParticleHandlerDeadEnemy : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _particleDead;

        public void PlayParticleDead()
        {
            _particleDead.Play();
        }
    }
}