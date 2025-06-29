using UnityEngine;
using UnityEngine.UI;

namespace _Game.Logic.Effects
{
    [RequireComponent(typeof(RawImage))]
    public class ParallaxBackground : MonoBehaviour
    {
        private const float TRESHOLD_X = 1f;
        private const float DEFAULT_POSITION_X = 0f;

        [SerializeField] private float _speedParallax;

        private RawImage _rawImage;
        private float _uvRectX;

        private void Start()
        {
            _rawImage = GetComponent<RawImage>();
            _uvRectX = _rawImage.uvRect.x;
        }

        private void LateUpdate()
        {
            UpdateParallax();
        }

        private void UpdateParallax()
        {
            float newPosition = _uvRectX += _speedParallax * Time.deltaTime;

            if (newPosition > TRESHOLD_X)
            {
                _uvRectX = DEFAULT_POSITION_X;
            }

            _rawImage.uvRect = new Rect(newPosition,
                _rawImage.uvRect.y,
                _rawImage.uvRect.width,
                _rawImage.uvRect.height);
        }
    }
}