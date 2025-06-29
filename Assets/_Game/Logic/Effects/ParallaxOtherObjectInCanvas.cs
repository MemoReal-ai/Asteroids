using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace _Game.Logic.Effects
{
    [RequireComponent(typeof(RectTransform))]
    public class ParallaxOtherObjectInCanvas : MonoBehaviour
    {
        private const float POINT_X_TO_SPAWN_OBJECT = 1f;

        [SerializeField] private float _speedEffect;
        [SerializeField] private float _offsetToInfinitySpawn;

        private RectTransform _rectTransform;
        private Camera _mainCamera;

        [Inject]
        public void Construct(Camera mainCamera)
        {
            _mainCamera = mainCamera;
        }

        private void Start()
        {
            _rectTransform = GetComponent<RectTransform>();
        }

        private void LateUpdate()
        {
            UpdateParallax();
        }

        private void UpdateParallax()
        {
            float positionX = _rectTransform.position.x - _speedEffect * Time.deltaTime;
            _rectTransform.position = new Vector3(positionX, _rectTransform.position.y, _rectTransform.position.z);

            var viewportPoint = _mainCamera.WorldToViewportPoint(_rectTransform.position);

            if (viewportPoint.x < -_offsetToInfinitySpawn)
            {
                Vector3 newPosition =
                    _mainCamera.ViewportToWorldPoint(new Vector3(POINT_X_TO_SPAWN_OBJECT + _offsetToInfinitySpawn,
                        Random.value,
                        _rectTransform.position.z));

                _rectTransform.position = newPosition;
            }
        }

        private void OnValidate()
        {
            if (_offsetToInfinitySpawn < 0)
            {
                _offsetToInfinitySpawn = 0;
            }
        }
    }
}