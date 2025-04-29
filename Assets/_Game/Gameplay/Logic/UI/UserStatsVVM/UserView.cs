using TMPro;
using UnityEngine;

namespace _Game.Gameplay.Logic.UI
{
    public class UserView : MonoBehaviour

    {
        public TextMeshProUGUI CoordinateX;
        public TextMeshProUGUI CoordinateY;
        public TextMeshProUGUI Velocity;
        public TextMeshProUGUI AngleRotation;
        public TextMeshProUGUI CountLaser;


        public void SetCoordinateX(string x)
        {
            CoordinateX.text = x;
        }

        public void SetCoordinateY(string y)
        {
            CoordinateY.text = y;
        }

        public void SetVelocity(string velocity)
        {
            Velocity.text = velocity;
        }

        public void SetAngleRotation(string rotation)
        {
            AngleRotation.text = rotation;
        }

        public void SetCountLaser(string laser)
        {
            CountLaser.text = laser;
        }
    }
}