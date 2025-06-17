using UnityEngine;

namespace _Game.Logic.Effects
{
    public class ParticleHandler : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _particleDead;

        public void PlayParticleDead(Transform transformEffect)
        {
            var effect=Instantiate(_particleDead,transformEffect.position,Quaternion.identity,null);
            effect.Play();
            Destroy(effect.gameObject, effect.main.duration);
        }
    }
}