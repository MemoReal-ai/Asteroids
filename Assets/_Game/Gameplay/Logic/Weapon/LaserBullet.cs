using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Game.Gameplay.Logic.Weapon
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class LaserBullet : Bullet
    {
        public event Action<float> OnLaserReload;

        private CancellationTokenSource _cancellationTokenSource;

        protected override void OnDisable()
        {
            base.OnDisable();
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource?.Dispose();
            _cancellationTokenSource = null;
        }

        protected override void Fade()
        {
            if (CheckDistance())
            {
                _ = Reload();
                gameObject.SetActive(IsAvailable);
            }
        }


        async UniTask Reload()
        {
            IsAvailable = false;
            try
            {
                var elepsedTime = 0f;
                while (elepsedTime<_reloadTime)
                {
                    elepsedTime += Time.deltaTime;
                    OnLaserReload?.Invoke(elepsedTime / _reloadTime);
                    await UniTask.Yield();
                }
            }
            catch (OperationCanceledException)
            {
                return;
            }

            IsAvailable = true;
        }
    }
}