using System;
using UnityEngine;
using UnityEngine.UI;

namespace _Game.Gameplay.Logic.UI
{
    public class ReloadView : MonoBehaviour
    {
        [SerializeField] private Image _imageReload;

        public void Filler(float amount)
        {
            _imageReload.fillAmount = amount;
        }
    }
}