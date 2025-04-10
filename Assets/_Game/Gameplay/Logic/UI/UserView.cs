using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Game.Gameplay.Logic.UI
{
    public class UserView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _coordinateX;
        [SerializeField] private TextMeshProUGUI _coordinateY;
        [SerializeField] private TextMeshProUGUI _velocity;
        [SerializeField] private TextMeshProUGUI _angleRotation;
        [SerializeField] private TextMeshProUGUI _countLaser;
        [SerializeField] private Image _imageTimeReloadLaser;


        public void SetCoordinate(float valueX, float valueY)
        {
            _coordinateX.text = Math.Round(valueX,2).ToString();
            _coordinateY.text = Math.Round(valueY,2).ToString();
        }

        public void SetVelocity(float valueVelocity)
        {
            _velocity.text = Math.Round(valueVelocity,2)+"m/s";
        }

        public void SetAngleRotation(float valueAngle)
        {
            _angleRotation.text = Math.Round(valueAngle,2)+"`";
        }

        public void SetCountLaser(float value)
        {
            _countLaser.text = value.ToString();
        }

        public void SetTimeReloadLaser(float value)
        {
            _imageTimeReloadLaser.fillAmount = value;
        }
    }
}