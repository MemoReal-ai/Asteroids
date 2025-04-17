using System;
using MVVM;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Game.Gameplay.Logic.UI
{
    public class UserView : MonoBehaviour

    {
        [Data("CoordinateX")]
        public TextMeshProUGUI CoordinateX;
        [Data("CoordinateY")]
        public TextMeshProUGUI CoordinateY;
        [Data("Velocity")] public TextMeshProUGUI Velocity;
        [Data("AngleRotation")]
        public TextMeshProUGUI AngleRotation;
        [Data("BulletCount")]
        public TextMeshProUGUI CountLaser;
    }
}