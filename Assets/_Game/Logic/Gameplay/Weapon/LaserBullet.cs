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

        private CancellationTokenSource _cancellationTokenSource = new();

        private void OnDestroy()
        {
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

        private async UniTask Reload()
        {
            IsAvailable = false;
            try
            {
                var elepsedTime = 0f;
                while (elepsedTime < BulletStatsConfig.ReloadTime)
                {
                    elepsedTime += Time.deltaTime;
                    OnLaserReload?.Invoke(elepsedTime / BulletStatsConfig.ReloadTime);

                    await UniTask.Yield(_cancellationTokenSource.Token);
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