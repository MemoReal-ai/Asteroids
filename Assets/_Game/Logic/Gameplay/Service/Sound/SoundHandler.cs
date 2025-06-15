using UnityEngine;

namespace _Game.Logic.Gameplay.Service.Sound
{
    public class SoundHandler : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioShootDefault;
        [SerializeField] private AudioSource _audioShootLaser;
        [SerializeField] private AudioSource _audioDeadEnemy;

        public void PlayAudioShootDefault()
        {
            _audioShootDefault.Play();
        }

        public void PlayAudioShootLaser()
        {
            _audioShootLaser.Play();
        }

        public void PlayAudioDead()
        {
            _audioDeadEnemy.Play();
        }
    }
}